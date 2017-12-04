using System;
using System.Diagnostics;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409



namespace CarInterface
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
     

        //This is the booleans for the button logic 
        Helper helper;
        Manager manager;
        bool setRadStation = false;
        public MainPage()
        {
            this.InitializeComponent();
            this.helper = new Helper();

            if (Manager.manager == null) {
                Manager.manager = new Manager();
            }
            

            this.helper = new Helper();

            manager = Manager.manager;
            this.dlTune.Angle = (360 * (manager.currentStation / 6.0));
            this.dlVolume.Angle = 360 * (manager.volume / 100.0);
            this.dlAir.Angle = 360 * (manager.air / 10.0);
            helper.setLed(this.ledAC, manager.acOn);
            helper.setLed(this.ledRearDefrost, manager.rearDefrost);
            helper.setLed(this.ledFrontDefrost, manager.frontDefrost);
            helper.setLed(this.ledIntCirc, manager.intCirc);
            helper.setLed(this.ledHazard, manager.hazards);
            this.btMedia.Content = "Media";
            setStation(manager.currentStation);
        }

        private void setStation(int stationPos) {
            
            this.lblSong.Text = manager.song[stationPos];
            this.lblArtist.Text = manager.by[stationPos];
            this.lblStation.Text = manager.station[stationPos];
            manager.currentStation = stationPos;
            if (stationPos == 0)
            {
                this.dlTune.Angle = 0;
            }
            else
            {
                this.dlTune.Angle = (360 * (stationPos / 6.0));
            }
            this.dlTune.Amount = stationPos;
        }




     
        

        private void btFrontDefrost_Click(object sender, RoutedEventArgs e)
        {
            manager.frontDefrost = helper.swapLed(this.ledFrontDefrost, manager.frontDefrost);

        }

        private void btTempLU_Click(object sender, RoutedEventArgs e)
        {
            manager.leftTempUp();
        }

        private void btTempLD_Click(object sender, RoutedEventArgs e)
        {
            manager.leftTempDown();
        }
        private void btTempRU_Click(object sender, RoutedEventArgs e)
        {
            manager.rightTempUp();

        }
        private void btTempRD_Click(object sender, RoutedEventArgs e)
        {
            manager.rightTempDown(); 
        }


        private void btMute_Click(object sender, RoutedEventArgs e)
        {
            manager.volMute();
        }

        private void btAC_Click(object sender, RoutedEventArgs e)
        {
            manager.acOn = helper.swapLed(this.ledAC, manager.acOn);
        }

        private void btRearDefrost_Click(object sender, RoutedEventArgs e)
        {
            manager.rearDefrost = helper.swapLed(this.ledRearDefrost, manager.rearDefrost);
        }

        private void btIntCirc_Click(object sender, RoutedEventArgs e)
        {
            manager.intCirc = helper.swapLed(this.ledIntCirc, manager.intCirc);
        }

        private void btHazard_Click(object sender, RoutedEventArgs e)
        {
            manager.hazards = helper.swapLed(this.ledHazard, manager.hazards);
        }


        private void Grid_ManipulationVolume(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            this.dlVolume.Angle = Helper.GetAngle(e.Position.X - this.dlVolume.Margin.Left,
                e.Position.Y - this.dlVolume.Margin.Top, this.dlVolume.RenderSize);

            this.dlVolume.Amount = (int)(this.dlVolume.Angle / 360 * 100);
            manager.setVolume(dlVolume.Amount);
        }


        private void Grid_ManipulationTune(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            this.dlTune.Angle = Helper.GetAngle(e.Position.X - this.dlTune.Margin.Left,
                e.Position.Y - this.dlTune.Margin.Top, this.dlTune.RenderSize);

            this.dlTune.Amount = (int)(this.dlTune.Angle / 360 * 6);

            this.setStation(dlTune.Amount );

        }

        private void Grid_ManipulationAir(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            this.dlAir.Angle = Helper.GetAngle(e.Position.X - this.dlAir.Margin.Left,
                e.Position.Y - this.dlAir.Margin.Top, this.dlAir.RenderSize);

            this.dlAir.Amount = (int)(this.dlAir.Angle / 360 * 10);
            manager.setAir(this.dlAir.Amount);

        }

        private void btRadio1_Click(object sender, RoutedEventArgs e)
        {

            if (this.setRadStation)
            {
                manager.radioMapping[0] = manager.currentStation;
                this.btRadio1.Content = manager.station[manager.currentStation];
                this.setRadStation = false;
                Helper.MessageBoxAsync("Radio Station set to: " + manager.station[manager.currentStation], "Radio Station");
            }
            else {
                this.setStation(manager.radioMapping[0]);
            }
            
        }

        private void btRadio2_Click(object sender, RoutedEventArgs e)
        {

            if (this.setRadStation)
            {
                manager.radioMapping[1] = manager.currentStation;
                this.btRadio2.Content = manager.station[manager.currentStation];
                this.setRadStation = false;
                Helper.MessageBoxAsync("Radio Station set to: " + manager.station[manager.currentStation], "Radio Station");
            }
            else
            {
                this.setStation(manager.radioMapping[1]);

            }
        }

        private void btRadio3_Click(object sender, RoutedEventArgs e)
        {

            if (this.setRadStation)
            {
                manager.radioMapping[2] = manager.currentStation;
                this.btRadio3.Content = manager.station[manager.currentStation];
                this.setRadStation = false;
                Helper.MessageBoxAsync("Radio Station set to: " + manager.station[manager.currentStation], "Radio Station");
            }
            else
            {
                this.setStation(manager.radioMapping[2]);
            }
        }

        private void btRadio4_Click(object sender, RoutedEventArgs e)
        {

            if (this.setRadStation)
            {
                manager.radioMapping[3] = manager.currentStation;
                this.btRadio4.Content = manager.station[manager.currentStation];
                this.setRadStation = false;
                Helper.MessageBoxAsync("Radio Station set to: " + manager.station[manager.currentStation], "Radio Station");
            }
            else
            {
                this.setStation(manager.radioMapping[3]);
            }
        }

        private void btRadio5_Click(object sender, RoutedEventArgs e)
        {

            if (this.setRadStation)
            {
                manager.radioMapping[4] = manager.currentStation;
                this.btRadio5.Content = manager.station[manager.currentStation];
                this.setRadStation = false;
                Helper.MessageBoxAsync("Radio Station set to: " + manager.station[manager.currentStation], "Radio Station");
            }
            else
            {
                this.setStation(manager.radioMapping[4]);
            }
        }

        private void btSet_Click(object sender, RoutedEventArgs e)
        {
            this.setRadStation = true;

            Helper.MessageBoxAsync("Click the button to preset", "Radio pre-sets");
        }

        private void btTuneForward_Click(object sender, RoutedEventArgs e)
        {
            manager.currentStation = (manager.currentStation +1)% manager.station.Length;

            this.setStation(manager.currentStation);
        }

        private void btTuneBack_Click(object sender, RoutedEventArgs e)
        {
            manager.currentStation = (manager.currentStation -1);

            if (manager.currentStation <0) {
                manager.currentStation =manager.station.Length - 1;
            }
            this.setStation(manager.currentStation);
        }
        private void btMedia_Click(object sender, RoutedEventArgs e)
        {
            manager.swapRadioButton();
            this.Frame.Navigate(typeof(MediaPage));
        }

        private void btCall_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CallPage));
        }

        private void btAir_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AirPage));
        }

        private void btSettings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }

        private void btNav_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NavPage));
        }

    }


}
