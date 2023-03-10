using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class Guest1FileHandler
    {
        private Serializer<Guest1> Serializer;
        private readonly string Filename = "../../../References/owners.csv";
        private List<Guest1> Guests1;

        public Guest1FileHandler()
        {
            Serializer = new Serializer<Guest1>();
            Guests1 = Serializer.fromCSV(Filename);
        }

        public List<Guest1> Load()
        {
            Guests1 = Serializer.fromCSV(Filename);
            return Guests1;
        }

        public void Save(List<Guest1> guests1)
        {
            Serializer.toCSV(Filename, guests1);
        }
    }

}
