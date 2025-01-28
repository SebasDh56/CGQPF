using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http.Json;
using AguaApp.CGQModels;

namespace AguaApp.CGQViewModel
{
    public class CGQSearchViewModel : INotifyPropertyChanged
    {
        private string _searchQuery;
        private User _selectedUser;
        private bool _isSearching;

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
            }
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public bool IsSearching
        {
            get => _isSearching;
            set
            {
                _isSearching = value;
                OnPropertyChanged();
            }
        }

        public Command SearchCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CGQSearchViewModel()
        {
            SearchCommand = new Command(async () => await SearchUser());
        }

        private async Task SearchUser()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return;

            IsSearching = true;

            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetFromJsonAsync<RandomUserResponse>("https://randomuser.me/api/?results=50");

                var user = response?.Results.FirstOrDefault(u =>
                    u.Name.First.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

                if (user != null)
                {
                    SelectedUser = new User
                    {
                        Name = $"{user.Name.First} {user.Name.Last}",
                        Email = user.Email,
                        Picture = user.Picture.Large
                    };
                }
                else
                {
                    SelectedUser = null;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
            }
            finally
            {
                IsSearching = false;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class RandomUserResponse
        {
            public List<RandomUser> Results { get; set; }
        }

        public class RandomUser
        {
            public RandomUserName Name { get; set; }
            public string Email { get; set; }
            public RandomUserPicture Picture { get; set; }
        }

        public class RandomUserName
        {
            public string First { get; set; }
            public string Last { get; set; }
        }

        public class RandomUserPicture
        {
            public string Large { get; set; }
        }
    }
}
