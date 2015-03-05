using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetComm;
using System.IO;
using RaptorWebWalker.HelperClasses;
using RaptorWebWalker.forms;

namespace RaptorWebWalker
{
    public partial class frmMain : Form
    {
        NetComm.Client tcpClient = new Client();
        Utilities utils = new Utilities();

        delegate void SetTextCallback(string text);
        delegate void SetLabelCallBack(Label lbl, string text);

        private string myClientID = "";
        private string lastCommand = "";

        private long SecondsPastSinceBoot = 0;
        private long totalRunTime = 0;


        private bool ClosingDown = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (FirstRun())
                SetupClient();
            else
                LoadSettings();


            tcpClient.Connected += tcpClient_Connected;
            tcpClient.DataReceived += tcpClient_DataReceived;
            tcpClient.errEncounter += tcpClient_errEncounter;
            tcpClient.Disconnected += tcpClient_Disconnected;
            tcpClient.NoDelay = true;

            Log("Connecting...");
            tcpClient.Connect("168.63.37.37", 9119, myClientID); //TODO: Ip Address may need to be more dynamic - shall check
            while (!tcpClient.isConnected)
            {
                System.Threading.Thread.Sleep(5000);
                Log("Retrying Connection.");
                tcpClient.Connect("168.63.37.37", 9119, myClientID); //TODO: Ip Address may need to be more dynamic - shall check
            }



        }



        private enum ClientCommands
        {
            Login,
            Register,
        }


        #region Send Commands



        private void SendLogin(string EmailAddress, string Password)
        {
            string command = ClientCommands.Login + " " + EmailAddress + " " + utils.HashPassword(Password);
            Send(command);
        }

        private void SendRegistration(string EmailAddress, string Password)
        {
            string command = ClientCommands.Register + " " + EmailAddress + " " + utils.HashPassword(Password);
            Send(command);
        }

        private void Send(string command)
        {
            lastCommand = command.Split(' ')[0].ToString();
            tcpClient.SendData(utils.GetBytes(command));
        }

        #endregion


