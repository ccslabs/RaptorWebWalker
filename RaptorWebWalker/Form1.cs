using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using RaptorWebWalker.HelperClasses;
using RaptorWebWalker.forms;
using System.Collections;
using System.Collections.ObjectModel;

namespace RaptorWebWalker
{
    public partial class frmMain : Form
    {
        RaptorWebWalker.Classes.Cache.Cache Cache = new Classes.Cache.Cache();
        Utilities utils = new Utilities();
        
        delegate void SetTextCallback(string text);
        delegate void SetLabelCallBack(Label lbl, string text);


        private string myClientID = "";
        private string lastCommand = "";

        private long SecondsPastSinceBoot = 0;
        private long totalRunTime = 0;

        private int ThirtySeconds = 0;

        private ObservableCollection<string> alUrls = new ObservableCollection<string>();

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


            alUrls.CollectionChanged += alUrls_CollectionChanged;
            Task.Run(() => Startup());
        }

        void alUrls_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    //  ProcessUrls();
                    break;

                default:
                    break;
            }
        }

        private void Startup()
        {
            Log("Starting...");
            
            if(IstcpServerOnline())
            {
                if(IsCacheInUse())
                {
                    Cache.ProcessCache();
                }
                else
                {

                }
            }
            else
            {
                if(IsCacheInUse())
                {
                    Cache.ProcessCache();
                }
                else
                {
                    Log("Waiting for TCP Server");
                }
            }
        }

        

        private bool IsCacheInUse()
        {
            throw new NotImplementedException();
        }

        private bool IstcpServerOnline()
        {
            // Send Hello and see if we get a response.

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
            Log("Sending Login request");
            string command = ClientCommands.Login + " " + EmailAddress + " " + utils.HashPassword(Password);
            Send(command);
        }

        private void SendRegistration(string EmailAddress, string Password)
        {
            Log("Sending Registration Request");
            string command = ClientCommands.Register + " " + EmailAddress + " " + utils.HashPassword(Password);
            Send(command);
        }

        private void Send(string command)
        {
            Log("Sending Data");
            lastCommand = command.Split(' ')[0].ToString();
            // tcpClient.SendData(utils.GetBytes(command));
            SetLabel(lblLastCommand, lastCommand);
            LogToConOut(lastCommand);
        }

        #endregion




        ////void tcpClient_errEncounter(Exception ex)
        ////{
        ////    Log("Error Encountered: " + ex.Message);
        ////    throw new NotImplementedException();
        ////}

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



        private void ProcessUrls()
        {
            Log("Processing URLS");
            throw new NotImplementedException();
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

        string lastMessage = "";
        private void LogToConOut(string message)
        {
            if (message != lastMessage)
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
                lastMessage = message;
            }
        }




        #endregion


        #region First Run Operations

        private void SetupClient()
        {
            Log("Setting Up Client");
            myClientID = CreateClientID(); // Create a Unique ID for this client
            SaveSettings();
        }

        private string CreateClientID()
        {
            Log("Creating Client ID");
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
            Log("Saving Settings");
            Properties.Settings.Default.ClientID = myClientID;
            Properties.Settings.Default.LastRunTimeDuration = totalRunTime;
            Properties.Settings.Default.Save();
        }

        private void LoadSettings()
        {
            Log("Loading Settings");
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
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log("Application Closing");
            Properties.Settings.Default.IsAuthorised = false;


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


    }
}
