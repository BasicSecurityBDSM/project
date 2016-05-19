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

namespace Encryption
{
    /// <summary>
    /// Interaction logic for UploadMessage.xaml
    /// </summary>
    public partial class UploadMessage : Window
    {
        public UploadMessage()
        {
            InitializeComponent();
        }

        private void cmn_Bestand_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void cmn_Audio_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void cmn_Foto_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            browseFile();
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

        private void btn_vorige_Click(object sender, RoutedEventArgs e)
        {
            Encrypteer window = new Encrypteer();
            this.Close();
            window.Show();
        }
    }
}
