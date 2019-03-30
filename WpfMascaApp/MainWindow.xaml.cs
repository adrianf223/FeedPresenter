using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Net;
using System.IO;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Text.RegularExpressions;

namespace WpfMascaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string theurl = "https://entwickler.de/windowsdeveloper/feed/rss2";
        SyndicationFeed downloadFeed;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string result = null;
            using (var webClientInstance = new WebClient())
            {
                webClientInstance.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                var result = webClientInstance.DownloadData(new Uri(theurl));
                var utf8 = new UTF8Encoding();
                var webdata = utf8.GetString(result);

                using (StringReader stringReaderInstance = new StringReader(webdata.ToString()))
                {
                    var reader = XmlReader.Create(stringReaderInstance);
                    downloadFeed = SyndicationFeed.Load(reader);
                }

                var firstPost = downloadFeed.Items.FirstOrDefault();

                var imgurl = firstPost.Links.FirstOrDefault(link => link.RelationshipType.Contains("enclosure"))?.Uri;

                if (imgurl != null)
                {
                    imgbackground.Source = new BitmapImage(imgurl);
                    imagethumb.Source = new BitmapImage(imgurl);
                }

                feedTitle.Text = firstPost.Title.Text;

                //var webtext =  firstPost.Summary.Text;

                //var writer = new StringWriter();
                //WebUtility.HtmlEncode(webtext, writer);

                //feedSummary.Text =  writer.ToString(); 
                //feedSummary.Selection.Text = WebUtility.HtmlEncode(firstPost.Summary.Text);
                feedSummary.Text = HtmlToPlainText( firstPost.Summary.Text );
            }

        }

        // from: 
        // https://stackoverflow.com/questions/286813/how-do-you-convert-html-to-plain-text
        //
        private static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
    }
}
