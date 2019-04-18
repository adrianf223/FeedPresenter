using System;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace FeedPresenter
{
    public class FeedPresenterDownloader : IFeedPresenterDownloader
    {

       public SyndicationFeed GetSourceFeed(string theUrl)
        {
            using (var webClientInstance = new WebClient())
            {

                webClientInstance.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                var result = webClientInstance.DownloadData(new Uri(theUrl));

                var utf8 = new UTF8Encoding();
                var webdata = utf8.GetString(result);

                using (StringReader stringReaderInstance = new StringReader(webdata.ToString()))
                {
                    var reader = XmlReader.Create(stringReaderInstance);
                    return SyndicationFeed.Load(reader);
                }
            }
        }

        public Uri GetPostImageUrl(SyndicationItem item)
        {
            var imgUrl = item.Links.FirstOrDefault(link => link.RelationshipType.Contains("enclosure"))?.Uri;
            return imgUrl;
        }

        public bool HasImage(SyndicationItem item)
        {
            return GetPostImageUrl(item)!= null;
        }
    }
}
