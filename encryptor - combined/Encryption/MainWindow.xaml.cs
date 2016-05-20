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
using System.Windows.Navigation;
using System.Windows.Shapes;
using hybridEncryptor;
namespace Encryption
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HybridEncryptor encryptor;
        public MainWindow()
        { 
            InitializeComponent();
            encryptor = new HybridEncryptor();
        }

        private void btn_encrypteer_Click(object sender, RoutedEventArgs e)
        {
            KeySelect window = new KeySelect(encryptor,true);
           Close();
            window.Show();
        }

        private void btn_decrypteer_Click(object sender, RoutedEventArgs e)
        {
            KeySelect window = new KeySelect(encryptor,false);
            Close();
            window.Show();
        }
    }
}
