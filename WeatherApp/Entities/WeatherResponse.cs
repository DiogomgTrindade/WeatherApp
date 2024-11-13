using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Entities
{
    public class WeatherResponse
    {
        public Main Main { get; set; }

        public Weather[] Weather { get; set; }
    }
}
