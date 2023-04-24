using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Repositories;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Repositories.IRepositories;

namespace Injector
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {

        };

        public static void BindComponents()
        {
            _implementations.Add(typeof(IAccommodationGradeRepository), new AccommodationGradeRepository());
            _implementations.Add(typeof(IAccommodationRepository), new AccommodationRepository());
            _implementations.Add(typeof(ICanceledReservationRepository), new CanceledReservationRepository());
            _implementations.Add(typeof(ICanceledTourAppointmentsRepository), new CanceledTourAppointmentsRepository());
            _implementations.Add(typeof(ICurrentUserRepository), new CurrentUserRepository());
            _implementations.Add(typeof(IGuest1GradeRepository), new Guest1GradeRepository());
            _implementations.Add(typeof(IGuest1Repository), new Guest1Repository());
            _implementations.Add(typeof(IGuest2Repository), new Guest2Repository());
            _implementations.Add(typeof(IGuideRepository), new GuideRepository());
            _implementations.Add(typeof(ILocationRepository), new LocationRepository());
            _implementations.Add(typeof(IOwnerRepository), new OwnerRepository());
            _implementations.Add(typeof(IPostponeRequestRepository), new PostponeRequestRepository());
            _implementations.Add(typeof(IReservationRepository), new ReservationRepository());
            _implementations.Add(typeof(ITicketGradeRepository), new TicketGradeRepository());
            _implementations.Add(typeof(ITourAppointmentRepository), new TourAppointmentRepository());
            _implementations.Add(typeof(ITourRepository), new TourRepository());
            _implementations.Add(typeof(IUserRepository), new UserRepository());
            _implementations.Add(typeof(IVoucherRepository), new VoucherRepository());
            _implementations.Add(typeof(ITicketRepository), new TicketRepository());
        }

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }

    }
}
