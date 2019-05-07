using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FeedPresenter
{
    public class FeedPresenterDownloader : IFeedPresenterDownloader
    {
        const string TheUrl = "https://entwickler.de/windowsdeveloper/feed/rss2";

        private string _webdata;
        private SyndicationFeed _downloadedFeed;

        public FeedPresenterDownloader()
        {
            _downloadedFeed = new SyndicationFeed();
        }

        private async Task Load()
        {
            try
            {
                 await DownloadWebDataAsync(); 
                ReadWebData(); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task DownloadWebDataAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(TheUrl);

                try
                {
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsByteArrayAsync();
                    var utf8 = new UTF8Encoding();
                    _webdata = utf8.GetString(content);
                }
                catch (Exception ex)
                {
                    Debug.Write (ex.Message);
                }
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

        public async Task<SyndicationFeed> GetSourceFeedAsync()
        {
            await Load();
            return _downloadedFeed;
        }

        public async Task<SyndicationItem> GetFirstPostAsync()
        {
            var feed = await GetSourceFeedAsync();
            return feed.Items.FirstOrDefault();
        }
    }
}
