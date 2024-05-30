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
        private Dictionary<int, int> szobaFerohelyMapping = new Dictionary<int, int>
        {
            { 1, 2 },
            { 2, 3 },
            { 3, 4 },
            { 4, 2 },
            { 5, 3 },
            { 6, 4 }
        };

        public MainWindow()
        {
            InitializeComponent();
            LoadUgyfelek();
            RefreshComboBox();
        }

        private void LoadUgyfelek()
        {
            if (File.Exists("ugyfelek.csv"))
            {
                using (StreamReader sr = new StreamReader("ugyfelek.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] data = sr.ReadLine().Split(';');
                        ugyfelek.Add(new Ugyfel(data[0], DateTime.Parse(data[1]), data[2], bool.Parse(data[3])));
                    }
                }
            }
        }

        private void RefreshComboBox()
        {
            cbxUgyfel.ItemsSource = ugyfelek;
        }

        private void cbxSzobaszam_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbxSzobaszam.SelectedItem != null)
            {
                int selectedSzobaszam = int.Parse(((System.Windows.Controls.ComboBoxItem)cbxSzobaszam.SelectedItem).Content.ToString());
                if (szobaFerohelyMapping.TryGetValue(selectedSzobaszam, out int ferohely))
                {
                    cbxFerohely.SelectedIndex = ferohely - 2;
                    int ar = ferohely * 6000;
                    tbxAr.Text = ar.ToString() + " Ft";
                }
            }
        }

        private void btnUgyfelRegisztracio_Click(object sender, RoutedEventArgs e)
        {
            RegisztracioWindow regisztracioAblak = new RegisztracioWindow();
            regisztracioAblak.ShowDialog();
            LoadUgyfelek();
            RefreshComboBox();
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            if (cbxSzobaszam.SelectedItem == null || cbxUgyfel.SelectedItem == null || dtpKezdet.SelectedDate == null || dtpVege.SelectedDate == null)
            {
                MessageBox.Show("Kérem válassza ki az összes mezőt.");
                return;
            }

            try
            {
                int szobaszam = int.Parse(((System.Windows.Controls.ComboBoxItem)cbxSzobaszam.SelectedItem).Content.ToString());
                int ferohely = szobaFerohelyMapping[szobaszam];
                int ar = int.Parse(tbxAr.Text.Split(' ')[0]);
                Ugyfel selectedUgyfel = (Ugyfel)cbxUgyfel.SelectedItem;
                DateTime foglalasKezdete = dtpKezdet.SelectedDate.Value;
                DateTime foglalasVege = dtpVege.SelectedDate.Value;

                Szoba ujSzoba = new Szoba(szobaszam, ferohely, ar, selectedUgyfel, foglalasKezdete, foglalasVege);
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
