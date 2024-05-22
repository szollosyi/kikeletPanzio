using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace kikeletPanzio
{
    public partial class MainWindow : Window
    {
        private List<Ugyfel> ugyfelek = new List<Ugyfel>();

        public MainWindow()
        {
            InitializeComponent();
            LoadUgyfelek();
            RefreshComboBox();
        }

        private void LoadUgyfelek()
        {
            string filePath = "ugyfelek.csv";
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length == 4)
                        {
                            string nev = parts[0];
                            DateTime szuletesiDatum = DateTime.Parse(parts[1]);
                            string email = parts[2];
                            bool vip = bool.Parse(parts[3]);
                            if (!ugyfelek.Any(u => u.Nev == nev && u.SzuletesiDatum == szuletesiDatum))
                            {
                                ugyfelek.Add(new Ugyfel(nev, szuletesiDatum, email, vip));
                            }
                        }
                    }
                }
            }
        }

        private void RefreshComboBox()
        {
            cbxUgyfel.ItemsSource = null;
            cbxUgyfel.ItemsSource = ugyfelek;
        }

        private void btnUgyfelRegisztracio_Click(object sender, RoutedEventArgs e)
        {
            RegisztracioWindow regisztracioAblak = new RegisztracioWindow();
            regisztracioAblak.ShowDialog();
            LoadUgyfelek();
            RefreshComboBox();
        }

        private void cbxFerohely_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbxFerohely.SelectedItem != null)
            {
                string selectedFerohely = ((System.Windows.Controls.ComboBoxItem)cbxFerohely.SelectedItem).Content.ToString();
                int ferohely = int.Parse(selectedFerohely.Split(' ')[0]);
                int ar = ferohely * 6000;
                tbxAr.Text = ar.ToString() + " Ft";
            }
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            if (cbxSzobaszam.SelectedItem == null || cbxFerohely.SelectedItem == null || cbxUgyfel.SelectedItem == null)
            {
                MessageBox.Show("Kérem válassza ki az összes mezőt.");
                return;
            }

            try
            {
                int szobaszam = int.Parse(((System.Windows.Controls.ComboBoxItem)cbxSzobaszam.SelectedItem).Content.ToString());
                int ferohely = int.Parse(((System.Windows.Controls.ComboBoxItem)cbxFerohely.SelectedItem).Content.ToString().Split(' ')[0]);
                int ar = int.Parse(tbxAr.Text.Split(' ')[0]);
                Ugyfel selectedUgyfel = (Ugyfel)cbxUgyfel.SelectedItem;

                Szoba ujSzoba = new Szoba(szobaszam, ferohely, ar, selectedUgyfel);
                dgrSzobak.Items.Add(ujSzoba);
                MessageBox.Show("Szoba adatai mentve.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a mentés során: {ex.Message}");
            }
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (dgrSzobak.SelectedItem != null)
            {
                dgrSzobak.Items.Remove(dgrSzobak.SelectedItem);
                MessageBox.Show("Az utolsó hozzáadott szoba törölve.");
            }
            else
            {
                MessageBox.Show("Nincs kijelölt elem a törléshez.");
            }
        }
    }
}