using Pcbuilder.Mobile.ViewModels;

namespace Pcbuilder.Mobile.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfileViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
