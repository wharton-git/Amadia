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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            dataGridViewDetail = new Guna.UI2.WinForms.Guna2DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDetail).BeginInit();
            SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(93, 35);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(97, 17);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "guna2HtmlLabel1";
            // 
            // guna2ImageButton1
            // 
            guna2ImageButton1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2ImageButton1.CheckedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.HoverState.ImageSize = new Size(22, 22);
            guna2ImageButton1.Image = Properties.Resources.icons8_fermer_la_fenêtre_96;
            guna2ImageButton1.ImageOffset = new Point(0, 0);
            guna2ImageButton1.ImageRotate = 0F;
            guna2ImageButton1.ImageSize = new Size(20, 20);
            guna2ImageButton1.Location = new Point(467, 2);
            guna2ImageButton1.Name = "guna2ImageButton1";
            guna2ImageButton1.PressedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.ShadowDecoration.CustomizableEdges = customizableEdges1;
            guna2ImageButton1.Size = new Size(27, 26);
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
            dataGridViewDetail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewDetail.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewDetail.GridColor = Color.FromArgb(231, 229, 255);
            dataGridViewDetail.Location = new Point(12, 92);
            dataGridViewDetail.Name = "dataGridViewDetail";
            dataGridViewDetail.ReadOnly = true;
            dataGridViewDetail.RowHeadersVisible = false;
            dataGridViewDetail.RowTemplate.Height = 25;
            dataGridViewDetail.Size = new Size(472, 425);
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
            dataGridViewDetail.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDetail.ThemeStyle.HeaderStyle.Height = 4;
            dataGridViewDetail.ThemeStyle.ReadOnly = true;
            dataGridViewDetail.ThemeStyle.RowsStyle.BackColor = Color.White;
            dataGridViewDetail.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewDetail.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewDetail.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewDetail.ThemeStyle.RowsStyle.Height = 25;
            dataGridViewDetail.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewDetail.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // commandeDetails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(496, 529);
            Controls.Add(dataGridViewDetail);
            Controls.Add(guna2ImageButton1);
            Controls.Add(guna2HtmlLabel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "commandeDetails";
            StartPosition = FormStartPosition.CenterParent;
            Text = "commandeDetails";
            Load += commandeDetails_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewDetail).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private Guna.UI2.WinForms.Guna2DataGridView dataGridViewDetail;
    }
}