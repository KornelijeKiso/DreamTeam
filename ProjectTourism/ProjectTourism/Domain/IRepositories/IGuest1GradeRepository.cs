using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IGuest1GradeRepository
    {
        Guest1Grade GetOne(int guest1GradeId);
        List<Guest1Grade> GetAll();
        void Add(Guest1Grade guest1Grade);
        void Delete(Guest1Grade guest1Grade);
        void Update(Guest1Grade guest1Grade);
        Guest1Grade GetOneByReservation(int reservationId);
    }
}
