using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WBHue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        ApiManager apiManager = new ApiManager();

        public MainPage()
        {
            this.InitializeComponent();
            LightViewModel lightsList = new LightViewModel();

            var val = Task.Run(async () => await apiManager.getLights());
            lightsList.AddLight(new LightModel() { name = "Lamp A" });

            

            this.DataContext = lightsList;


        }

        //private void lightSwitch_Toggled(object sender, RoutedEventArgs e)
        //{
            //Task.Run(async () => await apiManager.putLightState());
        //}
    }
}
