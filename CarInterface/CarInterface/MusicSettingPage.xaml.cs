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
        bool setRadStation = false;
        int bass = 0;
        public MusicSettingPage()
        {
            this.InitializeComponent();
            this.helper = new Helper();
            Manager.manager = new Manager();

            this.helper = new Helper();

            this.lblBass.Text = this.bass.ToString();

            manager = Manager.manager;
            this.dlTune.Angle = (360 * (manager.currentStation / 6.0));
            this.dlVolume.Angle = manager.volume;
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



        private void btBaseUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.bass < 10)
            {
                this.bass += 1;
                this.lblBass.Text = this.bass.ToString();
            }
        }

        private void btBassDown_Click(object sender, RoutedEventArgs e)
        {
            if (this.bass > -10)
            {
                this.bass -= 1;
                this.lblBass.Text = this.bass.ToString();
            }
        }

        private void btUpLeft_Click(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Left-Arrow_clicked.png"));
            this.btUpLeft.Background = brush;

            var brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Arrow.png"));
            this.btUp.Background = brush1;

            var brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Right-Arrow.png"));
            this.btUpRight.Background = brush2;

            var brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Right-Arrow.png"));
            this.btRight.Background = brush3;

            var brush4 = new ImageBrush();
            brush4.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Left-Arrow.png"));
            this.btLeft.Background = brush4;

            var brush5 = new ImageBrush();
            brush5.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Left-Arrow.png"));
            this.btDownLeft.Background = brush5;

            var brush6 = new ImageBrush();
            brush6.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Arrow.png"));
            this.btDown.Background = brush6;

            var brush7 = new ImageBrush();
            brush7.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Right-Arrow.png"));
            this.btDownRight.Background = brush7;
        }

        private void btUp_Click(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Arrow_clicked.png"));
            this.btUp.Background = brush;

            var brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Left-Arrow.png"));
            this.btUpLeft.Background = brush1;

            var brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Right-Arrow.png"));
            this.btUpRight.Background = brush2;

            var brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Right-Arrow.png"));
            this.btRight.Background = brush3;

            var brush4 = new ImageBrush();
            brush4.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Left-Arrow.png"));
            this.btLeft.Background = brush4;

            var brush5 = new ImageBrush();
            brush5.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Left-Arrow.png"));
            this.btDownLeft.Background = brush5;

            var brush6 = new ImageBrush();
            brush6.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Arrow.png"));
            this.btDown.Background = brush6;

            var brush7 = new ImageBrush();
            brush7.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Right-Arrow.png"));
            this.btDownRight.Background = brush7;
        }

        private void btUpRight_Click(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Right-Arrow_clicked.png"));
            this.btUpRight.Background = brush;

            var brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Left-Arrow.png"));
            this.btUpLeft.Background = brush1;

            var brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Arrow.png"));
            this.btUp.Background = brush2;

            var brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Right-Arrow.png"));
            this.btRight.Background = brush3;

            var brush4 = new ImageBrush();
            brush4.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Left-Arrow.png"));
            this.btLeft.Background = brush4;

            var brush5 = new ImageBrush();
            brush5.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Left-Arrow.png"));
            this.btDownLeft.Background = brush5;

            var brush6 = new ImageBrush();
            brush6.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Arrow.png"));
            this.btDown.Background = brush6;

            var brush7 = new ImageBrush();
            brush7.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Right-Arrow.png"));
            this.btDownRight.Background = brush7;
        }

        private void btLeft_Click(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Left-Arrow_clicked.png"));
            this.btLeft.Background = brush;

            var brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Left-Arrow.png"));
            this.btUpLeft.Background = brush1;

            var brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Arrow.png"));
            this.btUp.Background = brush2;

            var brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Right-Arrow.png"));
            this.btRight.Background = brush3;

            var brush4 = new ImageBrush();
            brush4.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Right-Arrow.png"));
            this.btUpRight.Background = brush4;

            var brush5 = new ImageBrush();
            brush5.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Left-Arrow.png"));
            this.btDownLeft.Background = brush5;

            var brush6 = new ImageBrush();
            brush6.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Arrow.png"));
            this.btDown.Background = brush6;

            var brush7 = new ImageBrush();
            brush7.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Right-Arrow.png"));
            this.btDownRight.Background = brush7;
        }

        private void btMid_Click(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Left-Arrow.png"));
            this.btLeft.Background = brush;

            var brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Left-Arrow.png"));
            this.btUpLeft.Background = brush1;

            var brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Arrow.png"));
            this.btUp.Background = brush2;

            var brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Right-Arrow.png"));
            this.btRight.Background = brush3;

            var brush4 = new ImageBrush();
            brush4.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Right-Arrow.png"));
            this.btUpRight.Background = brush4;

            var brush5 = new ImageBrush();
            brush5.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Left-Arrow.png"));
            this.btDownLeft.Background = brush5;

            var brush6 = new ImageBrush();
            brush6.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Arrow.png"));
            this.btDown.Background = brush6;

            var brush7 = new ImageBrush();
            brush7.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Right-Arrow.png"));
            this.btDownRight.Background = brush7;
        }

        private void btRight_Click(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Right-Arrow_clicked.png"));
            this.btRight.Background = brush;

            var brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Left-Arrow.png"));
            this.btUpLeft.Background = brush1;

            var brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Arrow.png"));
            this.btUp.Background = brush2;

            var brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Left-Arrow.png"));
            this.btLeft.Background = brush3;

            var brush4 = new ImageBrush();
            brush4.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Right-Arrow.png"));
            this.btUpRight.Background = brush4;

            var brush5 = new ImageBrush();
            brush5.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Left-Arrow.png"));
            this.btDownLeft.Background = brush5;

            var brush6 = new ImageBrush();
            brush6.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Arrow.png"));
            this.btDown.Background = brush6;

            var brush7 = new ImageBrush();
            brush7.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Right-Arrow.png"));
            this.btDownRight.Background = brush7;
        }

        private void btDownLeft_Click(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Left-Arrow_clicked.png"));
            this.btDownLeft.Background = brush;

            var brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Left-Arrow.png"));
            this.btUpLeft.Background = brush1;

            var brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Arrow.png"));
            this.btUp.Background = brush2;

            var brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Left-Arrow.png"));
            this.btLeft.Background = brush3;

            var brush4 = new ImageBrush();
            brush4.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Right-Arrow.png"));
            this.btUpRight.Background = brush4;

            var brush5 = new ImageBrush();
            brush5.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Right-Arrow.png"));
            this.btRight.Background = brush5;

            var brush6 = new ImageBrush();
            brush6.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Arrow.png"));
            this.btDown.Background = brush6;

            var brush7 = new ImageBrush();
            brush7.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Right-Arrow.png"));
            this.btDownRight.Background = brush7;
        }

        private void btDown_Click(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Arrow_clicked.png"));
            this.btDown.Background = brush;

            var brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Left-Arrow.png"));
            this.btUpLeft.Background = brush1;

            var brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Arrow.png"));
            this.btUp.Background = brush2;

            var brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Left-Arrow.png"));
            this.btLeft.Background = brush3;

            var brush4 = new ImageBrush();
            brush4.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Right-Arrow.png"));
            this.btUpRight.Background = brush4;

            var brush5 = new ImageBrush();
            brush5.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Right-Arrow.png"));
            this.btRight.Background = brush5;

            var brush6 = new ImageBrush();
            brush6.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Left-Arrow.png"));
            this.btDownLeft.Background = brush6;

            var brush7 = new ImageBrush();
            brush7.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Right-Arrow.png"));
            this.btDownRight.Background = brush7;
        }

        private void btDownRight_Click(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Right-Arrow_clicked.png"));
            this.btDownRight.Background = brush;

            var brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Left-Arrow.png"));
            this.btUpLeft.Background = brush1;

            var brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Arrow.png"));
            this.btUp.Background = brush2;

            var brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Left-Arrow.png"));
            this.btLeft.Background = brush3;

            var brush4 = new ImageBrush();
            brush4.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Up-Right-Arrow.png"));
            this.btUpRight.Background = brush4;

            var brush5 = new ImageBrush();
            brush5.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Right-Arrow.png"));
            this.btRight.Background = brush5;

            var brush6 = new ImageBrush();
            brush6.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Left-Arrow.png"));
            this.btDownLeft.Background = brush6;

            var brush7 = new ImageBrush();
            brush7.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down-Arrow.png"));
            this.btDown.Background = brush7;
        }
    }
}
