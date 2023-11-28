namespace AmadiaVente.Winforms.popUp
{
    partial class popUpRendu
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelPopUpRendu = new Guna.UI2.WinForms.Guna2GradientPanel();
            btnQuitList = new Guna.UI2.WinForms.Guna2ImageButton();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            btnSuivant = new Guna.UI2.WinForms.Guna2Button();
            btnRetour = new Guna.UI2.WinForms.Guna2Button();
            btnGenererPdf = new Guna.UI2.WinForms.Guna2Button();
            panelPopUpRendu.SuspendLayout();
            SuspendLayout();
            // 
            // panelPopUpRendu
            // 
            panelPopUpRendu.BackColor = Color.White;
            panelPopUpRendu.BorderColor = Color.FromArgb(23, 117, 197);
            panelPopUpRendu.BorderRadius = 5;
            panelPopUpRendu.BorderThickness = 4;
            panelPopUpRendu.Controls.Add(btnGenererPdf);
            panelPopUpRendu.Controls.Add(btnRetour);
            panelPopUpRendu.Controls.Add(btnSuivant);
            panelPopUpRendu.Controls.Add(guna2Panel1);
            panelPopUpRendu.Controls.Add(btnQuitList);
            panelPopUpRendu.CustomizableEdges = customizableEdges10;
            panelPopUpRendu.Dock = DockStyle.Fill;
            panelPopUpRendu.Location = new Point(0, 0);
            panelPopUpRendu.Name = "panelPopUpRendu";
            panelPopUpRendu.ShadowDecoration.CustomizableEdges = customizableEdges11;
            panelPopUpRendu.Size = new Size(897, 464);
            panelPopUpRendu.TabIndex = 0;
            panelPopUpRendu.MouseDown += panelPopUpRendu_MouseDown;
            panelPopUpRendu.MouseMove += panelPopUpRendu_MouseMove;
            panelPopUpRendu.MouseUp += panelPopUpRendu_MouseUp;
            // 
            // btnQuitList
            // 
            btnQuitList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuitList.CheckedState.ImageSize = new Size(64, 64);
            btnQuitList.HoverState.ImageSize = new Size(64, 64);
            btnQuitList.Image = Properties.Resources.icons8_fermer_la_fenêtre_96;
            btnQuitList.ImageOffset = new Point(0, 0);
            btnQuitList.ImageRotate = 0F;
            btnQuitList.ImageSize = new Size(30, 30);
            btnQuitList.Location = new Point(858, 12);
            btnQuitList.Name = "btnQuitList";
            btnQuitList.PressedState.ImageSize = new Size(64, 64);
            btnQuitList.ShadowDecoration.CustomizableEdges = customizableEdges9;
            btnQuitList.Size = new Size(27, 26);
            btnQuitList.TabIndex = 1;
            btnQuitList.Click += btnQuitList_Click;
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 15;
            guna2Elipse1.TargetControl = this;
            // 
            // guna2Panel1
            // 
            guna2Panel1.CustomizableEdges = customizableEdges7;
            guna2Panel1.Location = new Point(51, 44);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2Panel1.Size = new Size(793, 267);
            guna2Panel1.TabIndex = 2;
            // 
            // btnSuivant
            // 
            btnSuivant.CustomizableEdges = customizableEdges5;
            btnSuivant.DisabledState.BorderColor = Color.DarkGray;
            btnSuivant.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSuivant.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSuivant.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSuivant.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnSuivant.ForeColor = Color.White;
            btnSuivant.Location = new Point(653, 317);
            btnSuivant.Name = "btnSuivant";
            btnSuivant.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnSuivant.Size = new Size(180, 45);
            btnSuivant.TabIndex = 3;
            btnSuivant.Text = "Suivant";
            btnSuivant.Click += btnSuivant_Click;
            // 
            // btnRetour
            // 
            btnRetour.CustomizableEdges = customizableEdges3;
            btnRetour.DisabledState.BorderColor = Color.DarkGray;
            btnRetour.DisabledState.CustomBorderColor = Color.DarkGray;
            btnRetour.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnRetour.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnRetour.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnRetour.ForeColor = Color.White;
            btnRetour.Location = new Point(451, 317);
            btnRetour.Name = "btnRetour";
            btnRetour.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRetour.Size = new Size(180, 45);
            btnRetour.TabIndex = 4;
            btnRetour.Text = "retour";
            btnRetour.Click += btnRetour_Click;
            // 
            // btnGenererPdf
            // 
            btnGenererPdf.CustomizableEdges = customizableEdges1;
            btnGenererPdf.DisabledState.BorderColor = Color.DarkGray;
            btnGenererPdf.DisabledState.CustomBorderColor = Color.DarkGray;
            btnGenererPdf.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnGenererPdf.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnGenererPdf.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnGenererPdf.ForeColor = Color.White;
            btnGenererPdf.Location = new Point(682, 391);
            btnGenererPdf.Name = "btnGenererPdf";
            btnGenererPdf.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnGenererPdf.Size = new Size(180, 45);
            btnGenererPdf.TabIndex = 5;
            btnGenererPdf.Text = "PDF";
            btnGenererPdf.Click += btnGenererPdf_Click;
            // 
            // popUpRendu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(897, 464);
            Controls.Add(panelPopUpRendu);
            FormBorderStyle = FormBorderStyle.None;
            Name = "popUpRendu";
            StartPosition = FormStartPosition.CenterParent;
            Text = "popUpRendu";
            Load += popUpRendu_Load;
            panelPopUpRendu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel panelPopUpRendu;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2ImageButton btnQuitList;
        private Guna.UI2.WinForms.Guna2Button btnGenererPdf;
        private Guna.UI2.WinForms.Guna2Button btnRetour;
        private Guna.UI2.WinForms.Guna2Button btnSuivant;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}