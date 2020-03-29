/*
 * My StepCounter:
 * Copyright (C) 2014 Refractored LLC | http://refractored.com
 * James Montemagno | http://twitter.com/JamesMontemagno | http://MotzCod.es
 * 
 * Michael James | http://twitter.com/micjames6 | http://micjames.co.uk/
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using Android.App;
using Android.Hardware;
using Android.Content;
using System.ComponentModel;
using Android.OS;
using vMe.Droid.Implementations;
using vMe.Services;
using Android.Content.PM;
#if PRO
using Android.Support.V4.App;
#endif
using Android.Graphics;

namespace vMe.Droid.Implementations
{
	[Service(Enabled = true)]
	[IntentFilter(new String[]{ "using Android.Content;" })]
	public class StepService : Service, ISensorEventListener, INotifyPropertyChanged
	{
		private StepKeeper steps = new StepKeeper();
		private bool isRunning;
		private Int64 stepsToday = 0;
		public bool WarningState {
			get;
			set;
		}

		public Int64 StepsToday {
			get{ return stepsToday; }
			set{
				if (stepsToday == value)
					return;

				stepsToday = value;
				OnPropertyChanged ("StepsToday");
			}
		}

		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			Console.WriteLine ("StartCommand Called, setting alarm");
			#if DEBUG
			Android.Util.Log.Debug ("STEPSERVICE", "Start command result called, incoming startup");
			#endif

			var alarmManager = ((AlarmManager)ApplicationContext.GetSystemService (Context.AlarmService));
			var intent2 = new Intent (this, typeof(StepService));
			intent2.PutExtra ("warning", WarningState);
			var stepIntent = PendingIntent.GetService (ApplicationContext, 10, intent2, PendingIntentFlags.UpdateCurrent);
			// Workaround as on Android 4.4.2 START_STICKY has currently no
			// effect
			// -> restart service every 60 mins
			alarmManager.Set(AlarmType.Rtc, Java.Lang.JavaSystem
				.CurrentTimeMillis() + 1000 * 60 * 60, stepIntent);

			var warning = false;
			if (intent != null)
				warning = intent.GetBooleanExtra ("warning", false);
			Startup ();

			return StartCommandResult.Sticky;
		}

	

		public override void OnTaskRemoved (Intent rootIntent)
		{
			base.OnTaskRemoved (rootIntent);

			UnregisterListeners ();
			#if DEBUG
			Console.WriteLine ("OnTaskRemoved Called, setting alarm for 500 ms");
			Android.Util.Log.Debug ("STEPSERVICE", "Task Removed, going down");
			#endif
			var intent = new Intent (this, typeof(StepService));
			intent.PutExtra ("warning", WarningState);
			// Restart service in 500 ms
			((AlarmManager) GetSystemService(Context.AlarmService)).Set(AlarmType.Rtc, Java.Lang.JavaSystem
				.CurrentTimeMillis() + 500,
				PendingIntent.GetService(this, 11, intent, 0));
		}

		bool started = false;
        public void StartupSensor()
        {
            if (!started)
            {
				started = true;
                Console.WriteLine();
            }
        }

		public static bool IsKitKatWithStepCounter(PackageManager pm)
		{

			// Require at least Android KitKat
			int currentApiVersion = (int)Build.VERSION.SdkInt;
			// Check that the device supports the step counter and detector sensors
			return currentApiVersion >= 19
				&& pm.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepCounter)
				&& pm.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepDetector);

		}

		private void Startup(bool warning = false)
		{
			//check if kit kat can sensor compatible
			if (!IsKitKatWithStepCounter(PackageManager))
			{

				Console.WriteLine("Not compatible with sensors, stopping service.");
				StopSelf();
				return;
			}

			if (!isRunning) {
				RegisterListeners (warning ? SensorType.StepDetector : SensorType.StepCounter);
				WarningState = warning;
			}

			isRunning = true;
		}

		public override void OnDestroy ()
		{
			base.OnDestroy ();
			UnregisterListeners ();
			isRunning = false;
		}

		void RegisterListeners(SensorType sensorType) {
			Console.WriteLine("Register Listener 3366");
			var sensorManager = (SensorManager) GetSystemService(Context.SensorService);
			var sensor = sensorManager.GetDefaultSensor(sensorType);

			//get faster why not, nearly fast already and when
			//sensor gets messed up it will be better
			sensorManager.RegisterListener(this, sensor, SensorDelay.Normal);
			Console.WriteLine("Sensor listener registered of type: " + sensorType);

		}


		void UnregisterListeners() {

			if (!isRunning)
				return;

			try{
			var sensorManager = (SensorManager) GetSystemService(Context.SensorService);
			sensorManager.UnregisterListener(this);
			Console.WriteLine("Sensor listener unregistered.");
			#if DEBUG
			Android.Util.Log.Debug ("STEPSERVICE", "Sensor listener unregistered.");
			#endif
				isRunning = false;
			}
			catch(Exception ex) {
				#if DEBUG
				Android.Util.Log.Debug ("STEPSERVICE", "Unable to unregister: " + ex);
				#endif
			}
		}

		public void OnAccuracyChanged (Sensor sensor, SensorStatus accuracy)
		{
			//do nothing here
		}

		public void AddSteps(Int64 count){
			
			//if service rebooted or rebound then this will null out to 0, but count will still be since last boot.
			Console.WriteLine("add steps " + count);

			int stepCount = steps.RobotCounts;

			stepCount = Convert.ToInt32(count) + stepCount;

			steps.RobotCounts = stepCount;		
			
			Console.WriteLine ("New step detected by STEP_COUNTER sensor. Total step count: " + stepCount);			
		}

		public void OnSensorChanged (SensorEvent e)
		{
			Console.WriteLine("Step Detected!");
			AddSteps(1);
			switch (e.Sensor.Type) {

			case SensorType.StepCounter:			

				AddSteps (1);

				break;
			case SensorType.StepDetector:
				AddSteps (1);
				break;
			}
		}

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string name)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

