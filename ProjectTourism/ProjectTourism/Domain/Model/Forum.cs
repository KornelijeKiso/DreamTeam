using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectTourism.Domain.Model
{
    public  class Forum:Serializable
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public Forum(int id, int locationId)
        {
            Id = id;
            LocationId = locationId;
        }
        public Forum() { }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                LocationId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            LocationId = int.Parse(values[1]);
        }
    }
}
