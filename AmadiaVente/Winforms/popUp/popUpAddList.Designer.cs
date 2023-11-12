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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnQuitList = new Guna.UI2.WinForms.Guna2ImageButton();
            panelAddList = new Guna.UI2.WinForms.Guna2GradientPanel();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            SuspendLayout();
            // 
            // btnQuitList
            // 
            btnQuitList.CheckedState.ImageSize = new Size(64, 64);
            btnQuitList.HoverState.ImageSize = new Size(64, 64);
            btnQuitList.Image = Properties.Resources.icons8_fermer_la_fenêtre_96;
            btnQuitList.ImageOffset = new Point(0, 0);
            btnQuitList.ImageRotate = 0F;
            btnQuitList.ImageSize = new Size(30, 30);
            btnQuitList.Location = new Point(416, 12);
            btnQuitList.Name = "btnQuitList";
            btnQuitList.PressedState.ImageSize = new Size(64, 64);
            btnQuitList.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btnQuitList.Size = new Size(27, 26);
            btnQuitList.TabIndex = 0;
            btnQuitList.Click += btnQuitList_Click;
            // 
            // panelAddList
            // 
            panelAddList.BorderColor = Color.FromArgb(23, 117, 197);
            panelAddList.BorderRadius = 6;
            panelAddList.BorderThickness = 4;
            panelAddList.CustomizableEdges = customizableEdges2;
            panelAddList.Dock = DockStyle.Fill;
            panelAddList.Location = new Point(0, 0);
            panelAddList.Name = "panelAddList";
            panelAddList.ShadowDecoration.CustomizableEdges = customizableEdges3;
            panelAddList.Size = new Size(455, 541);
            panelAddList.TabIndex = 1;
            panelAddList.MouseDown += panelAddList_MouseDown;
            panelAddList.MouseMove += panelAddList_MouseMove;
            panelAddList.MouseUp += panelAddList_MouseUp;
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
            ClientSize = new Size(455, 541);
            Controls.Add(btnQuitList);
            Controls.Add(panelAddList);
            FormBorderStyle = FormBorderStyle.None;
            Name = "popUpAddList";
            StartPosition = FormStartPosition.CenterParent;
            Text = "popUpAddList";
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ImageButton btnQuitList;
        private Guna.UI2.WinForms.Guna2GradientPanel panelAddList;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}