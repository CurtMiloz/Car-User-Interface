using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInterface
{

    
    class Manager
    {

        public String[] station = { "97.7", "98.5", "99.9", "101.1", "102.1", "103.1" };
        public String[] song = { "Song 1", "Song 2", "Song 3", "Song 4", "Song 5", "Song 6" };
        public String[] by = { "by: 1", "by: 2", "by: 3", "by: 4", "by: 5", "by: 6" };



        public static Manager manager;


        public bool mute = false;
        public int volume = 5;
        public int airLeft = 23;
        public int airRight = 23;
        public bool RadioButton = true;
        public int air = 0;
        public string navImage = "";

        public bool acOn = false;
        public bool rearDefrost = false;
        public bool frontDefrost = false;
        public bool intCirc = false;
        public bool hazards = false;

        public int currentStation = 4;
        public int[] radioMapping = { 4, 3, 0, 2, 1 };

        public int currentPlaylist = 5;
        public int currSong = 0;
        public int[] playlistMapping = { 0, 1, 2, 3, 4 };


        public bool setRadStation = false;
        public bool leftSeatHeater = false;
        public bool rightSeatHeater = false;
        public int directionCounter = 0;

        public int fadeMode = 10;
        public int bass = 0;


        public bool connected = false;

        enum Screen {Radio, Media, Air, Settings, Call, Music_Setting, Connect_Settings};


        Screen screen = Screen.Radio;
        
        public Manager()
        {
            screen = Screen.Radio;
            this.directionCounter = 0;
            this.navImage =  "Assets/icons/map.png";
        }


        public void swapRadioButton() {
            RadioButton = !RadioButton;
        }

        public bool getRadioButton() {
            return RadioButton;
        }

        public void setVolume(int Amount) {
            
            if(Amount <= 100 && Amount >= 0) {
                volume = Amount;
               
            }
            if (!mute) {
                Helper.MessageBoxQuickAsync(volume.ToString(), "Volume");
            }
            
        }

        public void setAir(int Amount)
        {

            if (!mute && Amount <= 10 && Amount >= 0) { air = Amount; }

            Helper.MessageBoxQuickAsync(air.ToString(), "Air");
        }

      
        public void volMute() {
            mute = !mute;
            if (mute)
            {
                Helper.MessageBoxAsync("Off", "Volume");
            }
            else {
                Helper.MessageBoxAsync("On", "Volume");
            }
        }

        public void leftTempUp()
        {

            if (airLeft < 30) { airLeft = airLeft + 1; }
            Helper.MessageBoxAsync(airLeft.ToString(), "Left Side Temperature");
        }

        public void leftTempDown()
        {
            if (airLeft > 16) { airLeft = airLeft - 1; }
            Helper.MessageBoxAsync(airLeft.ToString(), "Left Side Temperature");
        }


        public void rightTempUp()
        {

            if (airRight < 30) { airRight = airRight + 1; }
            Helper.MessageBoxAsync(airRight.ToString(), "Right Side Temperature");
            
        }

        public void rightTempDown()
        {
            if (airRight > 16) { airRight = airRight - 1; }
            Helper.MessageBoxAsync(airRight.ToString(), "Right Side Temperature");
        }

    }
}
