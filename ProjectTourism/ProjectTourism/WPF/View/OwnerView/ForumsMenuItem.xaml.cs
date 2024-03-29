﻿using ProjectTourism.WPF.ViewModel.OwnerViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for ForumsManuItem.xaml
    /// </summary>
    public partial class ForumsMenuItem : UserControl
    {
        public ForumsMenuItem(string username)
        {
            InitializeComponent();
            DataContext = new ForumsMenuItemVM(username);
        }
    }
}
