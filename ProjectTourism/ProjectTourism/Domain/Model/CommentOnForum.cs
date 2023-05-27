using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.Model
{
    public class CommentOnForum:Serializable
    {
        public int Id { get; set; }
        public int ForumId { get; set; }
        public Forum Forum { get; set; }
        public string Username { get; set; }
        public int Reports { get; set; }
        public string Text { get; set; }
        public DateTime Published { get; set; }
        public CommentOnForum()
        {

        }
        public CommentOnForum(CommentOnForum c)
        {
            Reports = c.Reports;
            Text = c.Text;
            Username = c.Username;
            ForumId = c.ForumId;

        }
        public CommentOnForum(int id, int forumId, string username, int reports, string text)
        {
            Id = id;
            ForumId = forumId;
            Username = username;
            Reports = reports;
            Text = text;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ForumId.ToString(),
                Username,
                Reports.ToString(),
                Text,
                Published.ToString("dd.MM.yyyy HH:mm")
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ForumId = int.Parse(values[1]);
            Username = values[2];
            Reports = int.Parse(values[3]);
            Text = values[4];
            if (DateTime.TryParse(values[5], new CultureInfo("en-GB"), DateTimeStyles.None, out var dateTimeParsed))
                Published = dateTimeParsed;
        }
    }
}
