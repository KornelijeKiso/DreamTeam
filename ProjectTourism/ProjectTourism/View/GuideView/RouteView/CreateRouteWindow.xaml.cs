using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;

namespace ProjectTourism.View.RouteView
{
    /// <summary>
    /// Interaction logic for CreateRouteWindow.xaml
    /// </summary>
    public partial class CreateRouteWindow : Window, INotifyPropertyChanged, IObserver
    {
        public Route Route { get; set; }
        public RouteController RouteController { get; set; }
        public Location NewLocation { get; set; }
        public LocationDAO NewLocationDAO { get; set; }
        public CreateRouteWindow(Guide guide)
        {
            InitializeComponent();
            DataContext = this;
            Route = new Route();
            Route.GuideUsername= guide.Username;
            Route.Guide = guide;
            RouteController = new RouteController();
            RouteController.Subscribe(this);
            NewLocation = new Location();
            NewLocationDAO = new LocationDAO();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {
            
        }

        private void AttachRouteImagesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveRoute_Click(object sender, RoutedEventArgs e)
        {
            NewLocation.Id = NewLocationDAO.AddAndReturnId(NewLocation);
            Route.Location = NewLocation;
            Route.LocationId = NewLocation.Id;
            RouteController.Add(Route);
            Close();
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            switch (LanguageComboBox.SelectedItem.ToString())
            {
                    case "English":
                        Route.Language = "English";
                        break;
                    case "Serbian":
                        Route.Language = "Serbian";
                        break;
                    case "Spanish":
                        Route.Language = "Spanish";
                        break;
                    case "French":
                        Route.Language = "French";
                        break;
                    case "German":
                        Route.Language = "German";
                        break;
                    case "Italian":
                        Route.Language = "Italian";
                        break;
                    case "Portuguese":
                        Route.Language = "Portuguese";
                        break;
                    case "Russian":
                        Route.Language = "Russian";
                        break;
                    case "Arabic":
                        Route.Language = "Arabic";
                        break;
                    case "Chinese":
                        Route.Language = "Chinese";
                        break;                    
                    
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
