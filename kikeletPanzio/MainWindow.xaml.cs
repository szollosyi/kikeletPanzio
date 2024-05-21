using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace kikeletPanzio
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Szoba> szobak;

        public MainWindow()
        {
            InitializeComponent();
            szobak = new ObservableCollection<Szoba>();
            dgrSzobak.ItemsSource = szobak;
        }

        private void cbxFerohely_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxFerohely.SelectedItem != null)
            {
                string selectedFerohely = ((ComboBoxItem)cbxFerohely.SelectedItem).Content.ToString();
                int ferohely = int.Parse(selectedFerohely.Split(' ')[0]);

                int ar = ferohely * 6000;
                tbxAr.Text = ar.ToString() + " Ft";
            }
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            if (cbxSzobaszam.SelectedItem == null || cbxFerohely.SelectedItem == null)
            {
                MessageBox.Show("Válasszon ki egy szobát és férőhelyet a mentés előtt!");
                return;
            }

            try
            {
                int szobaszam = int.Parse(((ComboBoxItem)cbxSzobaszam.SelectedItem).Content.ToString());
                int ferohely = int.Parse(((ComboBoxItem)cbxFerohely.SelectedItem).Content.ToString().Split(' ')[0]);
                int ar = int.Parse(tbxAr.Text.Split(' ')[0]);

                Szoba ujSzoba = new Szoba(szobaszam, ferohely, ar);
                szobak.Add(ujSzoba);
                MessageBox.Show("Szoba adatai mentve.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a mentés során: {ex.Message}");
            }
        }


        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (szobak.Count > 0)
            {
                szobak.RemoveAt(szobak.Count - 1);
                MessageBox.Show("Az utolsó hozzáadott szoba törölve.");
            }
            else
            {
                MessageBox.Show("Nincs több szoba a listában.");
            }
        }
    }
}
