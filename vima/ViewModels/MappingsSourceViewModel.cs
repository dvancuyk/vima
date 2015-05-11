using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using vima.Commands;

namespace vima.ViewModels
{
    public class MappingsSourceViewModel
    {
        #region : Properties :

        public ICollection<MappingViewModel> Mappings { get; set; }
        public string FileName { get; set; }

        #endregion

        #region : Stuff :

        public MappingsSourceViewModel()
        {
            CommandBindings = new CommandBindingCollection();
            
            AssignCommandBinding(new AddVideoFilesCommand(this));
        }
        
        public CommandBindingCollection CommandBindings { get; }

        #endregion

        #region : Helpers :

        private void AssignCommandBinding<TCommand>(TCommand command)
            where TCommand : ICommand, IRoutedCommand
        {
            var binding = new CommandBinding(command, command.Executed, command.CanExecute);
            CommandManager.RegisterClassCommandBinding(GetType(), binding);

            //Adds the binding to the CommandBindingCollection
            CommandBindings.Add(binding);
        }

        #endregion
    }

    public class AttachedProperties : DependencyObject
    {
        public static DependencyProperty RegisterCommandBindingsProperty =
            DependencyProperty.RegisterAttached("RegisterCommandBindings",
            typeof(CommandBindingCollection),
            typeof(AttachedProperties),
            new PropertyMetadata(null, OnRegisterCommandBindingChanged));

        public CommandBindingCollection RegisterCommandBindings { get; set; }

        public static void SetRegisterCommandBindings(UIElement element, CommandBindingCollection value)
        {
            element?.SetValue(RegisterCommandBindingsProperty, value);
        }

        public static CommandBindingCollection GetRegisterCommandBindings(UIElement element)
        {
            return (CommandBindingCollection)element?.GetValue(RegisterCommandBindingsProperty);
        }

        private static void OnRegisterCommandBindingChanged
        (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var element = sender as UIElement;
            if (element == null)
            {
                return;
            }

            var bindings = (e.NewValue as CommandBindingCollection);
            if (bindings == null)
            {
                return;
            }

            // clear the collection first
            element.CommandBindings.Clear();
            element.CommandBindings.AddRange(bindings);
        }
    }
}
