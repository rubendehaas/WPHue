
using System.ComponentModel;

namespace WBHue
{
    public class LightModel : INotifyPropertyChanged
    {
        public LightModel()
        {

        }

        public int key
        {
            get;
            set;
        }
        public bool on
        {
            get;
            set;
        }
        public string name { get; set; }
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
