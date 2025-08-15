// Views/MovieDialog.xaml.cs
using System;
using System.Linq;
using System.Windows;
using The_Movie.Models;

namespace The_Movie.Views
{
    public partial class MovieDialog : Window
    {
        public Movie? ResultMovie { get; private set; }
        public bool Success { get; private set; }

        public MovieDialog()
        {
            InitializeComponent();
            LoadGenres();

            // Set focus to title field
            TitleTextBox.Focus();
        }

        public MovieDialog(Movie existingMovie) : this()
        {
            // For editing existing movie
            Title = "Rediger Film";
            TitleTextBox.Text = existingMovie.Name;
            DurationTextBox.Text = existingMovie.Duration.ToString();
            GenreComboBox.SelectedItem = existingMovie.Genre;
            IdTextBlock.Text = $"ID: {existingMovie.Id}";
        }

        private void LoadGenres()
        {
            var genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToArray();
            GenreComboBox.ItemsSource = genres;
            GenreComboBox.SelectedIndex = 0; // Select first genre as default
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                MessageBox.Show("Titel er påkrævet!", "Validering Fejl",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                TitleTextBox.Focus();
                return;
            }

            if (!int.TryParse(DurationTextBox.Text, out int duration) || duration <= 0)
            {
                MessageBox.Show("Varighed skal være et tal større end 0!", "Validering Fejl",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                DurationTextBox.Focus();
                return;
            }

            if (GenreComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vælg en genre!", "Validering Fejl",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                GenreComboBox.Focus();
                return;
            }

            // Create the movie object
            ResultMovie = new Movie
            {
                Id = 0, // Will be set by the calling code
                Name = TitleTextBox.Text.Trim(),
                Duration = duration,
                Genre = (Genre)GenreComboBox.SelectedItem
            };

            Success = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Success = false;
            Close();
        }
    }
}