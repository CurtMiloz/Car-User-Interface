using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CarInterface
{
    public sealed partial class Dial : UserControl, INotifyPropertyChanged
    {
       
        public Dial()
        {
            this.InitializeComponent();
            this.DataContext = this;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;


       /* private void Grid_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            this.Angle = GetAngle(e.Position, this.RenderSize);

            this.Amount = (int)(this.Angle / 360 * 100);

            Manager.setManager(this);
        }
        */
        double m_Angle = default(double);
        public double Angle { get { return m_Angle; } set { SetAngle(ref m_Angle, value); } }

        int m_Amount = default(int);
        public int Amount { get { return m_Amount; } set { SetAmount(ref m_Amount, value); } }

        private void SetAngle(ref double m_Angle, double value)
        {
            m_Angle = value;
            PropertyChanged(this, new PropertyChangedEventArgs("Angle"));
        }

        private void SetAmount(ref int m_Amount, int value)
        {
            m_Amount = value;
            PropertyChanged(this, new PropertyChangedEventArgs("Amount"));

            
        }

    }
}
