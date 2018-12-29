using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Hardware;
using System;

namespace StepCounter
{
    [Activity(Label = "Step Counter", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ISensorEventListener
    {
        private SensorManager sensorManager;
        private TextView steps;
        private bool Run = false;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            throw new System.NotImplementedException();
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (Run)
            {
                steps.SetText((int)Math.Ceiling(e.Values[0]));
            }
        }

        public new void OnResume()
        {
            Run = true;
            Sensor StepCount = sensorManager.GetDefaultSensor(SensorType.StepCounter);
            if (StepCount != null)
            {
                sensorManager.RegisterListener(this, StepCount, SensorDelay.Ui);
            }
            else
            {
                Toast.MakeText(this, "No Sensor Found!", ToastLength.Short).Show();
            }
        }

        public new void OnPause()
        {
            Run = false;
            sensorManager.UnregisterListener(this);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            steps = FindViewById<TextView>(Resource.Id.steps);
            sensorManager = (SensorManager)GetSystemService(SensorService);
        }
    }
}