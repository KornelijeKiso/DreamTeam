using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectTourism.Model
{
    public class Location: Serializable
    {
        public int Id;
        public string City;
        public string Country;
        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }
        public Location() { }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                City,
                Country          };
            return csvValues;
        }

        public void Reset()
        {
            City = "";
            Country = "";
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            City = values[1];
            Country = values[2];
        }
    }
}