        void tcpClient_Disconnected()
        {
            if (!ClosingDown)
            {
                while (!tcpClient.isConnected)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(500);
                    Log("Lost Connection.");
                    tcpClient.Connect("168.63.37.37", 9119, myClientID); //TODO: Ip Address may need to be more dynamic - shall check
                }
                Log("Connected.");
                //if (!IsAuthorised)
                //    LoginToRaptorTCP();
            }
        }

        void tcpClient_errEncounter(Exception ex)
        {
            Log("Error Encountered: " + ex.Message);
            throw new NotImplementedException();
        }

        private enum ServerCommands
        {
            Successful,
            Failed,
            UseCache,
            Wait,
            Resume,
        }

        void tcpClient_DataReceived(byte[] Data, string ID)
        {

            if (string.IsNullOrEmpty(ID)) // Message from the Server
            {
                string data = utils.GetString(Data);
                string[] parts = data.Split(' ');

                if (parts.Count() == 1)
                {

                }
                else if (parts.Count() == 2)
                {
                    // Received information about the command we sent - what happened?                    
                    ClientCommands commandSent = (ClientCommands)Enum.Parse(typeof(ClientCommands), parts[0].ToString());
                    ServerCommands valueReceived = (ServerCommands)Enum.Parse(typeof(ServerCommands), parts[1].ToString());

                    switch (commandSent)
                    {
                        case ClientCommands.Login:
                            switch (valueReceived)
                            {
                                case ServerCommands.Failed:                                    
                                    SetLabel(lblAuthorized, "Unauthorised");
                                    Properties.Settings.Default.IsAuthorised = false;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case ClientCommands.Register:
                            switch (valueReceived)
                            {
                                case ServerCommands.Failed:                                    
                                    SetLabel(lblAuthorized, "Unauthorised");
                                    Properties.Settings.Default.IsAuthorised = false;
                                    if (!string.IsNullOrEmpty(Properties.Settings.Default.Password) && !string.IsNullOrEmpty(Properties.Settings.Default.Username))
                                    {
                                        SendLogin(Properties.Settings.Default.Username, Properties.Settings.Default.Password);
                                    }

                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }


                }
                else if (parts.Count() == 3)
                {
                    // Received information about the command we sent - what happened?                    
                    ClientCommands commandSent = (ClientCommands)Enum.Parse(typeof(ClientCommands), parts[0].ToString());
                    ServerCommands valueReceived = (ServerCommands)Enum.Parse(typeof(ServerCommands), parts[1].ToString());
                    string response = parts[2].ToString();
                    switch (commandSent)
                    {
                        case ClientCommands.Login:
                            switch (valueReceived)
                            {
                                case ServerCommands.Successful:
                                    // Successfully Logged in.
                                    SetLabel(lblAuthorized, "Authorised");
                                    SetLabel(lblLicenseNumber, "License: " + response);
                                    Properties.Settings.Default.IsAuthorised = true;
                                    break;

                                default:
                                    break;
                            }
                            break;
                        case ClientCommands.Register:
                            switch (valueReceived)
                            {
                                case ServerCommands.Successful:
                                    // Successfully Registered and Logged In.
                                    SetLabel(lblAuthorized, "Authorised");
                                    SetLabel(lblLicenseNumber, "License: " + response);
                                    Properties.Settings.Default.IsAuthorised = true;
                                    break;

                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Log("Received: " + data);
                }

            }
            else // Message from another user.
            {
                Log("Message from another user: " + ID);
            }

        }




        void tcpClient_Connected()
        {
            Log("Connected to RaptorTCP Server");
        }


        #region Logging and UI Output

        private void SetLabel(Label lbl, string message)
        {
            if (lbl.InvokeRequired)
            {
                SetLabelCallBack d = new SetLabelCallBack(SetLabel);
                this.Invoke(d, new object[] { lbl, message });
            }
            else
            {
                if (lbl == lblAuthorized) // Update the User's Authroisation Status
                {
                    Properties.Settings.Default.IsAuthorised = true;

                    if (lbl.Text == "Unauthorised")
                    {
                        lbl.ForeColor = Properties.Settings.Default.ErrorForeColour;
                        lbl.BackColor = Properties.Settings.Default.ErrorBackColour;
                    }
                    else
                    {
                        lbl.ForeColor = Properties.Settings.Default.ForeColour;
                    }
                }
                lbl.Text = message;
            }
        }

        private void Log(string message)
        {

            if (this.lblStatus.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Log);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                lblStatus.Text = message;
                if (message.ToLowerInvariant().StartsWith("error"))
                {
                    string formattedMessage = DateTime.Now + "\t" + message;
                }
            }
        }




        #endregion


        #region First Run Operations

        private void SetupClient()
        {
            myClientID = CreateClientID(); // Create a Unique ID for this client
            SaveSettings();
        }

        private string CreateClientID()
        {
            return Guid.NewGuid().ToString();
        }

        private bool FirstRun()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.ClientID))
                return true;
            return false;
        }

        #endregion


        private void SaveSettings()
        {            
            Properties.Settings.Default.ClientID = myClientID;
            Properties.Settings.Default.LastRunTimeDuration = totalRunTime;
            Properties.Settings.Default.Save();
        }

        private void LoadSettings()
        {
            myClientID = Properties.Settings.Default.ClientID;
            totalRunTime = Properties.Settings.Default.LastRunTimeDuration;
        }

        private void timerSeconds_Tick(object sender, EventArgs e)
        {
            SecondsPastSinceBoot++;
            totalRunTime = SecondsPastSinceBoot + Properties.Settings.Default.LastRunTimeDuration;
            lblRuntimeSinceLastBoot.Text = utils.SecondsToDHMS(SecondsPastSinceBoot);
            lblTotalRuntime.Text = utils.SecondsToDHMS(totalRunTime);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.IsAuthorised = false;
            ClosingDown = true;
            tcpClient.Disconnect();
            SaveSettings();
        }

        #region CMS Menu
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoginRegister login = new frmLoginRegister();
            DialogResult dr = login.ShowDialog();

            if (dr != System.Windows.Forms.DialogResult.Cancel)
            {
                if (login.RememberMe)
                {
                    Properties.Settings.Default.Password = login.Password;
                    Properties.Settings.Default.Username = login.EmailAddress;
                }

                if (login.IsRegistering)
                {
                    SendRegistration(login.EmailAddress, login.Password);
                }
                else
                {
                    SendLogin(login.EmailAddress, login.Password);
                }
            }
        }
        #endregion



    }
}
