﻿using ProjectTourism.DTO;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System;
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

namespace ProjectTourism.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for MyProfileWindow.xaml
    /// </summary>
    public partial class MyProfileWindow : UserControl
    {
        public Guest1DTO Guest1 { get; set; }
        //public float AverageGrade { get; set; }
        public MyProfileWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guest1 = new Guest1DTO(username);
            //AverageGrade = Guest1VM.CalculateGrade();
        }
    }
}
