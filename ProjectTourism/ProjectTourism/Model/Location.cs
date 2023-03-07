using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Location:Serializable
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Location(int id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }
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
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            City = values[1];
            Country = values[2];
        }
    }
}
