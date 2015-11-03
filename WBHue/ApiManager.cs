using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.UI.Popups;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WBHue
{
    class ApiManager
    {
        LightViewModel lvm;
        string baseUrl = "http://192.168.1.179/api/626b39467d5a4f110a85bc2f0843b7/lights/";

        public async Task getLights(LightViewModel lightViewModel, TaskScheduler context)
        {
            lvm = lightViewModel;

            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(baseUrl);

                if (!response.IsSuccessStatusCode)
                {
                    await new MessageDialog("Failed request").ShowAsync();
                    return;
                }

                string json = await response.Content.ReadAsStringAsync();



                await Task.Run(()=> { }).ContinueWith(a =>
                {
                    parseJson(json);
                },context); 

                

            }
            catch (OperationCanceledException)
            {
                await new MessageDialog("Operation time out").ShowAsync();
            }
        }

        public void parseJson(string jsonString)
        {

            JObject jsonObject = JObject.Parse(jsonString);
            //LightViewModel lightVM = new LightViewModel();

            foreach (var light in jsonObject)
            {
                LightModel lightModel = new LightModel();
                lightModel.key = int.Parse(light.Key);
                lightModel.name = (string)light.Value["name"];
                lightModel.on = (bool)light.Value["state"]["on"];
                lightModel.bri = (int)light.Value["state"]["bri"];

                if (light.Value["state"]["sat"] != null)
                { 
                    lightModel.sat = (int)light.Value["state"]["sat"];
                    lightModel.hue = (int)light.Value["state"]["hue"];
                }


                lvm.AddLight(lightModel);
            }
        }

        public async Task putLightState(LightModel lm)
        {
            try
            {
                //var jsonString = "{\"on\":"+ lm.on + "}";
                var jsn = JsonConvert.SerializeObject(lm);

                var httpContent = new StringContent(jsn, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var response = await client.PutAsync(baseUrl+lm.key+"/state", httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    await new MessageDialog("Failed request").ShowAsync();
                    return;
                }

                string json = await response.Content.ReadAsStringAsync();

            }
            catch (OperationCanceledException)
            {
                await new MessageDialog("Operation time out").ShowAsync();
            }
        }
    }
}
