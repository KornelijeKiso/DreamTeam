using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;

namespace ProjectTourism.ModelDAO
{
    public class GuideDAO
    {
        public GuideFileHandler GuideFileHandler { get; set; }

        public List <Guide> Guides { get; set; }
        public GuideDAO()
        { 
            GuideFileHandler= new GuideFileHandler();
            Guides = GuideFileHandler.Load();
        }
        public void Add(Guide guide)
        {
            Guides.Add(guide);
            GuideFileHandler.Save(Guides);
        }
        public Guide? GetOne(string username)
        {
            foreach(Guide guide in Guides)
            {
                if (guide.Username.Equals(username))
                {
                    return guide;
                }
            }
            return null;
        }
        public Guide? Identify(string username)
        {
            Guides = GuideFileHandler.Load();
            foreach(Guide guide in Guides)
            {
                if(guide.Surname.Equals(username))
                {
                    return guide;
                }
            }
            return null;
        }
    }
}
