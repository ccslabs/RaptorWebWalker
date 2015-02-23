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

namespace RaptorWebWalker
{
    public partial class frmMain : Form
    {
        NetComm.Client tcpClient = new Client();

        private string myClientID = "";
        private string pathSettings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"RaptorWebWalker"); // This is the Settings Folder Path
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
            return Directory.Exists(pathSettings);
        }

        #endregion

    }
}
