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
using System.Collections;
using System.Collections.ObjectModel;

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

        private int ThirtySeconds = 0;
        private int LoginRegisterFailedCounter = 0; // When this get's to three - pop-up the login registration form as the automatic system is failing.
        
        private ObservableCollection<string> alUrls = new ObservableCollection<string>();

        private bool ClosingDown = false;
        private bool Waiting = true;

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
            //tcpClient.errEncounter += tcpClient_errEncounter;
            tcpClient.Disconnected += tcpClient_Disconnected;
            tcpClient.NoDelay = true;
            alUrls.CollectionChanged += alUrls_CollectionChanged;
            Task.Run(() => Startup());
        }

        void alUrls_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    ProcessUrls();
                    break;
                
                default:
                    break;
            }
        }

        private void Startup()
        {
            Log("Connecting...");
            tcpClient.Connect("168.63.37.37", 9119, myClientID); //TODO: Ip Address may need to be more dynamic - shall check
            timerConnectionCheck.Enabled = false;

            if (Properties.Settings.Default.RememberMe) // Auto login if RememberMe is true
            {
                SendLogin(Properties.Settings.Default.Username, Properties.Settings.Default.Password);
            }
        }



        private enum ClientCommands
        {
            Login,
            Register,
            Get,
            NOP,
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
            SetLabel(lblLastCommand, lastCommand);
            LogToConOut(lastCommand);
        }

        #endregion


        void tcpClient_Disconnected()
        {
            if (!ClosingDown)
            {
                if (!tcpClient.isConnected && timerConnectionCheck.Enabled == false)
                {
                    timerConnectionCheck.Enabled = true;
                    SetLabel(lblStatus, "Timer Connection Check Enabled");
                }
                else if (!tcpClient.isConnected && timerConnectionCheck.Enabled == true)
                {
                    // Ignore this
                    SetLabel(lblStatus, "Timer Connection Check Enabled");
                }
                else
                {
                    SetLabel(lblStatus, "Timer Connection Check Disabled");
                    timerConnectionCheck.Enabled = false;
                    Log("Connected.");
                    SendLogin(Properties.Settings.Default.Username, Properties.Settings.Default.Password);
                    Log("Re-Logging In");
                }
            }
        }

        //void tcpClient_errEncounter(Exception ex)
        //{
        //    Log("Error Encountered: " + ex.Message);
        //    throw new NotImplementedException();
        //}

        private enum ServerCommands
        {
            Successful,
            Failed,
            UseCache,
            Wait,
            Resume,
            SendEmailAddress,
            SetMessageSize,
        }

        void tcpClient_DataReceived(byte[] Data, string ID)
        {
            timerConnectionCheck.Enabled = false; // if this is running it is no longer needed
            if (string.IsNullOrEmpty(ID)) // Message from the Server
            {
                string data = utils.GetString(Data);
                string[] parts = data.Split(' ');

                if (parts.Count() == 1)
                {
                    ServerCommands commandReceived = (ServerCommands)Enum.Parse(typeof(ServerCommands), parts[0].ToString());
                    switch (commandReceived)
                    {
                        case ServerCommands.Successful:
                            break;
                        case ServerCommands.Failed:
                            break;
                        case ServerCommands.UseCache:
                            Waiting = true;
                            break;
                        case ServerCommands.Wait:
                            Waiting = true;
                            break;
                        case ServerCommands.Resume:
                            Waiting = false;
                            break;
                        default:
                            break;
                    }
                }
                else if (parts.Count() == 2)
                {
                    // Received information about the command we sent - what happened?  
                    ServerCommands valueReceived;
                    ClientCommands commandSent;

                    try
                    {
                        commandSent = (ClientCommands)Enum.Parse(typeof(ClientCommands), parts[0].ToString());
                        valueReceived = (ServerCommands)Enum.Parse(typeof(ServerCommands), parts[1].ToString());
                    }
                    catch (Exception)
                    {
                        // We do not always receive an answer to a question - sometimes the server sends a command without it being a reply to us
                        valueReceived = (ServerCommands)Enum.Parse(typeof(ServerCommands), parts[0].ToString());
                        commandSent = ClientCommands.NOP;
                    }


                    switch (valueReceived)
                    {
                        case ServerCommands.Successful:
                            break;
                        case ServerCommands.Failed:
                            break;
                        case ServerCommands.UseCache:
                            break;
                        case ServerCommands.Wait:
                            break;
                        case ServerCommands.Resume:
                            break;
                        case ServerCommands.SetMessageSize:
                            SetReceiveMessageSize(parts);
                            break;
                        default:
                            break;
                    }

                    switch (commandSent)
                    {
                        case ClientCommands.Login:
                            LoginFailed(valueReceived);
                            break;
                        case ClientCommands.Register:
                            RegistrationFailed(valueReceived);
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
                            LoginSuccessful(valueReceived, response);
                            break;
                        case ClientCommands.Register:
                            RegistrationSuccessful(valueReceived, response);
                            break;
                        default:
                            break;
                    }
                }
                else if (parts.Count() > 10)
                {
                    ClientCommands commandSent = (ClientCommands)Enum.Parse(typeof(ClientCommands), parts[0].ToString());
                    ServerCommands valueReceived = (ServerCommands)Enum.Parse(typeof(ServerCommands), parts[1].ToString());
                    string response = parts[2].ToString();
                    switch (valueReceived)
                    {
                        case ServerCommands.Successful:
                            GetSuccessful(parts, commandSent);
                            
                            break;
                        case ServerCommands.Failed:
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

        private void GetSuccessful(string[] parts, ClientCommands commandSent)
        {
            switch (commandSent)
            {
                case ClientCommands.Get:
                    {
                        for (int idx = 2; idx < 12; idx++)
                        {
                            string url = parts[idx].ToString();
                            if (!string.IsNullOrEmpty(url))
                            {
                                alUrls.Add(url);
                            }
                        }
                        break;
                    }
            }
        }

        private void RegistrationSuccessful(ServerCommands valueReceived, string response)
        {
            switch (valueReceived)
            {
                case ServerCommands.Successful:
                    LoginRegisterFailedCounter = 0;
                    // Successfully Registered and Logged In.
                    SetLabel(lblAuthorized, "Authorised");
                    SetLabel(lblLicenseNumber, "License: " + response);
                    Properties.Settings.Default.IsAuthorised = true;
                    timerConnectionCheck.Enabled = false; // if this is running it is no longer needed
                    Send(ClientCommands.Get.ToString());
                    break;

                default:
                    break;
            }
        }

        private void LoginSuccessful(ServerCommands valueReceived, string response)
        {
            switch (valueReceived)
            {
                case ServerCommands.Successful:
                    // Successfully Logged in.
                    LoginRegisterFailedCounter = 0;
                    SetLabel(lblAuthorized, "Authorised");
                    SetLabel(lblLicenseNumber, "License: " + response);
                    Properties.Settings.Default.IsAuthorised = true;
                    Send(ClientCommands.Get.ToString());
                    timerConnectionCheck.Enabled = false; // If this is running we can stop it now.
                    break;

                default:
                    break;
            }
        }

        private void SetReceiveMessageSize(string[] parts)
        {
            Log("Increasing Message size to: " + parts[1].ToString());
            tcpClient.ReceiveBufferSize = int.Parse(parts[1]);
        }

        private void LoginFailed(ServerCommands valueReceived)
        {
            switch (valueReceived)
            {
                case ServerCommands.Failed:
                    if (LoginRegisterFailedCounter < 2)
                    {
                        LoginRegisterFailedCounter++;
                        SetLabel(lblAuthorized, "Unauthorised");
                        Properties.Settings.Default.IsAuthorised = false;
                        // If we have the user's details saved - try re-registering
                        if (!string.IsNullOrEmpty(Properties.Settings.Default.Password) && !string.IsNullOrEmpty(Properties.Settings.Default.Username))
                        {
                            SendRegistration(Properties.Settings.Default.Username, Properties.Settings.Default.Password);
                        }
                    }
                    else
                    {
                        // Ok the automatic system is not working for some reason. reset the saved details and show the login register form
                        Properties.Settings.Default.Password = string.Empty;
                        Properties.Settings.Default.Username = string.Empty;
                        ShowLoginForm();
                    }
                    break;
                default:
                    break;
            }
        }

        private void RegistrationFailed(ServerCommands valueReceived)
        {
            switch (valueReceived)
            {
                case ServerCommands.Failed:
                    if (LoginRegisterFailedCounter < 2)
                    {
                        LoginRegisterFailedCounter++;
                        SetLabel(lblAuthorized, "Unauthorised");
                        Properties.Settings.Default.IsAuthorised = false;
                        // If we have the user details saved - try logging in instead.
                        if (!string.IsNullOrEmpty(Properties.Settings.Default.Password) && !string.IsNullOrEmpty(Properties.Settings.Default.Username))
                        {
                            SendLogin(Properties.Settings.Default.Username, Properties.Settings.Default.Password);
                        }
                    }
                    else
                    {
                        // Ok the automatic system is not working for some reason. reset the saved details and show the login register form
                        Properties.Settings.Default.Password = string.Empty;
                        Properties.Settings.Default.Username = string.Empty;
                        ShowLoginForm();
                    }
                    break;
                default:
                    break;
            }
        }

        private void ProcessUrls()
        {
            throw new NotImplementedException();
        }


        void tcpClient_Connected()
        {
            timerConnectionCheck.Enabled = false;
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
                    if (message == "Authorised")
                    {
                        lbl.ForeColor = Properties.Settings.Default.ForeColour;
                        Properties.Settings.Default.IsAuthorised = true;
                    }
                    else
                    {
                        lbl.ForeColor = Properties.Settings.Default.ErrorForeColour;
                        lbl.BackColor = Properties.Settings.Default.ErrorBackColour;
                        Properties.Settings.Default.IsAuthorised = false;
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
                LogToConOut(message);
            }
        }

        private void LogToConOut(string message)
        {
            if (this.rtbConOut.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(LogToConOut);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                rtbConOut.AppendText(DateTime.Now + "\t" + message + Environment.NewLine);
                rtbConOut.Focus();
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
            ThirtySeconds++;
            if(ThirtySeconds == 30)
            {
                ThirtySeconds = 0;
                if (!Properties.Settings.Default.IsAuthorised)
                    SendLogin(Properties.Settings.Default.Username, Properties.Settings.Default.Password);
            }
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
            ShowLoginForm();
        }

        private void ShowLoginForm()
        {
            frmLoginRegister login = new frmLoginRegister();
            DialogResult dr = login.ShowDialog();

            if (dr != System.Windows.Forms.DialogResult.Cancel)
            {
                Properties.Settings.Default.RememberMe = login.RememberMe;

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

        private void timerConnectionCheck_Tick(object sender, EventArgs e)
        {
            Log("Retrying Connection");
            tcpClient.Connect("168.63.37.37", 9119, myClientID); //TODO: Ip Address may need to be more dynamic - shall check
        }



    }
}
