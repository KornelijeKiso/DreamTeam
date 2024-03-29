﻿using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class OwnerRepository:IOwnerRepository
    {
        public OwnerFileHandler FileHandler { get; set; }
        public List<Owner> Owners { get; set; }
        public OwnerRepository()
        {
            FileHandler = new OwnerFileHandler();
            Owners = FileHandler.Load();
        }

        public void Add(Owner owner)
        {
            Owners.Add(owner);
            FileHandler.Save(Owners);
        }
        public void Delete(Owner owner)
        {
            Owners.Remove(owner);
            FileHandler.Save(Owners);
        }
        public List<Owner> GetAll()
        {
            return Owners;
        }
        public Owner GetOne(string username)
        {
            return Owners.Find(o => o.Username.Equals(username));
        }

        public void Update(Owner owner)
        {
            throw new NotImplementedException();
        }

        public void TurnHelpOn(Owner owner)
        {
            foreach(var o in Owners){
                if (o.Username.Equals(owner.Username))
                {
                    o.HelpOn = true;
                    break;
                }
            }
            FileHandler.Save(Owners); 
        }

        public void TurnHelpOff(Owner owner)
        {
            foreach (var o in Owners)
            {
                if (o.Username.Equals(owner.Username))
                {
                    o.HelpOn = false;
                    break;
                }
            }
            FileHandler.Save(Owners);
        }
    }
}
