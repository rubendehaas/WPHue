using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.UI.Popups;
using System.Diagnostics;
using Windows.Data.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WBHue
{
    class ApiManager
    {

        string baseUrl = "http://localhost:8000/api/newdeveloper/lights";

        public async Task getLights()
        {
           
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

                await Task.Run(()=>
                {
                    
                    return parseJson(json);
                }); 

            }
            catch (OperationCanceledException)
            {
                await new MessageDialog("Operation time out").ShowAsync();
            }
        }

        public List<LightModel> parseJson(string jsonString)
        {
            List<LightModel> lightList = new List<LightModel>();
            JObject jsonObject = JObject.Parse(jsonString);
            LightViewModel lightVM = new LightViewModel();

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

                
                //lightVM.AddLight(lightModel);
                lightList.Add(lightModel);
                
            }

            return lightList;
        }

        public async Task putLightState()
        {
            try
            {
                var jsonString = "{\"on\":false}";
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var response = await client.PutAsync(baseUrl+"/1/state", httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    await new MessageDialog("Failed request").ShowAsync();
                    return;
                }

                string json = await response.Content.ReadAsStringAsync();
                Debug.Write(json);

            }
            catch (OperationCanceledException)
            {
                await new MessageDialog("Operation time out").ShowAsync();
            }
        }
    }
}
