namespace AmadiaVente
{
    public partial class Form1 : Form
    {
        //Déclaration Globale
        private Size originalSize;
        
        //Méthodes (Fonctions)
        public Form1()
        {
            InitializeComponent();
            originalSize = this.Size;
        }

        private void connectAction()
        {
            Winforms.main mainWin = new Winforms.main();
            mainWin.StartPosition = FormStartPosition.Manual;
            mainWin.Location = this.Location;
            mainWin.Size = this.Size;
            if (this.WindowState == FormWindowState.Maximized)
            {
                mainWin.WindowState = FormWindowState.Maximized;
                mainWin.Size = originalSize;
            }
            else
            {
                mainWin.Size = this.Size;
            }
            mainWin.FormClosed += (s, args) => Application.Exit();
            mainWin.Show();
            this.Hide();
        }

        //Evénement

        private void btnConnect_Click(object sender, EventArgs e)
        {
            connectAction();
        }
    }
}
