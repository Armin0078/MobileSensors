using MobileSensors.MVVM.ViewModel;
using Plugin.Maui.ScreenRecording;


namespace MobileSensors
{
	public partial class MainPage : ContentPage
	{
		readonly IScreenRecording screenRecording;
		readonly SensorsViewModel sensorViewModel = new();
		public MainPage(IScreenRecording screenRecording)
		{
			InitializeComponent();
			BindingContext = new SensorsViewModel();

			this.screenRecording = screenRecording;
			btnStop.IsVisible = false;
		}
		private void CameraView_CamerasLoaded(object sender, EventArgs e)
		{
			cameraView.Camera = cameraView.Cameras.First();

			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await cameraView.StopCameraAsync();
				await cameraView.StartCameraAsync();
			});
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
				sensorViewModel.OnOffSensors();
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
				sensorViewModel.OnOffSensors();
				ScreenRecordingFile? screenResult = await screenRecording.StopRecording();
				
				btnStart.IsVisible = true;
				btnStop.IsVisible = false;
				if (screenResult != null)
				{
					FileInfo f = new(screenResult.FullPath);
					await Shell.Current.DisplayAlert("File Created", $"Path: {screenResult.FullPath} Size: {f.Length:N0} bytes", "OK");
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
