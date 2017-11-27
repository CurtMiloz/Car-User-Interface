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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CarInterface
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MediaPage : Page
    {

        String[] playlist = { "Playlist 1", "Playlist 2", "Playlist 3", "Playlist 4", "Playlist 5", "Awesome..."};
        String[] song = { "Song 1", "Song 2", "Song 3", "Song 4", "Song 5", "Song 6", "Heat of the Moment" };
        String[] by = { "by: 1", "by: 2", "by: 3", "by: 4", "by: 5", "by: 6", "by: Asia" };


        //This is the booleans for the button logic 
        bool setPlaylistButtons = false;
        

        Helper helper;
        Manager manager;
        public MediaPage()
        {
            this.InitializeComponent();
            this.helper = new Helper();

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




        private void setPlaylist(int playlistPos)
        {
            manager.currSong = 0;
            this.lblSong.Text = song[manager.currSong];
            this.lblArtist.Text = by[manager.currSong];
            this.lblPlaylist.Text = playlist[playlistPos];
            this.manager.currentPlaylist = playlistPos;
            
            
        }

        private void setSong(int songPos) {
            this.lblSong.Text = song[songPos];
            this.lblArtist.Text = by[songPos];

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btCall_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CallPage));
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

            this.setPlaylist(dlTune.Amount);

        }

        private void Grid_ManipulationAir(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            this.dlAir.Angle = Helper.GetAngle(e.Position.X - this.dlAir.Margin.Left,
                e.Position.Y - this.dlAir.Margin.Top, this.dlAir.RenderSize);

            this.dlAir.Amount = (int)(this.dlAir.Angle / 360 * 10);
           manager.setAir(this.dlAir.Amount);

        }

        private void btPlaylist1_Click(object sender, RoutedEventArgs e)
        {

            if (this.setPlaylistButtons)
            {
                manager.playlistMapping[0] = manager.currentPlaylist;
                this.btPlaylist1.Content = playlist[manager.currentPlaylist];
                this.setPlaylistButtons = false;
                Helper.MessageBoxAsync("Radio playlist set to: " + playlist[manager.currentPlaylist], "Radio playlist");
            }
            else
            {
                this.setPlaylist(manager.playlistMapping[0]);
            }

        }

        private void btPlaylist2_Click(object sender, RoutedEventArgs e)
        {

            if (this.setPlaylistButtons)
            {
                manager.playlistMapping[1] = manager.currentPlaylist;
                this.btPlaylist2.Content = playlist[manager.currentPlaylist];
                this.setPlaylistButtons = false;
                Helper.MessageBoxAsync("Radio playlist set to: " + playlist[manager.currentPlaylist], "Radio playlist");
            }
            else
            {
                this.setPlaylist(manager.playlistMapping[1]);

            }
        }

        private void btPlaylist3_Click(object sender, RoutedEventArgs e)
        {

            if (this.setPlaylistButtons)
            {
                manager.playlistMapping[2] = manager.currentPlaylist;
                this.btPlaylist3.Content = playlist[manager.currentPlaylist];
                this.setPlaylistButtons = false;
                Helper.MessageBoxAsync("Radio playlist set to: " + playlist[manager.currentPlaylist], "Radio playlist");
            }
            else
            {
                this.setPlaylist(manager.playlistMapping[2]);
            }
        }

        private void btPlaylist4_Click(object sender, RoutedEventArgs e)
        {

            if (this.setPlaylistButtons)
            {
                manager.playlistMapping[3] = manager.currentPlaylist;
                this.btPlaylist4.Content = playlist[manager.currentPlaylist];
                this.setPlaylistButtons = false;
                Helper.MessageBoxAsync("Radio playlist set to: " + playlist[manager.currentPlaylist], "Radio playlist");
            }
            else
            {
                this.setPlaylist(manager.playlistMapping[3]);
            }
        }

        private void btPlaylist5_Click(object sender, RoutedEventArgs e)
        {

            if (this.setPlaylistButtons)
            {
                manager.playlistMapping[4] = manager.currentPlaylist;
                this.btPlaylist5.Content = playlist[manager.currentPlaylist];
                this.setPlaylistButtons = false;
                Helper.MessageBoxAsync("Radio playlist set to: " + playlist[manager.currentPlaylist], "Radio playlist");
            }
            else
            {
                this.setPlaylist(manager.playlistMapping[4]);
            }
        }



        private void btTuneForward_Click(object sender, RoutedEventArgs e)
        {
            manager.currSong = (manager.currSong + 1) % this.playlist.Length;

            this.setSong(manager.currSong);
        }

        private void btTuneBack_Click(object sender, RoutedEventArgs e)
        {
            manager.currSong = (manager.currSong - 1);

            if (manager.currSong < 0)
            {
                manager.currSong = this.song.Length - 1;
            }
            this.setSong(manager.currSong);
        }

        private void btsetPlaylist_Click(object sender, RoutedEventArgs e)
        {
            this.setPlaylistButtons = true;

            Helper.MessageBoxAsync("Click the button to preset playlist", "Playlist pre-sets");
        }

        private void btSetSong_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btAir_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AirPage));
        }

        private void btMedia_Click(object sender, RoutedEventArgs e)
        {
           manager.swapRadioButton();
            this.Frame.Navigate(typeof(MainPage));
        }

        private void btSettings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }
    }

}
