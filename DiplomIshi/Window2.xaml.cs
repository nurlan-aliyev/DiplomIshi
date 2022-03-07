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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Temizle();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Hesabla(alfaTextBox.Text, radiusTextBox.Text, DBZTextBox.Text);
        }

        private void Hesabla(string alfa, string ERadius, string DBZ)
        {
            try
            {
                double eyriAlfaVerilen = Convert.ToDouble(alfa);
                double eyriRadiusVerilen = Convert.ToDouble(ERadius);
                double eyriDBZVerilen = Convert.ToDouble(DBZ);

                if (eyriRadiusVerilen > 2000)
                {
                    SadeEyriHesab(eyriAlfaVerilen, eyriRadiusVerilen, eyriDBZVerilen);
                }
                else if (eyriRadiusVerilen > 0 && eyriRadiusVerilen <= 2000)
                {
                    VirajliEyriHesab(eyriAlfaVerilen, eyriRadiusVerilen, eyriDBZVerilen);
                }
                else
                {
                    MessageBox.Show("Məlumatları düzgün daxil edin!", "Xəbərdarlıq", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    Temizle();
                }
            }
            catch
            {
                MessageBox.Show("Məlumatları düzgün daxil edin!", "Xəbərdarlıq", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                Temizle();
            }
        }

        private static double Radians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private void Temizle()
        {
            eyriHesabWindow.Width = 520;
            alfaTextBox.Text = "";
            radiusTextBox.Text = "";
            DBZTextBox.Text = "";

            TangensTextBox.Text = "";
            EyriTextBox.Text = "";
            EyriSonuTextBox.Text = "";
            EyriBashTextBox.Text = "";
            DomerTextBox.Text = "";
            BisektrisTextBox.Text = "";

            TangensTextBox.Background = Brushes.Gray;
            EyriTextBox.Background = Brushes.Gray;
            BisektrisTextBox.Background = Brushes.Gray;
            DomerTextBox.Background = Brushes.Gray;
            EyriBashTextBox.Background = Brushes.Gray;
            EyriSonuTextBox.Background = Brushes.Gray;

            TangensLabel.Content = "T = R x tan α/2 = ";
            EyriLabel.Content = "Ə = (π x R x α) / 180 = ";
            BisektrisLabel.Content = "B = R  x (1 / cos(α/ 2) - 1) = ";
            EyriBashLabel.Content = "ƏB = DBZ - T = ";
            EyriSonLabel.Content = "ƏS = ƏB + Ə = ";
        }

        private void SadeEyriHesab(double alfa, double ERadius, double DBZ)
        {
            double eyriAlfaVerilen = alfa;
            double eyriRadiusVerilen = ERadius;
            double eyriDBZVerilen = DBZ;

            double eyriTangens = eyriRadiusVerilen * Math.Tan(Radians(eyriAlfaVerilen / 2));
            double eyriUzunluq = (Math.PI * eyriRadiusVerilen * eyriAlfaVerilen) / 180;
            double eyriBisektris = eyriRadiusVerilen * ((1 / Math.Cos(Radians(eyriAlfaVerilen / 2))) - 1);
            double eyriDomer = 2 * eyriTangens - eyriUzunluq;
            double eyriBashlangic = eyriDBZVerilen - eyriTangens;
            double eyriSonu = eyriBashlangic + eyriUzunluq;

            TangensTextBox.Text = eyriTangens.ToString("F2");
            EyriTextBox.Text = eyriUzunluq.ToString("F2");
            BisektrisTextBox.Text = eyriBisektris.ToString("F2");
            DomerTextBox.Text = eyriDomer.ToString("F2");
            EyriBashTextBox.Text = eyriBashlangic.ToString("F2");
            EyriSonuTextBox.Text = eyriSonu.ToString("F2");

            TangensTextBox.IsEnabled = true;
            EyriTextBox.IsEnabled = true;
            BisektrisTextBox.IsEnabled = true;
            DomerTextBox.IsEnabled = true;
            EyriBashTextBox.IsEnabled = true;
            EyriSonuTextBox.IsEnabled = true;

            TangensTextBox.Background = Brushes.White;
            EyriTextBox.Background = Brushes.White;
            BisektrisTextBox.Background = Brushes.White;
            DomerTextBox.Background = Brushes.White;
            EyriBashTextBox.Background = Brushes.White;
            EyriSonuTextBox.Background = Brushes.White;
        }

        private void VirajliEyriHesab(double alfa, double ERadius, double DBZ)
        {
            double eyriAlfaVerilen = alfa;
            double eyriRadiusVerilen = ERadius;
            double eyriDBZVerilen = DBZ;

            if (kecidEyriCedveli.ContainsKey((int)eyriRadiusVerilen))
            {
                eyriHesabWindow.Width = 560;
                double ro = kecidEyriCedveli[(int)eyriRadiusVerilen]["ro"];
                double m = kecidEyriCedveli[(int)eyriRadiusVerilen]["m"];
                double l = kecidEyriCedveli[(int)eyriRadiusVerilen]["l"];
                double ikiBeta = kecidEyriCedveli[(int)eyriRadiusVerilen]["2beta"];


                double eyriTangens = ((eyriRadiusVerilen + ro) * Math.Tan(Radians(eyriAlfaVerilen / 2))) + m;
                double eyriUzunluq = (Math.PI * eyriRadiusVerilen * (eyriAlfaVerilen - ikiBeta)) / 180 + l*2;
                double eyriBisektris = ((eyriRadiusVerilen + ro) * (1 / Math.Cos(Radians(eyriAlfaVerilen / 2)))) - eyriRadiusVerilen;
                double eyriDomer = 2 * eyriTangens - eyriUzunluq;
                double eyriBashlangic = eyriDBZVerilen - eyriTangens;
                double eyriSonu = eyriBashlangic + eyriUzunluq;

                TangensLabel.Content = "T = (R + ρ) x tan α/2 + m = ";
                EyriLabel.Content = "Ə = (π x R x (α - 2β)) / 180 + 2L = ";
                BisektrisLabel.Content = "B = (R + ρ) x (1 / cos(α/ 2) - R = ";
                EyriBashLabel.Content = "KƏB = DBZ - T = ";
                EyriSonLabel.Content = "KƏS = KƏB + Ə = ";


                TangensTextBox.Text = eyriTangens.ToString("F2");
                EyriTextBox.Text = eyriUzunluq.ToString("F2");
                BisektrisTextBox.Text = eyriBisektris.ToString("F2");
                DomerTextBox.Text = eyriDomer.ToString("F2");
                EyriBashTextBox.Text = eyriBashlangic.ToString("F2");
                EyriSonuTextBox.Text = eyriSonu.ToString("F2");

                TangensTextBox.IsEnabled = true;
                EyriTextBox.IsEnabled = true;
                BisektrisTextBox.IsEnabled = true;
                DomerTextBox.IsEnabled = true;
                EyriBashTextBox.IsEnabled = true;
                EyriSonuTextBox.IsEnabled = true;

                TangensTextBox.Background = Brushes.White;
                EyriTextBox.Background = Brushes.White;
                BisektrisTextBox.Background = Brushes.White;
                DomerTextBox.Background = Brushes.White;
                EyriBashTextBox.Background = Brushes.White;
                EyriSonuTextBox.Background = Brushes.White;
            }
            else
            {
                MessageBox.Show("Məlumatları düzgün daxil edin!", "Xəbərdarlıq", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                Temizle();
            }


        }



        public Dictionary<int, Dictionary<string, double>> kecidEyriCedveli = new()
    {
        {
            30,
            new Dictionary<string, double>
            {
                {"l", 30},
                {"2beta", 57.3},
                {"m", 14.86},
                {"ro", 1.24}
            }
        },

            {
                50,
                new Dictionary<string, double>
            {
                {"l", 35},
                {"2beta", 40.08},
                {"m", 17.43},
                {"ro", 1.02}
            }
            },

            {
                60,
                new Dictionary<string, double>
            {
                {"l", 40},
                {"2beta", 38.2},
                {"m", 19.93},
                {"ro", 1.11}
            }
            },

            {
                80,
                new Dictionary<string, double>
            {
                {"l", 45},
                {"2beta", 40.08},
                {"m", 22.45},
                {"ro", 1.07}
            }
            },

            {
                100,
                new Dictionary<string, double>
            {
                {"l", 50},
                {"2beta", 28.65},
                {"m", 24.95},
                {"ro", 1.08}
            }
            },

            {
                150,
                new Dictionary<string, double>
            {
                {"l", 60},
                {"2beta", 22.92},
                {"m", 29.96},
                {"ro", 1.01}
            }
            },

            {
                200,
                new Dictionary<string, double>
            {
                {"l", 70},
                {"2beta", 20.05},
                {"m", 34.97},
                {"ro", 1.02}
            }
            },

            {
                250,
                new Dictionary<string, double>
            {
                {"l", 80},
                {"2beta", 18.4},
                {"m", 39.97},
                {"ro", 1.07}
            }
            },

            {
                300,
                new Dictionary<string, double>
            {
                {"l", 90},
                {"2beta", 17.18},
                {"m", 44.97},
                {"ro", 1.12}
            }
            },

            {
                400,
                new Dictionary<string, double>
            {
                {"l", 100},
                {"2beta", 14.32},
                {"m", 49.97},
                {"ro", 1.04}
            }
            },

            {
                500,
                new Dictionary<string, double>
            {
                {"l", 110},
                {"2beta", 12.6},
                {"m", 54.98},
                {"ro", 1.01}
            }
            },

            {
                600,
                new Dictionary<string, double>
            {
                {"l", 120},
                {"2beta", 11.47},
                {"m", 59.98},
                {"ro", 1.00}
            }
            },

            {
                700,
                new Dictionary<string, double>
            {
                {"l", 120},
                {"2beta", 9.84},
                {"m", 59.98},
                {"ro", 0.85}
            }
            },

            {
                800,
                new Dictionary<string, double>
            {
                {"l", 120},
                {"2beta", 8.6},
                {"m", 59.99},
                {"ro", 0.75}
            }
            },

            {
                900,
                new Dictionary<string, double>
            {
                {"l", 120},
                {"2beta", 7.64},
                {"m", 59.99},
                {"ro", 0.66}
            }
            },

            {
                1000,
                new Dictionary<string, double>
            {
                {"l", 120},
                {"2beta", 6.87},
                {"m", 59.99},
                {"ro", 0.60}
            }
            },

            {
                1100,
                new Dictionary<string, double>
            {
                {"l", 100},
                {"2beta", 5.2},
                {"m", 50.00},
                {"ro", 0.38}
            }
            },

            {
                1200,
                new Dictionary<string, double>
            {
                {"l", 100},
                {"2beta", 4.77},
                {"m", 50.00},
                {"ro", 0.35}
            }
            },

            {
                1300,
                new Dictionary<string, double>
            {
                {"l", 100},
                {"2beta", 4.4},
                {"m", 50.00},
                {"ro", 0.32}
            }
            },

            {
                1400,
                new Dictionary<string, double>
            {
                {"l", 100},
                {"2beta", 4.1},
                {"m", 50.00},
                {"ro", 0.30}
            }
            },

            {
                1500,
                new Dictionary<string, double>
            {
                {"l", 100},
                {"2beta", 3.82},
                {"m", 50.00},
                {"ro", 0.28}
            }
            },

            {
                1600,
                new Dictionary<string, double>
            {
                {"l", 100},
                {"2beta", 3.58},
                {"m", 50.00},
                {"ro", 0.26}
            }
            },

            {
                1700,
                new Dictionary<string, double>
            {
                {"l", 100},
                {"2beta", 3.37},
                {"m", 50.00},
                {"ro", 0.25}
            }
            },

            {
                1800,
                new Dictionary<string, double>
            {
                {"l", 100},
                {"2beta", 3.18},
                {"m", 50.00},
                {"ro", 0.24}
            }
            },

            {
                1900,
                new Dictionary<string, double>
            {
                {"l", 100},
                {"2beta", 3.02},
                {"m", 50.00},
                {"ro", 0.22}
            }
            },

            {
                2000,
                new Dictionary<string, double>
            {
                {"l", 100},
                {"2beta", 2.87},
                {"m", 50.00},
                {"ro", 0.21}
            }
            },


        };
    }
}
