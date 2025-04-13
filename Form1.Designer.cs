namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.spesbtn = new System.Windows.Forms.Button();
            this.gipbtn = new System.Windows.Forms.Button();
            this.dilpbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Logout = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // spesbtn
            // 
            this.spesbtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spesbtn.Location = new System.Drawing.Point(491, 111);
            this.spesbtn.Name = "spesbtn";
            this.spesbtn.Size = new System.Drawing.Size(253, 64);
            this.spesbtn.TabIndex = 0;
            this.spesbtn.Text = "SPES";
            this.spesbtn.UseVisualStyleBackColor = true;
            this.spesbtn.Click += new System.EventHandler(this.spesbtn_Click);
            // 
            // gipbtn
            // 
            this.gipbtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gipbtn.Location = new System.Drawing.Point(491, 301);
            this.gipbtn.Name = "gipbtn";
            this.gipbtn.Size = new System.Drawing.Size(253, 66);
            this.gipbtn.TabIndex = 1;
            this.gipbtn.Text = "GIP";
            this.gipbtn.UseVisualStyleBackColor = true;
            this.gipbtn.Click += new System.EventHandler(this.gipbtn_Click);
            // 
            // dilpbtn
            // 
            this.dilpbtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dilpbtn.Location = new System.Drawing.Point(491, 208);
            this.dilpbtn.Name = "dilpbtn";
            this.dilpbtn.Size = new System.Drawing.Size(253, 65);
            this.dilpbtn.TabIndex = 2;
            this.dilpbtn.Text = "DILP";
            this.dilpbtn.UseVisualStyleBackColor = true;
            this.dilpbtn.Click += new System.EventHandler(this.dilpbtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(568, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "PESO ALBAY Programs";
            // 
            // Logout
            // 
            this.Logout.Location = new System.Drawing.Point(671, 410);
            this.Logout.Name = "Logout";
            this.Logout.Size = new System.Drawing.Size(73, 23);
            this.Logout.TabIndex = 5;
            this.Logout.Text = "Exit";
            this.Logout.UseVisualStyleBackColor = true;
            this.Logout.Click += new System.EventHandler(this.Logout_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Location = new System.Drawing.Point(2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 458);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(768, 459);
            this.Controls.Add(this.Logout);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dilpbtn);
            this.Controls.Add(this.gipbtn);
            this.Controls.Add(this.spesbtn);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button spesbtn;
        private System.Windows.Forms.Button gipbtn;
        private System.Windows.Forms.Button dilpbtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Logout;
        private System.Windows.Forms.Panel panel1;
    }
}

