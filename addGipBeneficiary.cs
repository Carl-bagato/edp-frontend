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
    public partial class addGipBeneficiary : Form
    {
        public addGipBeneficiary()
        {
            InitializeComponent();

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            gipDashboard dilpForm = new gipDashboard();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }

        private void submitbtn_Click(object sender, EventArgs e)
        {
            gipDashboard dilpForm = new gipDashboard();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
