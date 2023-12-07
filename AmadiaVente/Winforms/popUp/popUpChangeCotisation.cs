using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace AmadiaVente.Winforms.popUp
{
    public partial class popUpChangeCotisation : Form
    {
        //Declaration globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");

        private bool isDragging = false;
        private Point lastCursorPos;
        private Point lastFormPos;

        //Constructeur
        public popUpChangeCotisation()
        {
            InitializeComponent();
        }

        //Méthodes
        private string getAmountCot()
        {
            string result = null;
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT * FROM value_cotisation";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = reader.GetString(0);
                        }
                    }
                }
            }
            return result;
        }

        private void changeAmount(string newAmount)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string updateStockQuery = "UPDATE value_cotisation SET somme_coti = @newAmount";

                using (SqliteCommand updateCommand = new SqliteCommand(updateStockQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@newAmount", newAmount);

                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        //Evenements

        private void popUpChangeCotisation_Load(object sender, EventArgs e)
        {
            txtBoxSomme.Text = getAmountCot();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelCotisationChange_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPos = Cursor.Position;
                lastFormPos = this.Location;
            }
        }

        private void panelCotisationChange_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                this.Location = new Point(lastFormPos.X + delta.X, lastFormPos.Y + delta.Y);
            }
        }

        private void panelCotisationChange_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void btnaliderNVsomme_Click(object sender, EventArgs e)
        {
            string newValue = getAmountCot();
            if (txtBoxSomme.Text != string.Empty)
            {
                newValue = txtBoxSomme.Text.ToString();
            }

            DialogResult confirm = MessageBox.Show("Confirmer la modification de la somme de cotisation ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                changeAmount(newValue);
            }
        }
    }
}
