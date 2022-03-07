using System;
using System.Collections.Generic;
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

namespace DiplomIshi
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Temizle()
        {
            herShid.Text = "";
            herShidArtm.Text = "";

            nFaktTextBox.Text = "";
            nHesabiTextBox.Text = "";
            sonDereceTextBlock.Text = "";

            nFaktTextBox.IsEnabled = false;
            nHesabiTextBox.IsEnabled = false;
            sonDereceTextBlock.IsEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Temizle();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Hesabla(herShid.Text, herShidArtm.Text);
        }

        private void Hesabla(string shiddet, string artim)
        {
            string sonNeticeStr = "";
            List<int> hereketShid_Int = new List<int>();
            string[] hereketShidStr = shiddet.Split();
            double hereketShidArtm_Double = (Convert.ToDouble(artim) / 100) + 1;
            try
            {
                foreach (string shd in hereketShidStr)
                {
                    hereketShid_Int.Add(Convert.ToInt32(shd));
                    if (sonNeticeStr == "")
                    {
                        sonNeticeStr += shd;
                    }
                    else
                    {
                        sonNeticeStr = sonNeticeStr + " + " + shd;
                    }

                }

                string hereketShidArtm = artim;

                double hereketShid_Sum = hereketShid_Int.Sum();

                double nHesabi = hereketShid_Sum * Math.Pow(hereketShidArtm_Double, 19);

                string nFaktikiStr = $"{sonNeticeStr} = {Convert.ToString(hereketShid_Sum)}";
                string nHesabiStr = $"{Convert.ToString(hereketShid_Sum)} x (1 + {hereketShidArtm}/100)^19 = {nHesabi:F2}";

                string derece;
                string suret;
                if (nHesabi > 7000)
                {
                    derece = "I";
                    suret = "150";
                }
                else if (nHesabi < 7000 && nHesabi > 3000)
                {
                    derece = "II";
                    suret = "120";
                }
                else if (nHesabi < 3000 && nHesabi > 1000)
                {
                    derece = "III";
                    suret = "100";
                }
                else if (nHesabi < 1000 && nHesabi > 100)
                {
                    derece = "IV";
                    suret = "80";
                }
                else
                {
                    derece = "V";
                    suret = "60";
                }

                nHesabiTextBox.Text = nHesabiStr;
                nFaktTextBox.Text = nFaktikiStr;
                sonDereceTextBlock.Text = $"Deməli, layihələndirilən yol {derece} dərəcəli yoldur və əsas sürət {suret} km/saat qəbul edilir";

                nFaktTextBox.IsEnabled = true;
                nHesabiTextBox.IsEnabled = true;
                sonDereceTextBlock.IsEnabled = true;

            }
            catch 
            {
                MessageBox.Show("Daxil etdiyiniz məlumatlar düzgün deyil!", "Xəta!", MessageBoxButton.OK, MessageBoxImage.Error);
                Temizle();
            }

        }
    }
}
