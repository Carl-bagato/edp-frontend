﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void gipbtn_Click(object sender, EventArgs e)
        {
            gipDashboard gipForm = new gipDashboard();
            gipForm.FormClosed += (s, args) => this.Show(); 
            gipForm.Show();
            this.Hide();
        }

        private void spesbtn_Click(object sender, EventArgs e)
        {
            spesDashboard spesForm = new spesDashboard();
            spesForm.FormClosed += (s, args) => this.Show(); 
            spesForm.Show();
            this.Hide();
        }

        private void dilpbtn_Click(object sender, EventArgs e)
        {
            dilpDashboard dilpForm = new dilpDashboard();
            dilpForm.FormClosed += (s, args) => this.Show(); 
            dilpForm.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Application.Exit();

            //loginForm spesForm = new loginForm();
            //spesForm.FormClosed += (s, args) => this.Show();
            //spesForm.Show();
            //this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin_program_bene spesForm = new admin_program_bene();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create a new login form
            loginForm newLogin = new loginForm();

            // Hide this form first to avoid UI flickering
            this.Hide();

            // Show login form
            newLogin.Show();

            // Close this form (after login form is fully shown)
            this.BeginInvoke((Action)(() => this.Close()));
        }
    }
}
