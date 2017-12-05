using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CarInterface
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavPage : Page
    {
        Helper helper;
        Manager manager;
        bool setRadStation = false;
        public NavPage()
        {
            this.InitializeComponent();

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
            if (manager.getRadioButton())
            {
                this.btMedia.Content = "Radio";
            }
            else
            {
                this.btMedia.Content = "Media";
            }
            loadMap();
            
        }

        private void setStation(int stationPos)
        {

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

            this.setStation(dlTune.Amount);

        }

        private void Grid_ManipulationAir(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            this.dlAir.Angle = Helper.GetAngle(e.Position.X - this.dlAir.Margin.Left,
                e.Position.Y - this.dlAir.Margin.Top, this.dlAir.RenderSize);

            this.dlAir.Amount = (int)(this.dlAir.Angle / 360 * 10);
            manager.setAir(this.dlAir.Amount);

        }


        private void btMedia_Click(object sender, RoutedEventArgs e)
        {
            if (manager.getRadioButton())
            {
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                this.Frame.Navigate(typeof(MediaPage));
            }
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

        private async void btTouchToSpeakAsync(object sender, RoutedEventArgs e)
        {
            await manager.RecordSpeechFromMicrophoneAsync("Say where to navigate to");

            if (manager.VoiceResult.Contains("navigate")|| manager.VoiceResult.Contains("gas"))
            {
                manager.navImage = "Assets/icons/route-map.png";
                loadMap();

            }
           
        }

       

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            manager.navImage = "Assets/icons/map.png";
            loadMap();


        }


        private void loadMap() {
            BitmapImage brush = new BitmapImage(new Uri(this.BaseUri, manager.navImage));
            this.btMap.Source = brush;
            if (manager.navImage.Equals("Assets/icons/route-map.png"))
            {
                btCancel.IsEnabled = true;

                this.txtNav.Visibility = Visibility.Visible;
            }
            else {
                btCancel.IsEnabled = false;

                this.txtNav.Visibility = Visibility.Collapsed;

            }

        }
        private void txtNav_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

      
    }
}
