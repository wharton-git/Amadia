using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmadiaVente.Winforms.functionality
{
    public partial class Fournisseur1 : Form
    {
        private int totalPayer;

        public Fournisseur1()
        {
            InitializeComponent();
        }

        private void btnAjoutPanier_Click(object sender, EventArgs e)
        {
            if (Medicament.SelectedItem != null && PrixMedicament.Text != string.Empty && QuantiteMedicament.Text != string.Empty)
            {
                btnValiderAchat.Enabled = true;
                NomFournisseur.Enabled = TypeMedicament.Enabled = false;

                string nomProduit = Medicament.SelectedItem.ToString();
                int quantite = int.Parse(QuantiteMedicament.Text);
                int prixTotal = int.Parse(PrixMedicament.Text);

                dataGridViewPanier.Rows.Add(nomProduit, quantite, prixTotal);

                Medicament.SelectedIndex = -1;
                QuantiteMedicament.Clear();
                PUMedicament.Clear();
                labelStock.Text = "";
                PrixMedicament.Clear();
                totalPayer += prixTotal;
                labelTotal.Text = totalPayer.ToString() + " Ar";
            }
            else
            {
                MessageBox.Show("Veuillez remplir les informations nécessaires !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
 }
