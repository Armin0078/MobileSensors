using Microsoft.Extensions.Logging;
using Plugin.Maui.ScreenRecording;

namespace MobileSensors
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseScreenRecording()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif

			builder.Services.AddTransient<MainPage>();

			return builder.Build();
		}
	}
}
