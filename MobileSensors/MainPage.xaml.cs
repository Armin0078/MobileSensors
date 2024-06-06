using Plugin.Maui.ScreenRecording;


namespace MobileSensors
{
	public partial class MainPage : ContentPage
	{
		readonly IScreenRecording screenRecording;
		public MainPage(IScreenRecording screenRecording)
		{
			InitializeComponent();
			this.screenRecording = screenRecording;
			btnStop.IsVisible = false;
		}

		async void StartRecordingClicked(object sender, EventArgs e)
		{
			if (!screenRecording.IsSupported)
			{
				await DisplayAlert("Not Supported", "Screen recording is not supported", "OK");
				return;
			}
			try
			{
				ScreenRecordingOptions options = new()
				{
					EnableMicrophone = true,
					SaveToGallery = true
				};

				screenRecording.StartRecording(options);
				btnStart.IsVisible = false;
				btnStop.IsVisible = true;
			}
			catch
			{
				await DisplayAlert("Error","Can't Start Record","Ok");
			}
		}

		async void StopRecordingClicked(object sender, EventArgs e)
		{
			try
			{
				ScreenRecordingFile screenResult = await screenRecording.StopRecording();
				btnStart.IsVisible = true;
				btnStop.IsVisible = false;
				if (screenResult != null)
				{
					FileInfo f = new(screenResult.FullPath);
					await Shell.Current.DisplayAlert("File Created", $"Path: {screenResult.FullPath} Size: {f.Length.ToString("N0")} bytes", "OK");
				}
				else
				{
					await Shell.Current.DisplayAlert("No Screen Recording", "NADA", "OK");
				}
			}
			catch
			{
				await Shell.Current.DisplayAlert("No Screen Recording", "NADA", "OK");
			}
			

			
		}
	}
}
