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
    public class ReportedComment:Serializable
    {
        public int CommentId;
        public string Username;
        public ReportedComment() { }
        public ReportedComment(int CommentId, string Username)
        {
            this.CommentId = CommentId;
            this.Username = Username;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                CommentId.ToString(),
                Username    };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            CommentId = int.Parse(values[0]);
            Username = values[1];
        }
    }
}
