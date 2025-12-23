using System.ComponentModel;
using System.Windows.Input;
using Pcbuilder.Mobile.Services;

namespace Pcbuilder.Mobile.ViewModels;

public class ProfileViewModel : INotifyPropertyChanged
{
    private readonly IAuthService _authService;

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsLoggedIn => _authService.IsLoggedIn;
    public bool IsLoggedOut => !IsLoggedIn;

    public ICommand LoginCommand { get; }
    public ICommand LogoutCommand { get; }
    public ICommand ViewSavedBuildsCommand { get; }

    public ProfileViewModel(IAuthService authService)
    {
        _authService = authService;
        _authService.PropertyChanged += OnAuthServicePropertyChanged;

        LoginCommand = new Command(OnLogin);
        LogoutCommand = new Command(OnLogout);
        ViewSavedBuildsCommand = new Command(OnViewSavedBuilds);
    }

    private void OnAuthServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IAuthService.IsLoggedIn))
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(IsLoggedOut));
        }
    }

    private void OnLogin()
    {
        _authService.Login();
    }

    private void OnLogout()
    {
        _authService.Logout();
    }

    private void OnViewSavedBuilds()
    {
        // Placeholder for navigation or action
        Console.WriteLine("Navigating to Saved Builds...");
        // In a real app, you would use Shell.Current.GoToAsync(...)
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
