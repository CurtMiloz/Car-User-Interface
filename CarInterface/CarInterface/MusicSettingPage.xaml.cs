using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class MusicSettingPage : Page
    {

        //This is the booleans for the button logic 
        Helper helper;
        Manager manager;
        
        public MusicSettingPage()
        {
            this.InitializeComponent();

            this.helper = new Helper();

            
            manager = Manager.manager;
            this.lblBass.Text = this.manager.bass.ToString();

            this.dlTune.Angle = (360 * (manager.currentStation / 6.0));
            this.dlVolume.Angle = 360*(manager.volume/100.0);
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
            setClickedFade();
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



        private void btBaseUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.manager.bass < 10)
            {
                this.manager.bass += 1;
                this.lblBass.Text = this.manager.bass.ToString();
            }
        }

        private void btBassDown_Click(object sender, RoutedEventArgs e)
        {
            if (this.manager.bass > -10)
            {
                this.manager.bass -= 1;
                this.lblBass.Text = this.manager.bass.ToString();
            }
        }


        private void btDownRight_Click(object sender, RoutedEventArgs e)
        {
            manager.fadeMode = 0;
            setClickedFade();
        }


        private void btUpLeft_Click(object sender, RoutedEventArgs e)
        {
            manager.fadeMode = 1;
            setClickedFade();
        }

        private void btLeft_Click(object sender, RoutedEventArgs e)
        {
            manager.fadeMode = 3;
            setClickedFade();
        }

        private void btUpRight_Click(object sender, RoutedEventArgs e)
        {
            manager.fadeMode = 4;
            setClickedFade();
        }
        private void btUp_Click(object sender, RoutedEventArgs e)
        {
            manager.fadeMode = 2;
            setClickedFade();
        }
       

        private void btMid_Click(object sender, RoutedEventArgs e)
        {
            manager.fadeMode = 10;
            setClickedFade();
        }

        private void btRight_Click(object sender, RoutedEventArgs e)
        {
            manager.fadeMode = 5;
            setClickedFade();
        }

        private void btDownLeft_Click(object sender, RoutedEventArgs e)
        {
            manager.fadeMode = 6;
            setClickedFade();
        }

        private void btDown_Click(object sender, RoutedEventArgs e)
        {
            manager.fadeMode = 7;
            setClickedFade();
        }


        void setClickedFade() {
            String[] nameList = { "btDownRight", "btUpLeft", "btUp", "btLeft", "btUpRight", "btRight", "btDownLeft","btDown" };
            String[] imagesRef = { "Assets/icons/Down-Right-Arrow", "Assets/icons/Up-Left-Arrow",
                "Assets/icons/Up-Arrow", "Assets/icons/Left-Arrow", "Assets/icons/Up-Right-Arrow",
                "Assets/icons/Right-Arrow", "Assets/icons/Down-Left-Arrow", "Assets/icons/Down-Arrow" };
            for (int i =0; i < nameList.Length; i++)
            {
                Button button = (Button)this.FindName(nameList[i]);
                var brush = new ImageBrush();
                if (manager.fadeMode == i)
                {
                    brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, imagesRef[i] +"_clicked.png"));
                }
                else {
                    brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, imagesRef[i] + ".png"));

                }

                button.Background = brush;
            }
           

        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }
    }
}
