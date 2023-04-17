using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Utilities;
using ProjectTourism.Repositories;
using ProjectTourism.Services;


namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class VouchersVM : ViewModelBase
    {
        public Guest2VM Guest2 { get; set; }
        public CurrentUserService CurrentUserService { get; set; }
        public UserVM CurrentUser { get; set; }

        public VouchersVM()
        {
            SetGuest2();
        }

        public void SetGuest2()
        {
            CurrentUserService = new CurrentUserService(new CurrentUserRepository());
            CurrentUser = new UserVM(CurrentUserService.GetUser());
            Guest2 = new Guest2VM(CurrentUser.Username);
        }
    }
}
