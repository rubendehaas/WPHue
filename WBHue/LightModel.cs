using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBHue
{
    public class LightModel : INotifyPropertyChanged
    {
        public LightModel()
        {

        }

        public int _key;

        public int key
        {
            get
            {
                return _key; 
            }
            set
            {
                _key = value;

                NotifyPropertyChanged("viewmodel updated");
            }
        }
        public string name { get; set; }
        public bool on { get; set; }
        public int bri { get; set; }
        public int sat { get; set; }
        public int hue { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
