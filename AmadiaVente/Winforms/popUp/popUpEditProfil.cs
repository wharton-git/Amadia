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
        }

        private void labelChangeMdp_MouseLeave(object sender, EventArgs e)
        {
            labelChangeMdp.ForeColor = Color.Black;
        }

        private void labelChangeMdp_Click(object sender, EventArgs e)
        {
            panelEditMdp.Visible = true;
        }

    }
}
