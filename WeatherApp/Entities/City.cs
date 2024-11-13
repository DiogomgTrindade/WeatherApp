using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Entities
{
    public class City
    {
        public string Name { get; set; }
        
        public double Lat {  get; set; }

        public double Lon { get; set; }

        public string Country { get; set; }
    }
}
