﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaptorWebWalker.forms
{
    public partial class frmLoginRegister : Form
    {

        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public bool IsRegistering { get; set; }

        public frmLoginRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            btnRegister.Visible = false;
            lblConfirmPassword.Visible = true;
            tbConfirmPassword.Visible = true;

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidEmail(tbEmailAddress.Text))
            {
                MessageBox.Show("That does not appear to be a valid email address.");
            }
            else
            {

                if (btnRegister.Visible)
                {
                    IsRegistering = false;
                    EmailAddress = tbConfirmPassword.Text;
                    Password = tbPassword.Text;
                    this.Close();
                }
                else // This person is registering for the first time
                {
                    IsRegistering = true;
                    if (tbPassword.Text == tbConfirmPassword.Text)
                    {
                        EmailAddress = tbConfirmPassword.Text;
                        Password = tbPassword.Text;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password do not match");
                    }
                }
            }
        }

        private bool ValidEmail(string p)
        {
            if (p.Contains('@'))
            {
                if (p.Contains('.'))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
