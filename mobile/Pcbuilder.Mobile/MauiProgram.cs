using Microsoft.Extensions.Logging;
using Pcbuilder.Mobile.Services;
using Pcbuilder.Mobile.ViewModels;
using Pcbuilder.Mobile.Views;

namespace Pcbuilder.Mobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddTransient<ProfileViewModel>();
        builder.Services.AddTransient<ProfilePage>();

		return builder.Build();
	}
}
