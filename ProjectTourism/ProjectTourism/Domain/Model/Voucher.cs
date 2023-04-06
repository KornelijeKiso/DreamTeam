using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public enum STATUS { USED, VALID, INVALID };

namespace ProjectTourism.Model
{
    public class Voucher : Serializable
    {
        public int Id { get; set; }
        public string Guest2Username { get; set; }
        public Guest2 Guest2 { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidDue { get; set; }
        public STATUS Status { get; set; }
        public string Description { get; set; }

        public Voucher()
        {}

        public Voucher(string guest2username, DateTime validFrom, DateTime validDue, STATUS status, string description)
        {
            Guest2Username = guest2username;
            ValidFrom = validFrom;
            ValidDue = validDue;
            Status = status;
            Description = description;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Guest2Username.ToString(),
                ValidFrom.ToString("dd.MM.yyyy HH:mm"),
                ValidDue.ToString("dd.MM.yyyy HH:mm"),
                Status.ToString(),
                Description
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest2Username = values[1];
            if (DateTime.TryParse(values[2], new CultureInfo("en-GB"), DateTimeStyles.None, out var dateTimeParsedFrom))
                ValidFrom = dateTimeParsedFrom;
            if (DateTime.TryParse(values[3], new CultureInfo("en-GB"), DateTimeStyles.None, out var dateTimeParsedDue))
                ValidDue = dateTimeParsedDue;
            switch (values[4])
            {
                case "USED":
                    { Status = STATUS.USED; break; }
                case "VALID":
                    { Status = STATUS.VALID; break; }
                case "INVALID":
                    { Status = STATUS.INVALID; break; }

                default:
                    { Status = STATUS.VALID; break; }
            }
            Description = values[5];
        }
    }
}
