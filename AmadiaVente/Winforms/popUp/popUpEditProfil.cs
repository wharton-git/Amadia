using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AmadiaVente.Winforms.popUp
{
    public partial class popUpEditProfil : Form
    {

        private bool isDragging = false;
        private Point lastCursorPos;
        private Point lastFormPos;

        public popUpEditProfil()
        {
            InitializeComponent();

            this.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    isDragging = true;
                    lastCursorPos = Cursor.Position;
                    lastFormPos = this.Location;
                }
            };

            // Gérez l'événement MouseMove pour déplacer le formulaire
            this.MouseMove += (sender, e) =>
            {
                if (isDragging)
                {
                    Point delta = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                    this.Location = new Point(lastFormPos.X + delta.X, lastFormPos.Y + delta.Y);
                }
            };

            // Gérez l'événement MouseUp pour arrêter de déplacer le formulaire
            this.MouseUp += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    isDragging = false;
                }
            };
        }


        private void popUpEditProfil_Load(object sender, EventArgs e)
        {
            panelEditMdp.Visible = false;
        }

        private void labelChangeMdp_MouseEnter(object sender, EventArgs e)
        {
            labelChangeMdp.ForeColor = Color.RoyalBlue;
            labelChangeMdp.Cursor = Cursors.Hand;
        }

        private void labelChangeMdp_MouseLeave(object sender, EventArgs e)
        {
            labelChangeMdp.ForeColor = Color.Black;
            labelChangeMdp.Cursor = Cursors.Default;
        }

        private void labelChangeMdp_Click(object sender, EventArgs e)
        {
            if (!panelEditMdp.Visible)
            {
                panelEditMdp.Visible = true;
                labelChangeMdp.Text = "Annuler modification du mot de passe ?";
            }
            else
            {
                panelEditMdp.Visible = false;
                labelChangeMdp.Text = "Changer le mot de passe ?";
            }
        }

        private void btnQuitPopUp_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
