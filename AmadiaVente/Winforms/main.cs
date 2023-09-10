using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmadiaVente.Winforms
{
    public partial class main : Form
    {
        //Déclaration Globale
        private Size originalSize;

        //Méthodes (fonctions)
        public main()
        {
            InitializeComponent();
            originalSize = this.Size;
        }

        private void disconnectAction()
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            if (this.WindowState == FormWindowState.Maximized)
            {
                form1.WindowState = FormWindowState.Maximized;
                form1.Size = originalSize;
            }
            else
            {
                form1.Size = this.Size;
            }
            form1.FormClosed += (s, args) => Application.Exit();
            form1.Show();
            this.Hide();
        }

        //Evénements
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            disconnectAction();
        }
    }
}
