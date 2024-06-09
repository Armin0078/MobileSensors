using System.Diagnostics;

namespace MobileSensors.MVVM.ViewModel
{
	public class SensorsViewModel
	{
		public string? generalText;

		readonly Stopwatch stopwatch = new();
		TimeSpan timeSpan;
		bool isStarted = false;

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

		static public void MobileSensors()
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

		static public void Gyroscope_Clicked()
		{
			try
			{
				if (Gyroscope.Default.IsMonitoring)
					Gyroscope.Default.Stop();
				else
					Gyroscope.Default.Start(SensorSpeed.UI);
			}
			catch (FeatureNotSupportedException fnsEx)
			{
				
			}
			catch (Exception ex)
			{
				
			}
		}
		void Gyroscope_ReadingChanged(object? sender, GyroscopeChangedEventArgs e)
		{
			timeSpan = stopwatch.Elapsed;
			string text = $"{timeSpan}---Gyroscope:-{e.Reading.AngularVelocity}";
			generalText += text + Environment.NewLine;
		}


		static public void Accelerometer_Clicked()
		{
			try
			{
				if (Accelerometer.Default.IsMonitoring)
					Accelerometer.Default.Stop();
				else
					Accelerometer.Default.Start(SensorSpeed.UI);
			}
			catch (FeatureNotSupportedException fnsEx)
			{
				
			}
			catch (Exception ex)
			{
				
			}
		}
		void Accelerometer_ReadingChanged(object? sender, AccelerometerChangedEventArgs e)
		{
			timeSpan = stopwatch.Elapsed;
			string text = $"{timeSpan}---Accelerometer:-{e.Reading.Acceleration}";
			generalText += text + Environment.NewLine;
		}

		static public void Compass_Clicked()
		{
			try
			{
				if (Compass.Default.IsMonitoring)
					Compass.Default.Stop();
				else
					Compass.Default.Start(SensorSpeed.UI);
			}
			catch (FeatureNotSupportedException fnsEx)
			{
				
			}
			catch (Exception ex)
			{
				
			}
		}
		private void Compass_ReadingChanged(object? sender, CompassChangedEventArgs e)
		{
			timeSpan = stopwatch.Elapsed;
			string text = $"{timeSpan}---Compass:-{e.Reading.HeadingMagneticNorth}";
			generalText += text + Environment.NewLine + Environment.NewLine;
		}
		static public void Magnetometer_Clicked()
		{
			try
			{
				if (Magnetometer.Default.IsMonitoring)
					Magnetometer.Default.Stop();
				else
					Magnetometer.Default.Start(SensorSpeed.UI);
			}
			catch (FeatureNotSupportedException fnsEx)
			{
				
			}
			catch (Exception ex)
			{
				
			}
		}
		private void Magnetometer_ReadingChanged(object? sender, MagnetometerChangedEventArgs e)
		{
			timeSpan = stopwatch.Elapsed;
			string text = $"{timeSpan}---Magnetometer:-{e.Reading.MagneticField}";
			generalText += text + Environment.NewLine;
		}
	}
}

