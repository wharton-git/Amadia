using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmadiaVente.Winforms.functionality;
using Microsoft.Data.Sqlite;

namespace AmadiaVente.Winforms.popUp
{
    public partial class popUpAddList : Form
    {
        //Declaration globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "../../../database.db");

        private bool isDragging = false;
        private Point lastCursorPos;
        private Point lastFormPos;

        String type = "Médicaments";

        //Construteur
        public popUpAddList()
        {
            InitializeComponent();
        }
        //Methodes
        private void reset()
        {
            type = "Médicaments";
            txtBoxNomArticle.Text = string.Empty;
            radioEquipement.Checked = false;
            radioMedicament.Checked = true;
        }

        private void addArticle(string designation, string type)
        {
            int prix = 0;
            int nbrStock = 0;
            
            try
            {
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    connection.Open();
                    string query = "INSERT INTO article (designation, prix_article, type_article, nbr_stock) VALUES(@designation, @prixArticle, @typeArticle, @nbrStock)";

                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@designation", designation);
                        command.Parameters.AddWithValue("@prixArticle", prix);
                        command.Parameters.AddWithValue("@typeArticle", type);
                        command.Parameters.AddWithValue("@nbrStock", nbrStock);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Ajouté", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur :" + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Evenements
        private void btnQuitList_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelAddList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPos = Cursor.Position;
                lastFormPos = this.Location;
            }
        }

        private void panelAddList_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                this.Location = new Point(lastFormPos.X + delta.X, lastFormPos.Y + delta.Y);
            }
        }

        private void panelAddList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void radioMedicament_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMedicament.Checked)
            {
                radioEquipement.Checked = false;
                type = "Médicaments";
            }
        }

        private void radioEquipement_CheckedChanged(object sender, EventArgs e)
        {
            if (radioEquipement.Checked)
            {
                radioMedicament.Checked = false;
                type = "Equipements";
            }
        }

        private void popUpAddList_Load(object sender, EventArgs e)
        {
            radioMedicament.Checked = true;
        }

        private void btnAddList_Click(object sender, EventArgs e)
        {
            String des = txtBoxNomArticle.Text;
            String nomArticle = des.Trim();
            if (!string.IsNullOrEmpty(nomArticle))
            {
                addArticle(nomArticle, type);
            }
            else
            {
                MessageBox.Show("Veuillez préciser le nom de l'article", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            reset();
        }
    }
}
