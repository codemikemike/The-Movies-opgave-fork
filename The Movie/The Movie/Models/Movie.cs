// Models/Movie.cs
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace The_Movie.Models
{
    public class Movie : INotifyPropertyChanged
    {
        private int _id;
        private string _name = string.Empty;
        private int _duration;
        private Genre _genre;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value ?? string.Empty;
                OnPropertyChanged();
            }
        }

        public int Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }

        public Genre Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged();
            }
        }

        public string DurationFormatted => $"{Duration} min";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}