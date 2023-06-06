using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace ProjectTourism.Utilities
{
    public class SelectedDateChangedBehavior : Behavior<DatePicker>
    {
        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(SelectedDateChangedBehavior),
            new PropertyMetadata(null));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectedDateChanged += OnSelectedDateChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectedDateChanged -= OnSelectedDateChanged;
        }

        private void OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Command?.CanExecute(null) == true)
                Command.Execute(null);
        }
    }
}
