using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;

namespace ProjectTourism.Controller
{
    public class GuideController
    {
        public GuideDAO GuideDAO { get; set; }
        public GuideController()
        {
            GuideDAO = new GuideDAO();
        }
        public List<Guide> GetAll()
        {
            return GuideDAO.Guides;
        }
        public Guide? GetOne(string username)
        {
            foreach(Guide guide in GetAll())
            {
                if (guide.Username == username)
                    return guide;
            }
            return null;
        }
        public void Add(Guide guide)
        {
            GuideDAO.Add(guide);
        }
        public Guide Identify(string username)
        {
            return GuideDAO.Identify(username);
        }
    }
}
