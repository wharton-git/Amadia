namespace AmadiaVente.Winforms.popUp
{
    partial class popUpAddList
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnQuitList = new Guna.UI2.WinForms.Guna2ImageButton();
            panelAddList = new Guna.UI2.WinForms.Guna2GradientPanel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnAddList = new Guna.UI2.WinForms.Guna2GradientButton();
            radioEquipement = new Guna.UI2.WinForms.Guna2RadioButton();
            radioMedicament = new Guna.UI2.WinForms.Guna2RadioButton();
            txtBoxNomArticle = new Guna.UI2.WinForms.Guna2TextBox();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            panelAddList.SuspendLayout();
            SuspendLayout();
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
            btnQuitList.Location = new Point(257, 12);
            btnQuitList.Name = "btnQuitList";
            btnQuitList.PressedState.ImageSize = new Size(64, 64);
            btnQuitList.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btnQuitList.Size = new Size(27, 26);
            btnQuitList.TabIndex = 0;
            btnQuitList.Click += btnQuitList_Click;
            // 
            // panelAddList
            // 
            panelAddList.BackColor = Color.White;
            panelAddList.BorderColor = Color.FromArgb(23, 117, 197);
            panelAddList.BorderRadius = 6;
            panelAddList.BorderThickness = 4;
            panelAddList.Controls.Add(guna2HtmlLabel3);
            panelAddList.Controls.Add(btnQuitList);
            panelAddList.Controls.Add(guna2HtmlLabel2);
            panelAddList.Controls.Add(guna2HtmlLabel1);
            panelAddList.Controls.Add(btnAddList);
            panelAddList.Controls.Add(radioEquipement);
            panelAddList.Controls.Add(radioMedicament);
            panelAddList.Controls.Add(txtBoxNomArticle);
            panelAddList.CustomizableEdges = customizableEdges6;
            panelAddList.Dock = DockStyle.Fill;
            panelAddList.Location = new Point(0, 0);
            panelAddList.Name = "panelAddList";
            panelAddList.ShadowDecoration.CustomizableEdges = customizableEdges7;
            panelAddList.Size = new Size(296, 384);
            panelAddList.TabIndex = 1;
            panelAddList.MouseDown += panelAddList_MouseDown;
            panelAddList.MouseMove += panelAddList_MouseMove;
            panelAddList.MouseUp += panelAddList_MouseUp;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            guna2HtmlLabel3.Location = new Point(12, 12);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(193, 39);
            guna2HtmlLabel3.TabIndex = 10;
            guna2HtmlLabel3.Text = "Nouvel Article";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            guna2HtmlLabel2.Location = new Point(23, 170);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(56, 27);
            guna2HtmlLabel2.TabIndex = 9;
            guna2HtmlLabel2.Text = "Type :";
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            guna2HtmlLabel1.Location = new Point(23, 80);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(120, 27);
            guna2HtmlLabel1.TabIndex = 8;
            guna2HtmlLabel1.Text = "Désignation :";
            // 
            // btnAddList
            // 
            btnAddList.BorderRadius = 15;
            btnAddList.CustomizableEdges = customizableEdges2;
            btnAddList.DisabledState.BorderColor = Color.DarkGray;
            btnAddList.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAddList.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAddList.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnAddList.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAddList.FillColor = Color.FromArgb(23, 117, 197);
            btnAddList.FillColor2 = Color.FromArgb(23, 117, 197);
            btnAddList.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnAddList.ForeColor = Color.White;
            btnAddList.Location = new Point(58, 285);
            btnAddList.Name = "btnAddList";
            btnAddList.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnAddList.Size = new Size(180, 45);
            btnAddList.TabIndex = 7;
            btnAddList.Text = "Ajouter l'Article";
            btnAddList.Click += btnAddList_Click;
            // 
            // radioEquipement
            // 
            radioEquipement.Animated = true;
            radioEquipement.AutoSize = true;
            radioEquipement.CheckedState.BorderColor = Color.FromArgb(192, 192, 255);
            radioEquipement.CheckedState.BorderThickness = 0;
            radioEquipement.CheckedState.FillColor = Color.FromArgb(192, 192, 255);
            radioEquipement.CheckedState.InnerColor = Color.MediumBlue;
            radioEquipement.Location = new Point(157, 219);
            radioEquipement.Name = "radioEquipement";
            radioEquipement.Size = new Size(108, 19);
            radioEquipement.TabIndex = 6;
            radioEquipement.Text = "Consommables";
            radioEquipement.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            radioEquipement.UncheckedState.BorderThickness = 2;
            radioEquipement.UncheckedState.FillColor = Color.Transparent;
            radioEquipement.UncheckedState.InnerColor = Color.Transparent;
            radioEquipement.CheckedChanged += radioEquipement_CheckedChanged;
            // 
            // radioMedicament
            // 
            radioMedicament.Animated = true;
            radioMedicament.AutoSize = true;
            radioMedicament.CheckedState.BorderColor = Color.FromArgb(192, 192, 255);
            radioMedicament.CheckedState.BorderThickness = 0;
            radioMedicament.CheckedState.FillColor = Color.FromArgb(192, 192, 255);
            radioMedicament.CheckedState.InnerColor = Color.MediumBlue;
            radioMedicament.Location = new Point(38, 219);
            radioMedicament.Name = "radioMedicament";
            radioMedicament.Size = new Size(97, 19);
            radioMedicament.TabIndex = 5;
            radioMedicament.Text = "Médicaments";
            radioMedicament.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            radioMedicament.UncheckedState.BorderThickness = 2;
            radioMedicament.UncheckedState.FillColor = Color.Transparent;
            radioMedicament.UncheckedState.InnerColor = Color.Transparent;
            radioMedicament.CheckedChanged += radioMedicament_CheckedChanged;
            // 
            // txtBoxNomArticle
            // 
            txtBoxNomArticle.BorderRadius = 15;
            txtBoxNomArticle.CustomizableEdges = customizableEdges4;
            txtBoxNomArticle.DefaultText = "";
            txtBoxNomArticle.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtBoxNomArticle.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtBoxNomArticle.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtBoxNomArticle.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtBoxNomArticle.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtBoxNomArticle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtBoxNomArticle.ForeColor = Color.Black;
            txtBoxNomArticle.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtBoxNomArticle.Location = new Point(48, 113);
            txtBoxNomArticle.Name = "txtBoxNomArticle";
            txtBoxNomArticle.PasswordChar = '\0';
            txtBoxNomArticle.PlaceholderForeColor = Color.Gray;
            txtBoxNomArticle.PlaceholderText = "Article à ajouter";
            txtBoxNomArticle.SelectedText = "";
            txtBoxNomArticle.ShadowDecoration.CustomizableEdges = customizableEdges5;
            txtBoxNomArticle.Size = new Size(200, 36);
            txtBoxNomArticle.TabIndex = 0;
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 15;
            guna2Elipse1.TargetControl = this;
            // 
            // popUpAddList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(296, 384);
            Controls.Add(panelAddList);
            FormBorderStyle = FormBorderStyle.None;
            Name = "popUpAddList";
            StartPosition = FormStartPosition.CenterParent;
            Text = "popUpAddList";
            Load += popUpAddList_Load;
            panelAddList.ResumeLayout(false);
            panelAddList.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ImageButton btnQuitList;
        private Guna.UI2.WinForms.Guna2GradientPanel panelAddList;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2TextBox txtBoxNomArticle;
        private Guna.UI2.WinForms.Guna2RadioButton radioMedicament;
        private Guna.UI2.WinForms.Guna2RadioButton radioEquipement;
        private Guna.UI2.WinForms.Guna2GradientButton btnAddList;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
    }
}