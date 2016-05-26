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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using hybridEncryptor;
using System.Diagnostics;

namespace Encryption
{
    /// <summary>
    /// Interaction logic for UploadMessage.xaml
    /// </summary>
    public partial class Encrypteer : Window
    {
        private HybridEncryptor encryptor;
        private string privateA;
        private string publicB;
        private byte[] file;
        public Encrypteer(HybridEncryptor encryptor, string privateA, string publicB)
        {
            InitializeComponent();
            cmb_enc.IsEnabled = false;
            btn_encrypteer.IsEnabled = false;
            this.encryptor = encryptor;
            this.privateA = privateA;
            this.publicB = publicB;
        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            //file ophalen 
            var browsed = browseFile();
            file = browsed.Item2;
            cmb_enc.IsEnabled = true;
            btn_browse.IsEnabled = false;
        }

        private void cmb_enc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //encrpytie typekiezen
            cmb_enc.IsEnabled = false;
            btn_encrypteer.IsEnabled = true;
        }
        private void btn_encrypteer_Click(object sender, RoutedEventArgs e)
        {
            //kijken welke optie geselecteerd is en afhankleijk daarvan de file aanmaken
            EncryptedFile encrypted = encryptor.Encrypt(file,publicB,privateA);
            switch (cmb_enc.SelectedIndex)
            {
                case 0:
                    encrypted = TxtEncryptedFile.FromEncryptedFile(encrypted);
                    break;
                case 1:
                    encrypted = WavEncryptedFile.FromEncryptedFile(encrypted);
                    break;
                case 2:
                    encrypted = ImgEncryptedFile.FromEncryptedFile(encrypted);
                    break;
            }
            //file opslaan
            encrypted.save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\encrypted");
            //melding weergeven en explorer openen op de locatie van de files
            System.Windows.MessageBox.Show("de files zijn gesaved onder:" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\encrypted");
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\encrypted");
        }
        private Tuple<string, byte[]> browseFile()
        {
            //tuple returnen met path en inhoud van file
            OpenFileDialog fd = new OpenFileDialog();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            byte[] output = null; string file = null;
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
                        fd.InitialDirectory = @"C:\";
                    }
                    output = File.ReadAllBytes(file);
                }
                catch (IOException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            return new Tuple<string, byte[]>(file, output);
        }

        private void btn_vorige_Click(object sender, RoutedEventArgs e)
        {
            KeySelect window = new KeySelect(encryptor,true);
            Close();
            window.Show();
        }
    }
}
