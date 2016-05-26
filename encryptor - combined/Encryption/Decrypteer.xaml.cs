using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using hybridEncryptor;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Windows.Media;

namespace Encryption
{
    /// <summary>
    /// Interaction logic for Decrypteer.xaml
    /// </summary>
    public partial class Decrypteer : Window
    {
        private HybridEncryptor encryptor;
        private string privateA;
        private string publicB;
        string keyPath = null;
        string IVPath = null;
        string HashPath = null;
        string FilePath = null;
        public Decrypteer(HybridEncryptor encryptor, string privateA, string publicB)
        {
            InitializeComponent();
            this.encryptor = encryptor;
            this.privateA = privateA;
            this.publicB = publicB;
            btnDecrypt.IsEnabled = false;
        }

        private Tuple<string, byte[]> browseFile()
        {
            //file ophalen in een tuple met path naar de file en de inhoud van de file
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

        private void btn_key_Click(object sender, RoutedEventArgs e)
        {
            //bestand inlezen en de kleur op groen zetten
            keyPath = browseFile().Item1;
            lblKey.Background = Brushes.Green;
            unlock();
        }

        private void btn_IV_Click(object sender, RoutedEventArgs e)
        {
            //bestand inlezen en de kleur op groen zetten
            IVPath = browseFile().Item1;
            lblIV.Background = Brushes.Green;
            unlock();
        }

        private void btn_hash_Click(object sender, RoutedEventArgs e)
        {
            //bestand inlezen en de kleur op groen zetten
            HashPath = browseFile().Item1;
            lblHash.Background = Brushes.Green;
            unlock();
        }

        private void btn_file_Click(object sender, RoutedEventArgs e)
        {
            //bestand inlezen en de kleur op groen zetten
            FilePath = browseFile().Item1;
            lblFile.Background = Brushes.Green;
            unlock();
        }
        private void unlock()
        {
            //controleren of alle bestanden ingeladen zijn
            if (keyPath != null && IVPath != null && FilePath != null && HashPath != null)
            {
                btnDecrypt.IsEnabled = true;
            }
        }
        private void btn_vorige_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }

        private void btn_decrypt_Click(object sender, RoutedEventArgs e)
        {
            //controleren welke extensie de bestanden hebben en afhankelijk daarvan de file maken
            EncryptedFile encrypted = null;
            switch (FilePath.Substring(FilePath.Length - 3))
            {
                case "txt":
                    encrypted = new TxtEncryptedFile(FilePath,keyPath,IVPath,HashPath);
                    break;
                case "wav":
                    encrypted = new WavEncryptedFile(FilePath, keyPath, IVPath, HashPath);
                    break;
                case "bmp":
                    encrypted = new ImgEncryptedFile(FilePath, keyPath, IVPath, HashPath);
                    break;
            }
            //decrypteren
            DecryptedFile decrypted = encryptor.Decrypt(encrypted,publicB,privateA);
            //hash controleren
            if (decrypted.GetHash())
            {
                //naam vragen voor de gedecrypteerde file
                string filename = Interaction.InputBox("geef een naam voor het bestand", "bestandsnaam","decrypted");
                //file schrijven
                FileStream fileStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\encrypted\\"+filename, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fileStream);
                writer.Write(decrypted.GetFile());
                writer.Close();
                fileStream.Close();
                //melding weergeven en explorer openen op de locatie van de file
                System.Windows.MessageBox.Show("de file is gesaved onder:" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\encrypted");
                Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\encrypted");
            }
            else
            {
                System.Windows.MessageBox.Show("hash niet geldig");
            }
        }
    }
}
