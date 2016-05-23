using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using hybridEncryptor;

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
        string keyPath;
        string IVPath;
        string HashPath;
        string FilePath;
        public Decrypteer(HybridEncryptor encryptor, string privateA, string publicB)
        {
            InitializeComponent();
            this.encryptor = encryptor;
            this.privateA = privateA;
            this.publicB = publicB;
        }

        private Tuple<string, byte[]> browseFile()
        {
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
            keyPath = browseFile().Item1;
        }

        private void btn_IV_Click(object sender, RoutedEventArgs e)
        {
            IVPath = browseFile().Item1;
        }

        private void btn_hash_Click(object sender, RoutedEventArgs e)
        {
            HashPath = browseFile().Item1;
        }

        private void btn_file_Click(object sender, RoutedEventArgs e)
        {
            FilePath = browseFile().Item1;
        }

        private void btn_vorige_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }

        private void btn_decrypt_Click(object sender, RoutedEventArgs e)
        {
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
            DecryptedFile decrypted = encryptor.Decrypt(encrypted,publicB,privateA);
            if (decrypted.GetHash())
            {
                FileStream fileStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\encrypted\\test.png", FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fileStream);
                writer.Write(decrypted.GetFile());
            }
        }
    }
}
