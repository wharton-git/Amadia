using AmadiaVente.Winforms.popUp;
using System.Data.SQLite;

namespace AmadiaVente
{
    public partial class Form1 : Form
    {
        //Déclaration Globale
        private Size originalSize;
        string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");
        private string username;
        private string password;
        public string sessionNom;
        public string sessionPrenom;
        public string sessionFunction;
        public string sessionId;

        //Méthodes (Fonctions)
        public Form1()
        {
            InitializeComponent();
            originalSize = this.Size;
            txtBoxPassword.UseSystemPasswordChar = true;
        }

        public string[] VerifierLogin(string username, string password)
        {
            //string connectionString = "Data Source=nom_de_votre_base_de_donnees.db";
            using (SQLiteConnection connection = new SQLiteConnection(cs))
            {
                connection.Open();

                string query = "SELECT id_user, nom_user, prenom_user, fonction_user FROM user WHERE username = @username AND password = @password";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            reader.Read();

                            int idUser = reader.GetInt32(0);
                            string nomUser = reader.GetString(1);
                            string prenomUser = reader.GetString(2);
                            string fonctionUser = reader.GetString(3);

                            return new string[] { nomUser, prenomUser, fonctionUser, idUser.ToString() };
                        }
                    }
                }
            }

            return null;
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
            if (txtBoxUsername.Text != string.Empty && txtBoxPassword.Text != string.Empty)
            {
                username = txtBoxUsername.Text;
                password = txtBoxPassword.Text;

                string[] connection = VerifierLogin(username, password);
                if (connection != null)
                {
                    sessionNom = connection[0];
                    sessionPrenom = connection[1];
                    sessionFunction = connection[2];
                    sessionId = connection[3];
                    Classes.Storage.SessionId = sessionId;
                    connectAction();
                }
                else
                {
                    MessageBox.Show("Login ou Mot de Pass Incorrect !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir correctement les champs !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBoxUsername.Text != string.Empty && txtBoxPassword.Text != string.Empty)
                {
                    username = txtBoxUsername.Text;
                    password = txtBoxPassword.Text;

                    string[] connection = VerifierLogin(username, password);
                    if (connection != null)
                    {
                        sessionNom = connection[0];
                        sessionPrenom = connection[1];
                        sessionFunction = connection[2];
                        sessionId = connection[3];
                        Classes.Storage.SessionId = sessionId;
                        connectAction();
                    }
                    else
                    {
                        MessageBox.Show("Login ou Mot de Pass Incorrect !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez remplir correctement les champs !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtBoxPassword.UseSystemPasswordChar = true;
        }

        private void checkBoxAfficheMdp_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxPassword.UseSystemPasswordChar = !checkBoxAfficheMdp.Checked;
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            popUpNewAccount popUp = new popUpNewAccount();

            popUp.ShowDialog();
            popUp.Dispose();
        }
    }
}
