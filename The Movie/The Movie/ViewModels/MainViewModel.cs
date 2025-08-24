using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using The_Movie.Models;
using The_Movie.Commands;

namespace The_Movie.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string titleName = "Enter movie title...";

        public string TitleName
        {
            get { return titleName; }
            set
            {
                titleName = value;
                OnPropertyChanged(nameof(TitleName));

                // Opdater FullTitle automatisk
                fullTitle = $"{titleName} ({duration} min)";
                OnPropertyChanged(nameof(FullTitle));

                // Trigger CanExecute check for SaveCommand
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private int duration = 0;

        public int Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged(nameof(Duration));

                // Opdater FullTitle automatisk
                fullTitle = $"{titleName} ({duration} min)";
                OnPropertyChanged(nameof(FullTitle));

                // Trigger CanExecute check for SaveCommand
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private MovieGenre? genre = MovieGenre.Action;

        public MovieGenre? Genre
        {
            get { return genre; }
            set
            {
                genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        private string fullTitle = "Enter movie title... (0 min)";

        public string FullTitle
        {
            get { return fullTitle; }
            set
            {
                fullTitle = value;
                OnPropertyChanged(nameof(FullTitle));
            }
        }

        // ICommand implementation - REN MVVM!
        public ICommand SaveCommand { get; private set; }

        public MainViewModel()
        {
            // Initialisér SaveCommand med RelayCommand
            SaveCommand = new RelayCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
        }

        // Command execution logic - forretningslogik i ViewModel hvor den hører hjemme
        private void ExecuteSaveCommand(object parameter)
        {
            // Gem movie logik
            var movieInfo = $"Movie saved!\n" +
                           $"Title: {TitleName}\n" +
                           $"Duration: {Duration} min\n" +
                           $"Genre: {Genre?.ToString() ?? "Not selected"}";

            MessageBox.Show(movieInfo, "Movie Saved", MessageBoxButton.OK, MessageBoxImage.Information);

            // Her kunne du kalde en service til at gemme til database, fil etc.
            // SaveToDatabase();
            // SaveToFile();
        }

        // Command can execute logic - afgør om knappen er enabled
        private bool CanExecuteSaveCommand(object parameter)
        {
            // Knappen er kun enabled hvis titel og duration er udfyldt
            return !string.IsNullOrWhiteSpace(TitleName) &&
                   !TitleName.Equals("Enter movie title...", StringComparison.OrdinalIgnoreCase) &&
                   Duration > 0;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}