
using System.Collections.ObjectModel;
using System.Linq;

namespace WBHue
{
    public class LightViewModel
    {
        private ObservableCollection<LightModel> m_rows;

        public LightViewModel()
        {
            m_rows = new ObservableCollection<LightModel>();

        }

        public ObservableCollection<LightModel> rows
        {
            get
            {
                
                return m_rows;
            }
            set
            {
                m_rows = value;
            }
        }

        public void AddLight(LightModel model)
        {
            rows.Add(model);
        }

        public void UpdateLight(LightModel model)
        {
            var item = rows.FirstOrDefault(r => r == model);
            if (item != null)
            {
                item = model;
            }
        }
    }
}
