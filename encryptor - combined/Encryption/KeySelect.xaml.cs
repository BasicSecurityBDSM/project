using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using hybridEncryptor;
using System.Windows.Media;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace Encryption
{
    /// <summary>
    /// Interaction logic for Encrypteer.xaml
    /// </summary>
    public partial class KeySelect : Window
    {
        private HybridEncryptor encryptor;
        string privateA = null;
        string publicB = null;
        bool next;
        public KeySelect(HybridEncryptor encryptor,bool type)
        {
            this.encryptor = encryptor;
            InitializeComponent();
            btn_volgende.IsEnabled = false;
            next = type;
        }

        private string browseFile()
        {
            OpenFileDialog fd = new OpenFileDialog();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string output = null; string file = null;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK) // Test result.
            {
                file = fd.FileName;

                try
                {
                    if (Directory.Exists(path))
                    {
                        fd.InitialDirectory = path;
                    }
                    else {
                        fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    }
                    output = File.ReadAllText(file);
                }
                catch (IOException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            return output;
        }

        private void btn_genereerKey_Click(object sender, RoutedEventArgs e)
        {
            encryptor.GenerateRsaKey();
            privateA = encryptor.GetRsaKey(true);
            string filename = Interaction.InputBox("geef een naam voor de keys", "key naam", "myKey");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\encrypted\\keys");
            using (StreamWriter file = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\encrypted\\keys\\private_"+filename))
            {
                file.WriteLine(privateA);
                file.Close();
            }
            using (StreamWriter file = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\encrypted\\keys\\public_"+filename))
            {
                file.WriteLine(encryptor.GetRsaKey(false));
                file.Close();
            }
            System.Windows.MessageBox.Show("de files zijn gesaved onder:" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\encrypted\\keys");
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\encrypted\\keys");
            lblUw.Background = Brushes.Green;
            Unlock();
        }

        private void btn_getPrivateA_Click(object sender, RoutedEventArgs e)
        {
            privateA = browseFile();
            lblUw.Background = Brushes.Green;
            Unlock();
        }

        private void btn_getPublicB_Click(object sender, RoutedEventArgs e)
        {
            publicB = browseFile();
            lblHun.Background = Brushes.Green;
            Unlock();
        }
        private void Unlock()
        {
            if (privateA != null && publicB != null)
            {
                btn_volgende.IsEnabled = true;
            }
        }
        private void btn_vorige_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            Close();
            window.Show();
        }

        private void btn_volgende_Click(object sender, RoutedEventArgs e)
        {
            if (next)
            {
                new Encrypteer(encryptor, privateA, publicB).Show();
            }
            else
            {
                new Decrypteer(encryptor, privateA, publicB).Show();
            }
            Close();
        }
    }
}
