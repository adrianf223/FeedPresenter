using System;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace FeedPresenter.Services
{
    public class FeedDownloader : IFeedDownloader
    {
        const string TheUrl = "https://entwickler.de/windowsdeveloper/feed/rss2";

        private WebClient webClientInstance;
        private string _webdata;
        private SyndicationFeed _downloadedFeed;

        public FeedDownloader()
        {
            DownloadWebData();
            ReadWebData();
        }

        private void DownloadWebData()
        {
            using (webClientInstance = new WebClient())
            {

                webClientInstance.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                var result = webClientInstance.DownloadData(new Uri(TheUrl));

                var utf8 = new UTF8Encoding();
                _webdata = utf8.GetString(result);
            }
        }

        private void ReadWebData()
        {
            using (StringReader stringReaderInstance = new StringReader(_webdata))
            {
                var reader = XmlReader.Create(stringReaderInstance);
                _downloadedFeed = SyndicationFeed.Load(reader);
            }
        }

        public SyndicationFeed GetSourceFeed() => _downloadedFeed;

        public SyndicationItem FirstPost => _downloadedFeed.Items.FirstOrDefault();
    }
}
