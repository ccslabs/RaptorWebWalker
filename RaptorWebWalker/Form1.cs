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

namespace RaptorWebWalker
{
    public partial class frmMain : Form
    {
        NetComm.Client tcpClient = new Client();
        Utilities utils = new Utilities();

        private string myClientID = "";
        private string pathSettings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"RaptorWebWalker"); // This is the Settings Folder Path
        private string CurrentLogFileName = "";

        private double SecondsPastSinceBoot = 0;
        private double totalRunTime = 0;
        private double PreviousTotalRuntime = 0;


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

            tcpClient.Connect("168.63.37.37", 9119, myClientID); //TODO: Ip Address may need to be more dynamic - shall check
            
        }

    

       

        void tcpClient_errEncounter(Exception ex)
        {
            throw new NotImplementedException();
        }

        void tcpClient_DataReceived(byte[] Data, string ID)
        {
            throw new NotImplementedException();
        }

        void tcpClient_Connected()
        {
            throw new NotImplementedException();
        }


        #region Logging
private void Log(string message)
        {
            lblStatus.Text = message;
            if (message.ToLowerInvariant().StartsWith("error"))
            {
                string formattedMessage = DateTime.Now + "\t" + message;
                SaveLog(formattedMessage);
            }

        }

        private void SaveLog(string message)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            string LogFileName = GetLogFileName();

            try
            {                
                fs = new FileStream(Path.Combine(pathSettings,LogFileName),FileMode.OpenOrCreate,FileAccess.Write, FileShare.None);
                sw = new StreamWriter(fs);

                sw.WriteLine(message);

                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
                throw;
            }
        }

        private string GetLogFileName()
        {
            DateTime dt = DateTime.Now;
            if (CurrentLogFileName.Contains(dt.DayOfWeek.ToString() + dt.Month.ToString() + dt.Year.ToString() + ".rww"))
                return CurrentLogFileName;
            else
            {
                CurrentLogFileName = dt.DayOfWeek.ToString() + dt.Month.ToString() + dt.Year.ToString() + ".rww";
                return CurrentLogFileName;
            }
        }
        #endregion        

        #region Save and Load Settings

        private void LoadSettings()
        {
            FileStream fs = null;
            StreamReader sr = null;

            try
            {
                fs = new FileStream(Path.Combine(pathSettings, "settings.rww"), FileMode.Open, FileAccess.Read, FileShare.None);
                sr = new StreamReader(fs);

                myClientID = sr.ReadLine();
                if (!Double.TryParse(sr.ReadLine(), out PreviousTotalRuntime))
                    PreviousTotalRuntime = 0;
                      

                sr.Close();
                fs.Close();
            }
            catch (Exception)
            {
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();
                throw;
            }
        }
        private void SaveSettings()
        {
            FileStream fs = null;
            StreamWriter sw = null;

            try
            {
                fs = new FileStream(Path.Combine(pathSettings, "settings.rww"), FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                sw = new StreamWriter(fs);

                sw.WriteLine(myClientID);
                sw.WriteLine(totalRunTime);

                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
                throw;
            }
        }

        #endregion

        #region First Run Operations

        private void SetupClient()
        {
            Directory.CreateDirectory(pathSettings); // We know it does not exist we just checked!
            myClientID = CreateClientID(); // Create a Unique ID for this client
            SaveSettings();
        }

        private string CreateClientID()
        {
            return new Guid().ToString();
        }

        private bool FirstRun()
        {
            // If the settings folder is missing then we need to Set the Client up
           return !Directory.Exists(pathSettings); // If the folder exists return FALSE - THIS IS NOT the first run!           
        }

        #endregion

        private void timerSeconds_Tick(object sender, EventArgs e)
        {
            SecondsPastSinceBoot++;
            totalRunTime = SecondsPastSinceBoot + PreviousTotalRuntime;
            lblRuntimeSinceLastBoot.Text = utils.SecondsToDHMS(SecondsPastSinceBoot);
            lblTotalRuntime.Text = utils.SecondsToDHMS(totalRunTime);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            tcpClient.Disconnect();
            SaveSettings();
        }

    }
}
