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
        public popUpEditProfil()
        {
            InitializeComponent();
        }

        private void popUpEditProfil_Load(object sender, EventArgs e)
        {
            panelEditMdp.Visible = false;
        }

        private void btnQuitPopUp_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnQuitPopUp_MouseEnter(object sender, EventArgs e)
        {
            btnQuitPopUp.Cursor = Cursors.Hand;
            btnQuitPopUp.ForeColor = Color.Red;
        }

        private void btnQuitPopUp_MouseLeave(object sender, EventArgs e)
        {
            btnQuitPopUp.Cursor = Cursors.Default;
            btnQuitPopUp.ForeColor = Color.Black;
        }
    }
}
