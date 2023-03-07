using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Controller
{
    public class OwnerController
    {
        private OwnerDAO OwnerDAO;
        public OwnerController()
        {
            OwnerDAO = new OwnerDAO();
        }
        public void Add(Owner owner)
        {
            OwnerDAO.Add(owner);
        }
        public void Delete(Owner owner)
        {
            OwnerDAO.Delete(owner);
        }
        public Owner GetOne(string username)
        {
            return OwnerDAO.GetOne(username);
        }
        public List<Owner> GetAll()
        {
            return OwnerDAO.GetAll();
        }
    }
}
