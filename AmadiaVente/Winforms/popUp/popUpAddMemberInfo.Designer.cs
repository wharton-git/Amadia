namespace AmadiaVente.Winforms.popUp
{
    partial class popUpAddMemberInfo
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(popUpAddMemberInfo));
            guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            btnQuit = new Guna.UI2.WinForms.Guna2ImageButton();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2GradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // guna2GradientPanel1
            // 
            guna2GradientPanel1.BackColor = Color.White;
            guna2GradientPanel1.BorderColor = Color.FromArgb(23, 117, 197);
            guna2GradientPanel1.BorderRadius = 6;
            guna2GradientPanel1.BorderThickness = 4;
            guna2GradientPanel1.Controls.Add(guna2PictureBox1);
            guna2GradientPanel1.Controls.Add(btnQuit);
            guna2GradientPanel1.Controls.Add(guna2HtmlLabel2);
            guna2GradientPanel1.Controls.Add(guna2HtmlLabel1);
            guna2GradientPanel1.CustomizableEdges = customizableEdges4;
            guna2GradientPanel1.Dock = DockStyle.Fill;
            guna2GradientPanel1.Location = new Point(0, 0);
            guna2GradientPanel1.Name = "guna2GradientPanel1";
            guna2GradientPanel1.ShadowDecoration.CustomizableEdges = customizableEdges5;
            guna2GradientPanel1.Size = new Size(760, 111);
            guna2GradientPanel1.TabIndex = 0;
            // 
            // guna2PictureBox1
            // 
            guna2PictureBox1.CustomizableEdges = customizableEdges1;
            guna2PictureBox1.Image = Properties.Resources.icons8_info_310;
            guna2PictureBox1.ImageRotate = 0F;
            guna2PictureBox1.Location = new Point(12, 12);
            guna2PictureBox1.Name = "guna2PictureBox1";
            guna2PictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2PictureBox1.Size = new Size(35, 26);
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            guna2PictureBox1.TabIndex = 5;
            guna2PictureBox1.TabStop = false;
            // 
            // btnQuit
            // 
            btnQuit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuit.CheckedState.ImageSize = new Size(64, 64);
            btnQuit.HoverState.ImageSize = new Size(64, 64);
            btnQuit.Image = Properties.Resources.icons8_fermer_la_fenêtre_96;
            btnQuit.ImageOffset = new Point(0, 0);
            btnQuit.ImageRotate = 0F;
            btnQuit.ImageSize = new Size(30, 30);
            btnQuit.Location = new Point(718, 12);
            btnQuit.Name = "btnQuit";
            btnQuit.PressedState.ImageSize = new Size(64, 64);
            btnQuit.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnQuit.Size = new Size(27, 26);
            btnQuit.TabIndex = 4;
            btnQuit.Click += btnQuit_Click;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            guna2HtmlLabel2.Location = new Point(12, 74);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(688, 22);
            guna2HtmlLabel2.TabIndex = 3;
            guna2HtmlLabel2.Text = "Si le numéro est laissé vide, le membre se verra attribué un numéro par défaut suivant son adhésion.";
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            guna2HtmlLabel1.Location = new Point(12, 42);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(364, 22);
            guna2HtmlLabel1.TabIndex = 2;
            guna2HtmlLabel1.Text = "Le numéro d'un membre peut être défini laisser vide.";
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 15;
            guna2Elipse1.TargetControl = this;
            // 
            // popUpAddMemberInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 111);
            Controls.Add(guna2GradientPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "popUpAddMemberInfo";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Info sur l'ajout d'un membre";
            guna2GradientPanel1.ResumeLayout(false);
            guna2GradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2ImageButton btnQuit;
    }
}