using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace Encryption
{
    /// <summary>
    /// Interaction logic for Decrypteer.xaml
    /// </summary>
    public partial class Decrypteer : Window
    {
        public Decrypteer()
        {
            InitializeComponent();
        }

        private void browseFile()
        {
            OpenFileDialog fd = new OpenFileDialog();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK) // Test result.
            {
                string file = fd.FileName;

                try
                {
                    if (Directory.Exists(path))
                    {
                        fd.InitialDirectory = path;
                    }
                    else {
                        fd.InitialDirectory = @"C:\";
                    }
                    string text = File.ReadAllText(file);
                }
                catch (IOException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_f1_Click(object sender, RoutedEventArgs e)
        {
            browseFile();
        }

        private void btn_f2_Click(object sender, RoutedEventArgs e)
        {
            browseFile();
        }

        private void btn_f3_Click(object sender, RoutedEventArgs e)
        {
            browseFile();
        }

        private void btn_puA_Click(object sender, RoutedEventArgs e)
        {
            browseFile();
        }

        private void btn_prB_Click(object sender, RoutedEventArgs e)
        {
            browseFile();
        }

        private void btn_vorige_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }
    }
}
