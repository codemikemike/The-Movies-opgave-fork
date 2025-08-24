using System;
using System.Windows.Input;

namespace The_Movie.Commands
{
    // RelayCommand implementering for ren MVVM (MOTOREN)
    // ICommand = "kontrakt" der definerer hvordan kommandoer fungerer
    // ICommand har to dele, en i RelayCommand og en i ViewModel
    public class RelayCommand : ICommand
    {
        // _execute = selve handlingen der skal udføres (Execute metoden)
        private readonly Action<object> _execute;

        // _canExecute = tjekker om kommandoen kan udføres (CanExecute metoden)
        private readonly Func<object, bool> _canExecute;

        // Constructor - modtager execute handling og optional canExecute tjek
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            // Execute er påkrævet - throw exception hvis null
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));

            // CanExecute er optional - kan være null
            _canExecute = canExecute;
        }

        // Event der fortæller UI hvornår CanExecute skal checkes igen
        // CommandManager.RequerySuggested = WPF system der automatisk checker CanExecute
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }     // Tilmeld til WPF's system
            remove { CommandManager.RequerySuggested -= value; }  // Afmeld fra WPF's system
        }

        // Kan kommandoen udføres? (true/false)
        // Afgør om knap er enabled/disabled
        public bool CanExecute(object parameter)
        {
            // Hvis ingen CanExecute funktion = altid true (knap altid enabled)
            // Ellers kald _canExecute funktionen og returner resultatet
            return _canExecute == null || _canExecute(parameter);
        }

        // Udfør kommandoen (selve handlingen)
        // Dette kaldes når bruger klikker på knappen
        public void Execute(object parameter)
        {
            // Kald den handling der blev givet i constructor
            _execute(parameter);
        }
    }
}