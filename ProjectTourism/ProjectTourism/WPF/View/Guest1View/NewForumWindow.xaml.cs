using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Model;
using ProjectTourism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectTourism.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for NewForumWindow.xaml
    /// </summary>
    public partial class NewForumWindow : Window
    {
        public ForumDTO noviforum { get; set; }
        public ObservableCollection<ForumDTO> ExistingForums { get; set; }
        public Location location { get; set; }
        public string commentUsername { get; set; }
        public NewForumWindow(ObservableCollection<ForumDTO> Forums, string username)
        {
            ExistingForums = Forums;
            commentUsername = username;

            InitializeComponent();
        }
        public void SubmitNewForum(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(City.Text.ToString()) || string.IsNullOrEmpty(Country.Text.ToString()) || string.IsNullOrEmpty(ForumComment.Text.ToString())) {
                location = new Location(City.Text, Country.Text);
                if(ExistingForums.ToList().Find(f=>f.Location.City==location.City && f.Location.Country.Equals(location.Country)) != null){
                    MessageBox.Show("Forum on this location already exists.");
                    return;
                }
                if(new LocationService().GetAll().Find(l => l.City.Equals(location.City) && l.Country.Equals(location.Country)) == null)
                {
                    location.Id = new LocationService().AddAndReturnId(location);
                }
                else
                {
                    location = new LocationService().GetAll().Find(l => l.City.Equals(location.City) && l.Country.Equals(location.Country));
                }
                Forum f = new Forum();
                f.LocationId = location.Id;
                
                new ForumService().Add(f);
                noviforum = new ForumDTO(new ForumService().GetAll().Find(f => f.LocationId == location.Id));
                noviforum.Location = new LocationDTO(new LocationService().GetOne(location.Id));
                new NotificationService().NotifyAllOwnersAboutNewForum(location);
                CommentOnForum cf = new CommentOnForum();
                cf.ForumId = noviforum.Id;
                cf.Text = ForumComment.Text;
                cf.Published = DateTime.Now;
                cf.Username = commentUsername;
                new CommentOnForumService().Add(cf);
                noviforum.Comments = new ObservableCollection<CommentOnForumDTO>();
                noviforum.Comments.Add(new CommentOnForumDTO(cf));
                Close();
            }
            else
            {
                MessageBox.Show("You did not enterred all data properly.");
            }
        }
    }
}
