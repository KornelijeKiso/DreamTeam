using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class OwnerFileHandler
    {
        private Serializer<Owner> Serializer;
        private readonly string Filename = "../../../References/owners.csv";
        private List<Owner> Owners;

        public OwnerFileHandler()
        {
            Serializer = new Serializer<Owner>();
            Owners = Serializer.fromCSV(Filename);
        }

        public List<Owner> Load()
        {
            Owners = Serializer.fromCSV(Filename);
            return Owners;
        }

        public void Save(List<Owner> owners)
        {
            Serializer.toCSV(Filename, owners);
        }
    }
}
