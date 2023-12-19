using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
namespace AmadiaVente.Winforms.userControles
{
    public partial class uc_recupMdpAskLoginAndRecoveryCode : UserControl
    {
        //Déclaration Globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");

        //Constructeur
        public uc_recupMdpAskLoginAndRecoveryCode()
        {
            InitializeComponent();
        }


        //Evenements
        private void brnCheckMdpRecovery_Click(object sender, EventArgs e)
        {

        }
    }
}
