using System;
using System.ComponentModel;

namespace Pcbuilder.Mobile.Services;

public interface IAuthService : INotifyPropertyChanged
{
    bool IsLoggedIn { get; }
    void Login();
    void Logout();
}

public class AuthService : IAuthService
{
    private bool _isLoggedIn;

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        private set
        {
            if (_isLoggedIn != value)
            {
                _isLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }
    }

    public void Login()
    {
        IsLoggedIn = true;
    }

    public void Logout()
    {
        IsLoggedIn = false;
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
