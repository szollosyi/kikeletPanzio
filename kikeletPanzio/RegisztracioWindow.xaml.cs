using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace kikeletPanzio
{
    public partial class RegisztracioWindow : Window
    {
        public RegisztracioWindow()
        {
            InitializeComponent();
        }

        private void btnRegisztracio_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNev.Text))
            {
                MessageBox.Show("Kérem adja meg az ügyfél nevét.");
                return;
            }

            if (dpSzuletesiDatum.SelectedDate == null)
            {
                MessageBox.Show("Kérem adja meg az ügyfél születési dátumát.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Kérem adja meg az ügyfél e-mail címét.");
                return;
            }

            string azonosito = txtNev.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            bool vip = chkVIP.IsChecked ?? false;
            string vipUzenet = vip ? "VIP ügyfél" : "Nem VIP ügyfél";

            Ugyfel ugyfel = new Ugyfel(txtNev.Text, dpSzuletesiDatum.SelectedDate.Value, txtEmail.Text, vip);

            string filePath = "ugyfelek.csv";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{ugyfel.Nev};{ugyfel.SzuletesiDatum.ToShortDateString()};{ugyfel.Email};{ugyfel.VIP}");
            }

            MessageBox.Show($"Ügyfél regisztráció sikeres!\nAzonosító: {azonosito}\n{vipUzenet}");
            this.Close();
        }
    }
}