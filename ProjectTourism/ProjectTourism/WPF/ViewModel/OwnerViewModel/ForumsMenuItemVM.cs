using ProjectTourism.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectTourism.WPF.ViewModel.OwnerViewModel
{
    public class ForumsMenuItemVM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public OwnerDTO Owner { get; set; }
        private ForumDTO _SelectedForum;
        public ForumDTO SelectedForum {
            get => _SelectedForum;
            set
            {
                if(value != _SelectedForum)
                {
                    _SelectedForum = value;
                    OnPropertyChanged();
                }
            }
        }
        public ForumsMenuItemVM() { }
        public ForumsMenuItemVM(string username)
        {
            Owner = new OwnerDTO(username);
            if(Owner.Forums.Any())
            {
                SelectedForum = Owner.Forums.First();
            }
        }
    }
}
