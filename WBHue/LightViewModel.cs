using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
