using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class Guest2FileHandler
    {
        private Serializer<Guest2> Serializer;
        private readonly string Filename = "../../../References/guest2.csv";
        private List<Guest2> Guests;

        public Guest2FileHandler()
        {
            Serializer = new Serializer<Guest2>();
            Guests = Serializer.fromCSV(Filename);
        }

        public List<Guest2> Load()
        {
            Guests = Serializer.fromCSV(Filename);
            return Guests;
        }

        public void Save(List<Guest2> guests)
        {
            Serializer.toCSV(Filename, guests);
        }
    }
}
