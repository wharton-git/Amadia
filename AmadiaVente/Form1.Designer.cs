namespace AmadiaVente
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            txtBoxUsername = new Guna.UI2.WinForms.Guna2TextBox();
            txtBoxPassword = new Guna.UI2.WinForms.Guna2TextBox();
            btnConnect = new Guna.UI2.WinForms.Guna2GradientButton();
            guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            guna2GradientPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtBoxUsername
            // 
            txtBoxUsername.Anchor = AnchorStyles.None;
            txtBoxUsername.BorderRadius = 15;
            txtBoxUsername.CustomizableEdges = customizableEdges9;
            txtBoxUsername.DefaultText = "";
            txtBoxUsername.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtBoxUsername.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtBoxUsername.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtBoxUsername.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtBoxUsername.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtBoxUsername.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtBoxUsername.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtBoxUsername.Location = new Point(355, 168);
            txtBoxUsername.Name = "txtBoxUsername";
            txtBoxUsername.PasswordChar = '\0';
            txtBoxUsername.PlaceholderText = "Username";
            txtBoxUsername.SelectedText = "";
            txtBoxUsername.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txtBoxUsername.Size = new Size(200, 36);
            txtBoxUsername.TabIndex = 0;
            // 
            // txtBoxPassword
            // 
            txtBoxPassword.Anchor = AnchorStyles.None;
            txtBoxPassword.BorderRadius = 15;
            txtBoxPassword.CustomizableEdges = customizableEdges11;
            txtBoxPassword.DefaultText = "";
            txtBoxPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtBoxPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtBoxPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtBoxPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtBoxPassword.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtBoxPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtBoxPassword.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtBoxPassword.Location = new Point(355, 222);
            txtBoxPassword.Name = "txtBoxPassword";
            txtBoxPassword.PasswordChar = '*';
            txtBoxPassword.PlaceholderText = "Password";
            txtBoxPassword.SelectedText = "";
            txtBoxPassword.ShadowDecoration.CustomizableEdges = customizableEdges12;
            txtBoxPassword.Size = new Size(200, 36);
            txtBoxPassword.TabIndex = 1;
            // 
            // btnConnect
            // 
            btnConnect.Anchor = AnchorStyles.None;
            btnConnect.BorderRadius = 15;
            btnConnect.CustomizableEdges = customizableEdges13;
            btnConnect.DisabledState.BorderColor = Color.DarkGray;
            btnConnect.DisabledState.CustomBorderColor = Color.DarkGray;
            btnConnect.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnConnect.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnConnect.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnConnect.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnConnect.ForeColor = Color.White;
            btnConnect.Location = new Point(366, 290);
            btnConnect.Name = "btnConnect";
            btnConnect.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnConnect.Size = new Size(180, 45);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Se Connecter";
            btnConnect.Click += btnConnect_Click;
            // 
            // guna2GradientPanel1
            // 
            guna2GradientPanel1.Controls.Add(btnConnect);
            guna2GradientPanel1.Controls.Add(txtBoxUsername);
            guna2GradientPanel1.Controls.Add(txtBoxPassword);
            guna2GradientPanel1.CustomizableEdges = customizableEdges15;
            guna2GradientPanel1.Dock = DockStyle.Fill;
            guna2GradientPanel1.Location = new Point(0, 0);
            guna2GradientPanel1.Name = "guna2GradientPanel1";
            guna2GradientPanel1.ShadowDecoration.CustomizableEdges = customizableEdges16;
            guna2GradientPanel1.Size = new Size(913, 502);
            guna2GradientPanel1.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(913, 502);
            Controls.Add(guna2GradientPanel1);
            Name = "Form1";
            Text = "Form1";
            guna2GradientPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox txtBoxUsername;
        private Guna.UI2.WinForms.Guna2TextBox txtBoxPassword;
        private Guna.UI2.WinForms.Guna2GradientButton btnConnect;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
    }
}
