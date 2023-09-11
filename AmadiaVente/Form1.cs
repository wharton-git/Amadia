using System.Data.SQLite;

namespace AmadiaVente
{
    public partial class Form1 : Form
    {
        //D�claration Globale
        private Size originalSize;
        string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "../../../database.db");
        private string username;
        private string password;
        public string sessionNom;
        public string sessionPrenom;
        public string sessionFunction;

        //M�thodes (Fonctions)
        public Form1()
        {
            InitializeComponent();
            originalSize = this.Size;
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

                            return new string[] { nomUser, prenomUser, fonctionUser };
                        }
                    }
                }
            }

            return null;
        }

        private void connectAction()
        {
            Winforms.main mainWin = new Winforms.main(sessionNom, sessionPrenom, sessionFunction);
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

        //Ev�nement

        private void btnConnect_Click(object sender, EventArgs e)
        {
            username = txtBoxUsername.Text;
            password = txtBoxPassword.Text;

            string[] connection = VerifierLogin(username, password);
            if (connection != null)
            {
                sessionNom = connection[0];
                sessionPrenom = connection[1];
                sessionFunction = connection[2];
                connectAction();
            }
            else
            {
                MessageBox.Show("Login ou Mot de Pass Incorrect !");
            }
        }
    }
}
