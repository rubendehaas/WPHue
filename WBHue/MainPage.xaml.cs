
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WBHue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        ApiManager apiManager = new ApiManager();
        LightViewModel lightsList;

        public MainPage()
        {
            this.InitializeComponent();
            lightsList = new LightViewModel();

            var UISyncContext = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Run(async () => await apiManager.getLights(lightsList, UISyncContext));
            
            this.DataContext = lightsList;
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            LightModel selectedLight = (LightModel) e.ClickedItem;

            Payload payload = new Payload();
            payload.lvm = lightsList;
            payload.lm = selectedLight;

            Frame.Navigate(typeof(LightDetailPage), payload);
            
        }
    }

    public class Payload
    {
        public LightViewModel lvm { get; set; }
        public LightModel lm { get; set; }
    }

}
