using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WBHue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LightDetailPage : Page
    {
        Payload payload;
        LightViewModel lightViewModel;
        LightModel lightModel;
        ApiManager apiManager = new ApiManager();

        public LightDetailPage()
        {
            this.InitializeComponent();

            
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            payload = e.Parameter as Payload;
            lightModel = payload.lm;
            lightViewModel = payload.lvm;

            this.DataContext = lightModel;
        }

        private void lightSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            
            lightModel.on = lightSwitch.IsOn;

            Task.Run(async () => await apiManager.putLightState(lightModel));
            lightViewModel.UpdateLight(lightModel);
        }

        private void lightBriSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            lightModel.bri = Convert.ToInt32(lightBriSlider.Value);

            Task.Run(async () => await apiManager.putLightState(lightModel));
            lightViewModel.UpdateLight(lightModel);
        }

        private void lightSatSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            lightModel.sat = Convert.ToInt32(lightSatSlider.Value);

            Task.Run(async () => await apiManager.putLightState(lightModel));
            lightViewModel.UpdateLight(lightModel);
        }

        private void lightHueSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            lightModel.hue = Convert.ToInt32(lightHueSlider.Value);

            Task.Run(async () => await apiManager.putLightState(lightModel));
            lightViewModel.UpdateLight(lightModel);
        }
    }
}
