namespace AmadiaVente.Winforms.functionality
{
    partial class profil
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            labelProfilNom = new Guna.UI2.WinForms.Guna2HtmlLabel();
            labelProfilPrenom = new Guna.UI2.WinForms.Guna2HtmlLabel();
            labelProfilUsername = new Guna.UI2.WinForms.Guna2HtmlLabel();
            labelProfilId = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnEditProfil = new Guna.UI2.WinForms.Guna2GradientButton();
            SuspendLayout();
            // 
            // labelProfilNom
            // 
            labelProfilNom.BackColor = Color.Transparent;
            labelProfilNom.Font = new Font("Arial", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelProfilNom.Location = new Point(58, 153);
            labelProfilNom.Name = "labelProfilNom";
            labelProfilNom.Size = new Size(246, 36);
            labelProfilNom.TabIndex = 0;
            labelProfilNom.Text = "guna2HtmlLabel1";
            // 
            // labelProfilPrenom
            // 
            labelProfilPrenom.BackColor = Color.Transparent;
            labelProfilPrenom.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point);
            labelProfilPrenom.Location = new Point(58, 221);
            labelProfilPrenom.Name = "labelProfilPrenom";
            labelProfilPrenom.Size = new Size(193, 29);
            labelProfilPrenom.TabIndex = 1;
            labelProfilPrenom.Text = "guna2HtmlLabel1";
            // 
            // labelProfilUsername
            // 
            labelProfilUsername.BackColor = Color.Transparent;
            labelProfilUsername.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point);
            labelProfilUsername.Location = new Point(58, 94);
            labelProfilUsername.Name = "labelProfilUsername";
            labelProfilUsername.Size = new Size(220, 29);
            labelProfilUsername.TabIndex = 2;
            labelProfilUsername.Text = "labelProfilUsername";
            // 
            // labelProfilId
            // 
            labelProfilId.BackColor = Color.Transparent;
            labelProfilId.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point);
            labelProfilId.Location = new Point(58, 43);
            labelProfilId.Name = "labelProfilId";
            labelProfilId.Size = new Size(193, 29);
            labelProfilId.TabIndex = 3;
            labelProfilId.Text = "guna2HtmlLabel1";
            // 
            // btnEditProfil
            // 
            btnEditProfil.BorderRadius = 10;
            btnEditProfil.CustomizableEdges = customizableEdges1;
            btnEditProfil.DisabledState.BorderColor = Color.DarkGray;
            btnEditProfil.DisabledState.CustomBorderColor = Color.DarkGray;
            btnEditProfil.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnEditProfil.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnEditProfil.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnEditProfil.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnEditProfil.ForeColor = Color.White;
            btnEditProfil.Location = new Point(279, 357);
            btnEditProfil.Name = "btnEditProfil";
            btnEditProfil.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnEditProfil.Size = new Size(180, 45);
            btnEditProfil.TabIndex = 4;
            btnEditProfil.Text = "Editer Mon Profil";
            btnEditProfil.Click += btnEditProfil_Click;
            // 
            // profil
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(893, 478);
            Controls.Add(btnEditProfil);
            Controls.Add(labelProfilId);
            Controls.Add(labelProfilUsername);
            Controls.Add(labelProfilPrenom);
            Controls.Add(labelProfilNom);
            Name = "profil";
            Text = "profil";
            Load += profil_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel labelProfilNom;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelProfilPrenom;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelProfilUsername;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelProfilId;
        private Guna.UI2.WinForms.Guna2GradientButton btnEditProfil;
    }
}