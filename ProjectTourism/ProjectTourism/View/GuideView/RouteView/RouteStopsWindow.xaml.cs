using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.View.GuideView.RouteView
{
    /// <summary>
    /// Interaction logic for RouteStopsWindow.xaml
    /// </summary>
    public partial class RouteStopsWindow : Window, INotifyPropertyChanged, IObserver
    {
        public RouteController RouteController { get; set; }
        public Route Route { get; set; }
        public ObservableCollection<Ticket> tickets { get; set; }
        public TicketController TicketController { get; set; }
        public Ticket SelectedTicket { get; set; }  

        public RouteStopsWindow(int id)
        {
            InitializeComponent();
            DataContext = this;
            RouteController = new RouteController();
            TicketController = new TicketController();
            Route = RouteController.GetOne(id);
            tickets = new ObservableCollection<Ticket>(TicketController.GetByRoute(Route));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int PassedButtonClicks(Route route)
        {
            int number = 0;
            foreach(var stop in route.StopsList)
            {
                if (stop.Equals(route.CurrentStop))
                    break;
                number++;
            }
            return number;
        }

        public bool IsLastStop(Route route)
        {
            return route.StopsList.Last().Equals(route.CurrentStop);
        }

        public void FinishRoute(Route route)
        {
            StopPassedButton.Content = "Finish route";
            route.IsNotFinished = false;
            route.State = ROUTESTATE.FINISHED;
            RouteController.ChangeState(route);
            StopPassedButton.IsEnabled = false;
            EmergencyStopButton.IsEnabled = false;
        }

        public void NextStop(Route route)
        {
            StopTextBox.Text = RouteController.GetNextStop(route, PassedButtonClicks(route));
            StopPassedButton.Content = "Stop passed";
            route.CurrentStop = route.StopsList[PassedButtonClicks(route) + 1];
            RouteController.ChangeCurrentStop(route);
            route.State = ROUTESTATE.STARTED;
            RouteController.ChangeState(route);
        }
        private void StopPassedButton_Click(object sender, RoutedEventArgs e)
        {
            NextStop(Route);
            if (IsLastStop(Route))
            {
                FinishRoute(Route);
            }
        }
        private void EmergencyStopButton_Click(object sender, RoutedEventArgs e)
        {
            Route.State = ROUTESTATE.STOPPED;
            RouteController.ChangeState(Route);
            Route.IsNotFinished = false;
        }

        private void TicketStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTicket.ButtonColor!=Brushes.Green)
            {
                SelectedTicket.ButtonColor = Brushes.IndianRed;
                TicketController.GuideCheck(SelectedTicket);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void Update()
        {
            tickets = new ObservableCollection<Ticket>((IEnumerable<Ticket>)TicketController.GetByRoute(Route));
        }

    }
}
