// Views/MainWindow.xaml.cs
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using The_Movie.Models;
using The_Movie.ViewModels;

namespace The_Movie.Views
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel => (MainViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddMovieButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MovieDialog
            {
                Owner = this
            };

            var result = dialog.ShowDialog();

            if (dialog.Success && dialog.ResultMovie != null)
            {
                // Auto-generate ID
                dialog.ResultMovie.Id = ViewModel.Movies.Any() ? ViewModel.Movies.Max(m => m.Id) + 1 : 1;

                // Add to collection
                ViewModel.Movies.Add(dialog.ResultMovie);

                // Force update of filtered list
                ViewModel.FilteredMovies.Add(dialog.ResultMovie);

                // Select the new movie
                ViewModel.SelectedMovie = dialog.ResultMovie;

                MessageBox.Show($"Film '{dialog.ResultMovie.Name}' blev registreret med ID {dialog.ResultMovie.Id}!",
                    "Film Registreret", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditMovieButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedMovie == null)
            {
                MessageBox.Show("Vælg en film først!", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dialog = new MovieDialog(ViewModel.SelectedMovie)
            {
                Owner = this
            };

            var result = dialog.ShowDialog();

            if (dialog.Success && dialog.ResultMovie != null)
            {
                // Update the existing movie
                dialog.ResultMovie.Id = ViewModel.SelectedMovie.Id; // Keep original ID

                ViewModel.SelectedMovie.Name = dialog.ResultMovie.Name;
                ViewModel.SelectedMovie.Duration = dialog.ResultMovie.Duration;
                ViewModel.SelectedMovie.Genre = dialog.ResultMovie.Genre;

                MessageBox.Show($"Film '{dialog.ResultMovie.Name}' blev opdateret!",
                    "Film Redigeret", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteMovieButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedMovie == null)
            {
                MessageBox.Show("Vælg en film først!", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Er du sikker på at du vil slette '{ViewModel.SelectedMovie.Name}'?",
                "Bekræft Sletning",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var movieName = ViewModel.SelectedMovie.Name;
                var movieToDelete = ViewModel.SelectedMovie;

                // Remove from both collections
                ViewModel.Movies.Remove(movieToDelete);
                ViewModel.FilteredMovies.Remove(movieToDelete);

                MessageBox.Show($"Film '{movieName}' slettet!", "Succes",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // Rebuild filtered list from scratch
            ViewModel.FilteredMovies.Clear();

            var filtered = string.IsNullOrWhiteSpace(ViewModel.SearchText)
                ? ViewModel.Movies
                : ViewModel.Movies.Where(m =>
                    m.Name.Contains(ViewModel.SearchText, StringComparison.OrdinalIgnoreCase) ||
                    m.Genre.ToString().Contains(ViewModel.SearchText, StringComparison.OrdinalIgnoreCase));

            foreach (var movie in filtered)
            {
                ViewModel.FilteredMovies.Add(movie);
            }

            MessageBox.Show($"Liste opdateret! Viser {ViewModel.FilteredMovies.Count} film.", "Opdateret",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridRow row && row.DataContext is Movie movie)
            {
                EditMovieButton_Click(sender, new RoutedEventArgs());
            }
        }
    }
}