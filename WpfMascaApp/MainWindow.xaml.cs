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
using System.Net;
using System.IO;
using System.ServiceModel.Syndication;
using System.Xml;

namespace WpfMascaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string theurl = "https://www.stiridecluj.ro/rss.xml";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string result = null;
            using (var webClientInstance = new WebClient())
            {
                webClientInstance.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                result = webClientInstance.DownloadString(new Uri(theurl));


                using (StringReader stringReaderInstance = new StringReader(result.ToString()))
                {
                    var reader = XmlReader.Create(stringReaderInstance);
                    var downloadFeed = SyndicationFeed.Load(reader);
                }
            }

        }
    }
}
