﻿using System;
using System.Collections.Generic;
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
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectTourism.Domain.Model;

namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for CreateTourRequestWindow.xaml
    /// </summary>
    public partial class CreateTourRequestWindow : Window, INotifyPropertyChanged
    {
        public Guest2VM Guest2 { get; set; }
        public List<string> LanguageList { get; set; }
        public TourRequestVM TourRequest { get; set; }


        public CreateTourRequestWindow(Guest2VM guest2)
        {
            InitializeComponent();
            DataContext = this;

            Guest2 = guest2;
            SetTourRequest();
            LanguageList = new List<string> { "English", "Spanish", "French", "German", "Italian", "Portuguese", "Russian", "Japanese", "Korean", "Chinese", "Dutch", "Swedish", "Norwegian", "Danish", "Finnish", "Turkish", "Greek", "Polish", "Arabic", "Hebrew", };
            SetUpDatePicker();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {
            throw new NotImplementedException();
        }

        private void SetTourRequest()
        {
            TourRequest = new TourRequestVM(new TourRequest());
            TourRequest.Guest2Username = Guest2.Username;
            TourRequest.CreationDateTime = DateTime.Now;
            TourRequest.Location = new LocationVM(new Location());
        }
        private void SetUpDatePicker()
        {
            StartDatePicker.DisplayDate = DateTime.Now;
            TourRequest.StartDate = DateOnly.FromDateTime(new DateTime(1, 1, 1));
            StartDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(2)));

            EndDatePicker.DisplayDate = DateTime.Now;
            TourRequest.EndDate = DateOnly.FromDateTime(new DateTime(1, 1, 1));
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(2)));
        }
        private void StartDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TourRequest.StartDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
            DateTime startDate = (TourRequest.StartDate.ToDateTime(TimeOnly.MinValue));
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), startDate));
        }
        private void EndDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TourRequest.EndDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
        }

        private void CreateTourRequest(object sender, RoutedEventArgs e)
        {
            if (TourRequest.IsValid && 
               (TourRequest.StartDate != DateOnly.FromDateTime(new DateTime(1, 1, 1))) && 
               (TourRequest.EndDate != DateOnly.FromDateTime(new DateTime(1, 1, 1)))    )
            {
                if (TourRequest.StartDate > TourRequest.EndDate)
                    MessageBox.Show("Invalid start and end date!");
                else
                {
                    Guest2.CreateTourRequest(TourRequest);
                    Close();
                }
            }
            else 
                MessageBox.Show("Tour Request can't be made because the data were not entered correctly.");
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}