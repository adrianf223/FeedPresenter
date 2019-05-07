using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
                             
namespace FeedPresenter
{
    public class FeedPresenterViewModel : FeedPresenterBase
    {
        public IFeedPresenterDownloader Downloader { get; }
        public IFeedPresenterItem FeedItem { get; }

        private BitmapImage _imageBackground;
        private BitmapImage _imageThumb;
        private string _feedTitle;
        private string _feedSummary;

        private FeedPresenterLoadedAction _loadAction;

        public FeedPresenterViewModel()
        {
            Downloader = (IFeedPresenterDownloader)FeedServiceProvider.GetService(typeof(IFeedPresenterDownloader));
            FeedItem = (IFeedPresenterItem)FeedServiceProvider.GetService(typeof(IFeedPresenterItem));
        }

        public BitmapImage ImageBackground
        {
            get => _imageBackground;
            set =>  Set<BitmapImage>(ref _imageBackground, value);
        }

        public BitmapImage ImageThumb
        {
            get => _imageThumb;
            set => Set<BitmapImage>(ref _imageThumb, value);
        }

        public String FeedTitle
        {
            get => _feedTitle;
            set => Set<string>(ref _feedTitle, value);
        }

        public String FeedSummary
        {
            get => _feedSummary;
            set => Set<string>(ref _feedSummary, value);
        }

        public FeedPresenterLoadedAction LoadAction => _loadAction ?? (
            _loadAction = new FeedPresenterLoadedAction(OnLoadAction())
            );


        private Action OnLoadAction()
        {
            return async () => await UpdateFeedDisplay();
        }

        public async Task UpdateFeedDisplay()
        {

            var first = await Downloader.GetFirstPostAsync();
            FeedItem.Load(first);

            FeedTitle = FeedItem.FeedTitle;
            FeedSummary = FeedItem.FeedSummary;
              
            if (FeedItem.HasImage)
            {
                ImageBackground = ImageThumb = FeedItem.Image;
            }
        }
    }
}
