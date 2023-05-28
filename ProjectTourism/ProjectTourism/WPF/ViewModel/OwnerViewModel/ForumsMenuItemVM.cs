﻿using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Services;
using ProjectTourism.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectTourism.WPF.ViewModel.OwnerViewModel
{
    public class ForumsMenuItemVM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _CommentText;
        public string CommentText
        {
            get => _CommentText;
            set
            {
                if (value != _CommentText)
                {
                    _CommentText= value;
                    OnPropertyChanged();
                }
            }
        }
        public CommentOnForumDTO SelectedComment { get; set; }
        public OwnerDTO Owner { get; set; }

        private ForumDTO _SelectedForum;
        public ForumDTO SelectedForum {
            get => _SelectedForum;
            set
            {
                if(value != _SelectedForum)
                {
                    _SelectedForum = value;
                    NewComment.ForumId = _SelectedForum.Id;
                    OnPropertyChanged();
                }
            }
        }
        public CommentOnForum NewComment = new CommentOnForum();
        public ForumsMenuItemVM() { }
        public ForumsMenuItemVM(string username)
        {
            Owner = new OwnerDTO(username);
            if(Owner.Forums.Any())
            {
                SelectedForum = Owner.Forums.First();
            }
            NewComment.Username = username;
            NewComment.Reports = 0;
            NewComment.ForumId = SelectedForum.Id;
        }

        private void Post(object parameter)
        {
            if(string.IsNullOrEmpty(CommentText))
            {
                MessageBox.Show("You have to enter your comment in order to publish it.");
                return;
            }
            NewComment.Text = CommentText;
            SelectedForum.Comments.Add(new CommentOnForumDTO(new CommentOnForumService().Add(new CommentOnForum(NewComment))));
            CommentText = "";
            SelectedForum.CommentsByOwner++;
            SelectedForum.IsVeryUseful = SelectedForum.CheckIfVeryUseful();
        }
        private void Report(object parameter)
        {
            if(new ReportedCommentService().Add(SelectedComment.Id, Owner.Username))
            {
                new CommentOnForumService().Report(SelectedComment.Id);
                Owner.timer.Stop();
                Owner.Synchronize(Owner.Username);
                Owner.timer.Start();
            }  
            else
                MessageBox.Show("You have already reported this comment.");
        }
        public ICommand PostCommand
        {
            get => new RelayCommand(Post);
        }
        public ICommand ReportCommand
        {
            get => new RelayCommand(Report);
        }
    }
}