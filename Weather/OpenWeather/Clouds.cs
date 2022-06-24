using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.OpenWeather
{
    class Clouds
    {
        public double _all;

        public double all
        {
            get
            {
                return _all;
            }
            set
            {
                _all = (value / 10);
            }
        }
    }
}
