using System.Windows;
using System.Windows.Input;

namespace GameBookView.Element
{
    public class ExitEventToCommandPipe
    {
        public static DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(ExitEventToCommandPipe),
            new FrameworkPropertyMetadata(null, CommandChanged));

        public static void SetCommand(DependencyObject target, ICommand value) =>target.SetValue(ExitEventToCommandPipe.CommandProperty, value);
        

        public static ICommand GetCommand(DependencyObject target) => (ICommand)target.GetValue(ExitEventToCommandPipe.CommandProperty);
        

        private static void CommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target is Window window)
            {
                window.Closed += (o, evt) => GetCommand(target)?.Execute(null);
            }
        }
    }

}
