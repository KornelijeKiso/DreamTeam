using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class Guest1GradeService
    {
        private IGuest1GradeRepository Guest1GradeRepo;
        public Guest1GradeService(IGuest1GradeRepository ig1gr)
        {
            Guest1GradeRepo = ig1gr;
        }
        public void Add(Guest1Grade guest1Grade)
        {
            Guest1GradeRepo.Add(guest1Grade);
        }
        public void Delete(Guest1Grade guest1Grade)
        {
            Guest1GradeRepo.Delete(guest1Grade);
        }
        public Guest1Grade GetOneByReservation(int reservationId)
        {
            return Guest1GradeRepo.GetOneByReservation(reservationId);
        }
        public List<Guest1Grade> GetAll()
        {
            return Guest1GradeRepo.GetAll();
        }  
    }
}
