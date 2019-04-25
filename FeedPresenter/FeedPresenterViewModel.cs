using FeedPresenter.Services;
using System;
using System.Windows.Media.Imaging;
                             
namespace FeedPresenter
{
    public class FeedPresenterViewModel : FeedPresenterBase
    {
        public IFeedDownloader Downloader { get; }
        public IFeedItemContentSelector FeedItemContent { get; }

        private BitmapImage _imageBackground;
        private BitmapImage _imageThumb;
        private string _feedTitle;
        private string _feedSummary;

        private FeedPresenterLoadedAction _loadAction;

        public FeedPresenterViewModel()
        {
            Downloader = (IFeedDownloader)FeedServiceProvider.GetService(typeof(IFeedDownloader));
            FeedItemContent = (IFeedItemContentSelector)FeedServiceProvider.GetService(typeof(IFeedItemContentSelector));
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


        private Action OnLoadAction() => () => UpdateFeedDisplay();

        public void UpdateFeedDisplay()
        {
            FeedTitle = FeedItemContent.GetTitlePlainText(Downloader.FirstPost);
            FeedSummary = FeedItemContent.GetSummaryPlainText(Downloader.FirstPost);
              
            if (FeedItemContent.PostHasImage(Downloader.FirstPost))
            {
                ImageBackground = ImageThumb = FeedItemContent.GetPostImage(Downloader.FirstPost);
            }
        }
    }
}
