using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace Encryption
{
    /// <summary>
    /// Interaction logic for Encrypteer.xaml
    /// </summary>
    public partial class Encrypteer : Window
    {

        public Encrypteer()
        {
            InitializeComponent();
            grb_KeyFiles.Visibility = Visibility.Hidden;
            windEncr.Height = 80;

        }

        private void btn_ophalenKeys_Click(object sender, RoutedEventArgs e)
        {
            grb_KeyFiles.Visibility = Visibility.Visible;
            windEncr.Height = 225;
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

        private void btn_privateB_Click(object sender, RoutedEventArgs e)
        {
            browseFile();
        }

        private void btn_publicB_Click(object sender, RoutedEventArgs e)
        {
            browseFile();
        }

        private void btn_privateA_Click(object sender, RoutedEventArgs e)
        {
            browseFile();
        }

        private void btn_publicA_Click(object sender, RoutedEventArgs e)
        {
            browseFile();
        }

        private void btn_vorige_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }

        private void btn_volgende_Click(object sender, RoutedEventArgs e)
        {
            UploadMessage window = new UploadMessage();
            this.Close();
            window.Show();
        }
    }
}
