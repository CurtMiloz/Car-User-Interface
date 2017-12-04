using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class AirPage : Page
    {
 
            //This is the booleans for the button logic 
            Helper helper;
            Manager manager;
           
            public AirPage()
            {
                this.InitializeComponent();
                this.helper = new Helper();

                this.manager = Manager.manager;
                this.dlTune.Angle = (360 * (manager.currentStation / 6.0));
            this.dlVolume.Angle = 360 * (manager.volume / 100.0);
            this.dlAir.Angle = 360 * (manager.air / 10.0);
                helper.setLed(this.ledAC, manager.acOn);
                helper.setLed(this.ledRearDefrost, manager.rearDefrost);
                helper.setLed(this.ledFrontDefrost, manager.frontDefrost);
                helper.setLed(this.ledIntCirc, manager.intCirc);
                helper.setLed(this.ledHazard, manager.hazards);
                helper.setLed(this.ledLeftSeatHeater, manager.leftSeatHeater);
                helper.setLed(this.ledRightSeatHeater, manager.rightSeatHeater);
                 lblRightSideTemp.Text = "Right Set Temp: " + manager.airRight.ToString() + "°C";

                 lblLeftSideTemp.Text = "Left Set Temp: " + manager.airLeft.ToString() + "°C";
            setFanImage();

            Canvas.SetTop(recLeftLevel, Canvas.GetTop(recLeftLevel) + (23 - manager.airLeft) * 30);
            Canvas.SetTop(recRightLevel, Canvas.GetTop(recRightLevel) + (23 - manager.airRight) * 30);
            
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
                if (manager.airLeft < 30)
                {
                    double y = Canvas.GetTop(recLeftLevel);
                    y -= 30;
                    Canvas.SetTop(recLeftLevel, y);
                }
                manager.leftTempUp();
                lblLeftSideTemp.Text = "Left Set Temp: " + manager.airLeft.ToString() + "°C";
            }

            private void btTempLD_Click(object sender, RoutedEventArgs e)
            {
                if (manager.airLeft > 16)
                {
                    double y = Canvas.GetTop(recLeftLevel);
                    y += 30;
                    Canvas.SetTop(recLeftLevel, y);
                }
                manager.leftTempDown();
                lblLeftSideTemp.Text = "Left Set Temp: " + manager.airLeft.ToString() + "°C";
            }
            private void btTempRU_Click(object sender, RoutedEventArgs e)
            {
                if (manager.airRight < 30)
                {
                    double y = Canvas.GetTop(recRightLevel);
                    y -= 30;
                    Canvas.SetTop(recRightLevel, y);
                }
                manager.rightTempUp();
                lblRightSideTemp.Text = "Right Set Temp: " + manager.airRight.ToString() + "°C";

            }
            private void btTempRD_Click(object sender, RoutedEventArgs e)
            {
                if (manager.airRight > 16)
                {
                    double y = Canvas.GetTop(recRightLevel);
                    y += 30;
                    Canvas.SetTop(recRightLevel, y);
                }
                manager.rightTempDown();
                lblRightSideTemp.Text = "Right Set Temp: " + manager.airRight.ToString() + "°C";
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


 

        private void lblInsideTemp_Copy_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void btHeatedLeft_Click(object sender, RoutedEventArgs e)
        {
            helper.swapLed(this.ledLeftSeatHeater, manager.leftSeatHeater);
            manager.leftSeatHeater = !manager.leftSeatHeater;
        }

        private void btFanPos_Click(object sender, RoutedEventArgs e)
        {

            manager.directionCounter += 1;
            setFanImage();


        }

        private void btHeatedRight_Click(object sender, RoutedEventArgs e)
        {

            helper.swapLed(this.ledRightSeatHeater, manager.rightSeatHeater);
            manager.rightSeatHeater = !manager.rightSeatHeater;
        }

        private void btLeftUpTouch_Click(object sender, RoutedEventArgs e)
        {
            if (manager.airLeft < 30)
            {
                double y = Canvas.GetTop(recLeftLevel);
                y -= 30;
                Canvas.SetTop(recLeftLevel, y);
            }
            manager.leftTempUp();
            lblLeftSideTemp.Text = "Left Set Temp: " + manager.airLeft.ToString() + "°C";
          
            
            
        }

        private void btLeftDownTouch_Click(object sender, RoutedEventArgs e)
        {
            if (manager.airLeft > 16)
            {
                double y = Canvas.GetTop(recLeftLevel);
                y += 30;
                Canvas.SetTop(recLeftLevel, y);
            }
            manager.leftTempDown();
            lblLeftSideTemp.Text = "Left Set Temp: " + manager.airLeft.ToString() + "°C";
            
        }

        private void btRightUpTouch_Click(object sender, RoutedEventArgs e)
        {
            if (manager.airRight < 30)
            {
                double y = Canvas.GetTop(recRightLevel);
                y -= 30;
                Canvas.SetTop(recRightLevel, y);
            }
            manager.rightTempUp();
            lblRightSideTemp.Text = "Right Set Temp: " + manager.airRight.ToString() + "°C";
           
        }

        private void btRightDownTouch_Click(object sender, RoutedEventArgs e)
        {
            if (manager.airRight > 16)
            {
                double y = Canvas.GetTop(recRightLevel);
                y += 30;
                Canvas.SetTop(recRightLevel, y);
            }
            manager.rightTempDown();
            lblRightSideTemp.Text = "Right Set Temp: " + manager.airRight.ToString() + "°C";
            
        }

        private void setFanImage() {
            if (manager.directionCounter % 3 == 0)
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/up.png"));
                this.btFanPos.Background = brush;
            }
            else if (manager.directionCounter % 3 == 1)
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down_and_up.png"));
                this.btFanPos.Background = brush;
            }
            else
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/icons/Down.png"));
                this.btFanPos.Background = brush;
            }
        }

       
    }


    }
