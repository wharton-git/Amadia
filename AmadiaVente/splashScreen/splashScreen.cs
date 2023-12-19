using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmadiaVente.splashScreen
{
    public partial class splashScreen : Form
    {
        public splashScreen()
        {
            InitializeComponent();
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            timerClose.Stop();
            Form1 mainWIndows = new Form1();
            mainWIndows.Show();
            mainWIndows.FormClosed += (s, args) => Application.Exit();
            this.Hide();
        }

        private void splashScreen_Load(object sender, EventArgs e)
        {
            timerClose.Start();
        }
    }
}
