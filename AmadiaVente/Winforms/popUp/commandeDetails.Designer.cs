namespace AmadiaVente.Winforms.popUp
{
    partial class commandeDetails
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            dataGridViewDetail = new Guna.UI2.WinForms.Guna2DataGridView();
            elipseFormulaire = new Guna.UI2.WinForms.Guna2Elipse(components);
            panelCommande = new Guna.UI2.WinForms.Guna2GradientPanel();
            labelIdCommande = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            labelPrixDetail = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDetail).BeginInit();
            panelCommande.SuspendLayout();
            SuspendLayout();
            // 
            // guna2ImageButton1
            // 
            guna2ImageButton1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2ImageButton1.BackColor = Color.Transparent;
            guna2ImageButton1.CheckedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.HoverState.ImageSize = new Size(22, 22);
            guna2ImageButton1.Image = Properties.Resources.icons8_fermer_la_fenêtre_96;
            guna2ImageButton1.ImageOffset = new Point(0, 0);
            guna2ImageButton1.ImageRotate = 0F;
            guna2ImageButton1.ImageSize = new Size(30, 30);
            guna2ImageButton1.Location = new Point(415, 12);
            guna2ImageButton1.Name = "guna2ImageButton1";
            guna2ImageButton1.PressedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.ShadowDecoration.CustomizableEdges = customizableEdges1;
            guna2ImageButton1.Size = new Size(25, 24);
            guna2ImageButton1.TabIndex = 1;
            guna2ImageButton1.Click += guna2ImageButton1_Click;
            // 
            // dataGridViewDetail
            // 
            dataGridViewDetail.AllowUserToAddRows = false;
            dataGridViewDetail.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewDetail.ColumnHeadersHeight = 20;
            dataGridViewDetail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewDetail.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewDetail.GridColor = Color.FromArgb(231, 229, 255);
            dataGridViewDetail.Location = new Point(12, 130);
            dataGridViewDetail.Name = "dataGridViewDetail";
            dataGridViewDetail.ReadOnly = true;
            dataGridViewDetail.RowHeadersVisible = false;
            dataGridViewDetail.RowTemplate.Height = 25;
            dataGridViewDetail.Size = new Size(428, 360);
            dataGridViewDetail.TabIndex = 2;
            dataGridViewDetail.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dataGridViewDetail.ThemeStyle.AlternatingRowsStyle.Font = null;
            dataGridViewDetail.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dataGridViewDetail.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dataGridViewDetail.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dataGridViewDetail.ThemeStyle.BackColor = Color.White;
            dataGridViewDetail.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dataGridViewDetail.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewDetail.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewDetail.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewDetail.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dataGridViewDetail.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewDetail.ThemeStyle.HeaderStyle.Height = 20;
            dataGridViewDetail.ThemeStyle.ReadOnly = true;
            dataGridViewDetail.ThemeStyle.RowsStyle.BackColor = Color.White;
            dataGridViewDetail.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewDetail.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewDetail.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewDetail.ThemeStyle.RowsStyle.Height = 25;
            dataGridViewDetail.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewDetail.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // elipseFormulaire
            // 
            elipseFormulaire.BorderRadius = 15;
            elipseFormulaire.TargetControl = this;
            // 
            // panelCommande
            // 
            panelCommande.BorderColor = Color.FromArgb(23, 117, 197);
            panelCommande.BorderRadius = 5;
            panelCommande.BorderThickness = 4;
            panelCommande.Controls.Add(labelPrixDetail);
            panelCommande.Controls.Add(guna2HtmlLabel2);
            panelCommande.Controls.Add(dataGridViewDetail);
            panelCommande.Controls.Add(labelIdCommande);
            panelCommande.Controls.Add(guna2HtmlLabel1);
            panelCommande.Controls.Add(guna2ImageButton1);
            panelCommande.CustomizableEdges = customizableEdges2;
            panelCommande.Dock = DockStyle.Fill;
            panelCommande.Location = new Point(0, 0);
            panelCommande.Name = "panelCommande";
            panelCommande.ShadowDecoration.CustomizableEdges = customizableEdges3;
            panelCommande.Size = new Size(452, 529);
            panelCommande.TabIndex = 3;
            panelCommande.MouseDown += panelCommande_MouseDown;
            panelCommande.MouseMove += panelCommande_MouseMove;
            panelCommande.MouseUp += panelCommande_MouseUp;
            // 
            // labelIdCommande
            // 
            labelIdCommande.BackColor = Color.Transparent;
            labelIdCommande.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelIdCommande.Location = new Point(308, 37);
            labelIdCommande.Name = "labelIdCommande";
            labelIdCommande.Size = new Size(29, 34);
            labelIdCommande.TabIndex = 3;
            labelIdCommande.Text = "ID";
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            guna2HtmlLabel1.Location = new Point(12, 37);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(290, 34);
            guna2HtmlLabel1.TabIndex = 2;
            guna2HtmlLabel1.Text = "Détail du commande N° :";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Location = new Point(12, 107);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(58, 17);
            guna2HtmlLabel2.TabIndex = 4;
            guna2HtmlLabel2.Text = "Total Prix :";
            // 
            // labelPrixDetail
            // 
            labelPrixDetail.BackColor = Color.Transparent;
            labelPrixDetail.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelPrixDetail.Location = new Point(76, 94);
            labelPrixDetail.Name = "labelPrixDetail";
            labelPrixDetail.Size = new Size(51, 34);
            labelPrixDetail.TabIndex = 5;
            labelPrixDetail.Text = "0 Ar";
            // 
            // commandeDetails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(452, 529);
            Controls.Add(panelCommande);
            FormBorderStyle = FormBorderStyle.None;
            Name = "commandeDetails";
            StartPosition = FormStartPosition.CenterParent;
            Text = "commandeDetails";
            Load += commandeDetails_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewDetail).EndInit();
            panelCommande.ResumeLayout(false);
            panelCommande.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private Guna.UI2.WinForms.Guna2DataGridView dataGridViewDetail;
        private Guna.UI2.WinForms.Guna2Elipse elipseFormulaire;
        private Guna.UI2.WinForms.Guna2GradientPanel panelCommande;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelIdCommande;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelPrixDetail;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
    }
}