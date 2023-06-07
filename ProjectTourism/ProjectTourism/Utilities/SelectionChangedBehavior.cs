using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace ProjectTourism.Utilities
{
    public class SelectionChangedBehavior : Behavior<UIElement>
    {
        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(SelectionChangedBehavior),
            new PropertyMetadata(null));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AddHandler(
                Selector.SelectionChangedEvent,
                new RoutedEventHandler(OnSelectionChanged));
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.RemoveHandler(
                Selector.SelectionChangedEvent,
                new RoutedEventHandler(OnSelectionChanged));
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (Command?.CanExecute(null) == true)
                Command.Execute(null);
        }
    }
}
