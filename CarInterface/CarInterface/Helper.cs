using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Threading;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.Media.SpeechRecognition;

namespace CarInterface
{

    class Helper
    {
        SolidColorBrush red;
        SolidColorBrush green;
        static bool doneMessage = false;

        public Helper()
        {
            this.red = new SolidColorBrush();
            this.red.Color = Color.FromArgb(255, 255, 0, 0);
            this.green = new SolidColorBrush();
            this.green.Color = Color.FromArgb(255, 0, 255, 0);
        }

        public bool swapLed(Ellipse e, bool isGreen)
        {
            if (isGreen)
            {
                e.Fill = this.red;
                return false;
            }
            else
            {
                e.Fill = this.green;
                return true;
            }

        }
        public void setLed(Ellipse e, bool isGreen)
        {
            if (isGreen)
            {
                e.Fill = this.green;
            }
            else
            {
                e.Fill = this.red;
            }

        }

        public static async Task MessageBoxLongAsync(String message, String title)
        {


            ContentDialog dialog = new ContentDialog()
            {
                Title = title,

            };
            var panel = new StackPanel();

            panel.Children.Add(new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
            });
            dialog.Content = panel;


            var task = dialog.ShowAsync();
            await Task.Delay(TimeSpan.FromSeconds(2));
            dialog.Hide();



        }

        public static async Task MessageBoxAsync(String message, String title)
        {


            ContentDialog dialog = new ContentDialog()
            {
                Title = title,

            };
            var panel = new StackPanel();

            panel.Children.Add(new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
            });
            dialog.Content = panel;

            
            var task = dialog.ShowAsync();
            await Task.Delay(TimeSpan.FromSeconds(1));
            dialog.Hide();
            
            

        }


        public static async Task MessageBoxQuickAsync(String message, String title)
        {


            ContentDialog dialog = new ContentDialog()
            {
                Title = title,

            };
            var panel = new StackPanel();

            panel.Children.Add(new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
            });
            dialog.Content = panel;


            var task = dialog.ShowAsync();
            await Task.Delay(TimeSpan.FromSeconds(0.3));
            dialog.Hide();



        }



        public enum Quardrants : int { nw = 2, ne = 1, sw = 4, se = 3 }
        public static double GetAngle(double touchPoint_X, double touchPoint_Y, Size circleSize)
        {
            var _X = touchPoint_X - (circleSize.Width / 2d);
            var _Y = circleSize.Height - touchPoint_Y
                - (circleSize.Height / 2d);

            var _Hypot = Math.Sqrt(_X * _X + _Y * _Y);
            var _Value = Math.Asin(_Y / _Hypot) * 180 / Math.PI;
            var _Quadrant = (_X >= 0) ?
                (_Y >= 0) ? Quardrants.ne : Quardrants.se :
                (_Y >= 0) ? Quardrants.nw : Quardrants.sw;

            switch (_Quadrant)
            {
                case Quardrants.ne: _Value = 090 - _Value; break;
                case Quardrants.nw: _Value = 270 + _Value; break;
                case Quardrants.se: _Value = 090 - _Value; break;
                case Quardrants.sw: _Value = 270 + _Value; break;
            }
            return _Value;
        }

    }



}
