using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace MobileSensors.MVVM.ViewModel
{
	public partial class SensorsViewModel : ObservableObject
	{ 
		public string? generalText;

		readonly Stopwatch stopwatch = new();
		TimeSpan timeSpan;
		bool isStarted = false;

		[ObservableProperty]
		string? lblGyroscopeColor;

		[ObservableProperty]
		string? lblGyroscopeText;

		[ObservableProperty]
		string? lblAccelerometerColor;

		[ObservableProperty]
		string? lblAccelerometerText;

		[ObservableProperty]
		string? lblCompassColor;

		[ObservableProperty]
		string? lblCompassText;

		[ObservableProperty]
		string? lblMagnetometerColor;

		[ObservableProperty]
		string? lblMagnetometerText;


		public SensorsViewModel()
		{
			Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
			Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
			Compass.ReadingChanged += Compass_ReadingChanged;
			Magnetometer.ReadingChanged += Magnetometer_ReadingChanged;
		}
		public void OnOffSensors()
		{
			if (isStarted == false)
			{
				timeSpan = TimeSpan.Zero;
				isStarted = true;
				stopwatch.Reset();
				stopwatch.Start();
				Task.Run(() => MobileSensors());

			}
			else
			{
				Task.Run(() => MobileSensors());
				stopwatch.Stop();
				Clipboard.SetTextAsync(generalText);
				isStarted = false;
			}
		}

		public void MobileSensors()
		{
			Gyroscope_Clicked();
			Accelerometer_Clicked();
			Compass_Clicked();
			Magnetometer_Clicked();

			//Task,Run(() => Gyroscope_Clicked());
			//Task.Run(() => Accelerometer_Clicked());
			//Task.Run(() => Compass_Clicked());
			//Task.Run(() => Magnetometer_Clicked());
		}

		public void Gyroscope_Clicked()
		{
			try
			{
				if (Gyroscope.Default.IsMonitoring)
					Gyroscope.Default.Stop();
				else
					Gyroscope.Default.Start(SensorSpeed.UI);		
			}
			catch (FeatureNotSupportedException)
			{
				LblGyroscopeColor = "Red";
				LblGyroscopeText = "Your Device Dose Not Support Gyroscope Sensor";
			}
			catch (Exception e)
			{
				LblGyroscopeColor = "Red";
				LblGyroscopeText = $"Gyroscope: {e.Message}";
			}
		}
		void Gyroscope_ReadingChanged(object? sender, GyroscopeChangedEventArgs e)
		{
			LblGyroscopeColor = "Green";
			LblGyroscopeText = $"Gyroscope: {e.Reading.AngularVelocity}";

			timeSpan = stopwatch.Elapsed;
			string text = $"{timeSpan}---Gyroscope:-{e.Reading.AngularVelocity}";
			generalText += text + Environment.NewLine;
		}


		public void Accelerometer_Clicked()
		{
			try
			{
				if (Accelerometer.Default.IsMonitoring)
					Accelerometer.Default.Stop();
				else
					Accelerometer.Default.Start(SensorSpeed.UI);
			}
			catch (FeatureNotSupportedException)
			{
				LblAccelerometerColor = "Red";
				LblAccelerometerText = "Your Device Dose Not Support Accelerometer Sensor";
			}
			catch (Exception e)
			{
				LblAccelerometerColor = "Red";
				LblAccelerometerText = $"Accelerometer: {e.Message}";
			}
		}
		void Accelerometer_ReadingChanged(object? sender, AccelerometerChangedEventArgs e)
		{
			LblAccelerometerColor = "Green";
			LblAccelerometerText = $"Accelerometer: {e.Reading.Acceleration}";

			timeSpan = stopwatch.Elapsed;
			string text = $"{timeSpan}---Accelerometer:-{e.Reading.Acceleration}";
			generalText += text + Environment.NewLine;
		}

		public void Compass_Clicked()
		{
			try
			{
				if (Compass.Default.IsMonitoring)
					Compass.Default.Stop();
				else
					Compass.Default.Start(SensorSpeed.UI);
			}
			catch (FeatureNotSupportedException)
			{
				LblCompassColor = "Red";
				LblCompassText = "Your Device Dose Not Support Compass Sensor";
			}
			catch (Exception e)
			{
				LblCompassColor = "Red";
				LblCompassText = $"Compass: {e.Message}";
			}
		}
		private void Compass_ReadingChanged(object? sender, CompassChangedEventArgs e)
		{
			LblCompassColor = "Green";
			LblCompassText = $"Compass: {e.Reading.HeadingMagneticNorth}";

			timeSpan = stopwatch.Elapsed;
			string text = $"{timeSpan}---Compass:-{e.Reading.HeadingMagneticNorth}";
			generalText += text + Environment.NewLine + Environment.NewLine;
		}
		public void Magnetometer_Clicked()
		{
			try
			{
				if (Magnetometer.Default.IsMonitoring)
					Magnetometer.Default.Stop();
				else
					Magnetometer.Default.Start(SensorSpeed.UI);
			}
			catch (FeatureNotSupportedException)
			{
				LblMagnetometerColor = "Red";
				LblMagnetometerText = "Your Device Dose Not Support Magnetometer Sensor";
			}
			catch (Exception e)
			{
				LblMagnetometerColor = "Red";
				LblMagnetometerText = $"Magnetometer: {e.Message}";
			}
		}
		private void Magnetometer_ReadingChanged(object? sender, MagnetometerChangedEventArgs e)
		{
			LblMagnetometerColor = "Green";
			LblMagnetometerText = $"Magnetometer: {e.Reading.MagneticField}";

			timeSpan = stopwatch.Elapsed;
			string text = $"{timeSpan}---Magnetometer:-{e.Reading.MagneticField}";
			generalText += text + Environment.NewLine;
		}
	}
}

