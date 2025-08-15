// ViewModels/MainViewModel.cs
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using The_Movie.Commands;
using The_Movie.Models;

namespace The_Movie.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Movie? _selectedMovie;
        private string _searchText = string.Empty;

        public ObservableCollection<Movie> Movies { get; }
        public ObservableCollection<Movie> FilteredMovies { get; }

        public Movie? SelectedMovie
        {
            get => _selectedMovie;
            set => SetProperty(ref _selectedMovie, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterMovies();
                }
            }
        }

        // Commands
        public RelayCommand AddMovieCommand { get; }
        public RelayCommand<Movie> EditMovieCommand { get; }
        public RelayCommand<Movie> DeleteMovieCommand { get; }
        public RelayCommand RefreshCommand { get; }

        public MainViewModel()
        {
            Movies = new ObservableCollection<Movie>();
            FilteredMovies = new ObservableCollection<Movie>();

            // Initialize commands with debug
            AddMovieCommand = new RelayCommand(AddMovie);
            EditMovieCommand = new RelayCommand<Movie>(EditMovie, movie => movie != null);
            DeleteMovieCommand = new RelayCommand<Movie>(DeleteMovie, movie => movie != null);
            RefreshCommand = new RelayCommand(RefreshMovies);

            // Load sample data
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            Movies.Clear();

            // Sample movies based on your database
            var sampleMovies = new[]
            {
                new Movie { Id = 1, Name = "1917", Duration = 119, Genre = Genre.War },
                new Movie { Id = 2, Name = "The Wife", Duration = 99, Genre = Genre.Drama },
                new Movie { Id = 3, Name = "Ayka", Duration = 100, Genre = Genre.Drama },
                new Movie { Id = 4, Name = "Avengers: Endgame", Duration = 181, Genre = Genre.Action },
                new Movie { Id = 5, Name = "The Lion King", Duration = 118, Genre = Genre.Family }
            };

            foreach (var movie in sampleMovies)
            {
                Movies.Add(movie);
            }

            FilterMovies();
        }

        private void FilterMovies()
        {
            FilteredMovies.Clear();

            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? Movies
                : Movies.Where(m =>
                    m.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    m.Genre.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            foreach (var movie in filtered)
            {
                FilteredMovies.Add(movie);
            }
        }

        private void AddMovie(object? parameter)
        {
            try
            {
                MessageBox.Show("AddMovie command called!", "Debug",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                var newMovie = new Movie
                {
                    Id = Movies.Any() ? Movies.Max(m => m.Id) + 1 : 1,
                    Name = $"Ny Film {DateTime.Now:HH:mm:ss}",
                    Duration = 120,
                    Genre = Genre.Action
                };

                Movies.Add(newMovie);
                FilterMovies();

                MessageBox.Show($"Film '{newMovie.Name}' tilføjet!", "Film Tilføjet",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl i AddMovie: {ex.Message}", "Fejl",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditMovie(Movie? movie)
        {
            try
            {
                MessageBox.Show($"EditMovie command called for: {movie?.Name ?? "null"}", "Debug",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                if (movie == null) return;

                movie.Name = $"{movie.Name} (Redigeret)";

                MessageBox.Show($"Film '{movie.Name}' redigeret!", "Film Redigeret",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl i EditMovie: {ex.Message}", "Fejl",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteMovie(Movie? movie)
        {
            try
            {
                MessageBox.Show($"DeleteMovie command called for: {movie?.Name ?? "null"}", "Debug",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                if (movie == null) return;

                var result = MessageBox.Show(
                    $"Er du sikker på at du vil slette '{movie.Name}'?",
                    "Bekræft Sletning",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Movies.Remove(movie);
                    FilterMovies();

                    MessageBox.Show($"Film '{movie.Name}' slettet!", "Film Slettet",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl i DeleteMovie: {ex.Message}", "Fejl",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshMovies(object? parameter)
        {
            try
            {
                MessageBox.Show("RefreshMovies command called!", "Debug",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                LoadSampleData();

                MessageBox.Show("Film liste opdateret!", "Opdateret",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl i RefreshMovies: {ex.Message}", "Fejl",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}