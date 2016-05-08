using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUFOController
{
    class LedApi
    {

        public MagicUFOController.LedTCPControl ledControl; 

        public LedApi(string ArgsIPs) 
        {
            ledControl = new MagicUFOController.LedTCPControl(ArgsIPs);
        }

        public enum FlashMode
        {
            Gradual = 0x3a,
            Jumping = 0x3b,
            Strobe = 0x3c
        }
        public void GetStatus(string ipAddress)
        {
            string commandString = "818A8B";
            LEDStatus status = ledControl.GetStatus(commandString, ipAddress);
        }

        public void TurnOn()
        {
            string commandString = "71230F";
            ledControl.SendGroupCommand(commandString);
        }
        public void TurnOff()
        {
            string commandString = "71240F";
            ledControl.SendGroupCommand(commandString);
        }

        private string BuildColorString(int red, int green, int blue, int warmWhite)
        {
            string commandString = "31" + red.ToString("X2") + green.ToString("X2") + blue.ToString("X2") + warmWhite.ToString("X2");
            commandString += "00" + "0f";
            return commandString;
        }


        // Does custom fades between up to 16 colors
        // speed needs to be a number between 1 and 30
        private string BuildCustomFadeCommand(LedColor[] colors, FlashMode mode, int speed)
        {
            string commandString = "";
            // Command is 0x51 for custom fade
            int command = 0x51;

            // Up to 16 colors can be supported;
            int totalColors = 16;
            int speedStart = 30;

            commandString += command.ToString("X2");
            for (int i = 0; i < colors.Length; i++)
            {
                commandString += colors[i].red.ToString("X2") + colors[i].green.ToString("X2") + colors[i].blue.ToString("X2") + colors[i].warmWhite.ToString("X2");
            }

            // API requires all 16 colors sent.  Send 01 02 03 00 for the empty colors
            for (int j = 0; j < totalColors - colors.Length; ++j)
            {
                commandString += "01020300";
            }

            // 0 is fastest, 1F is slowest
            commandString += (speedStart - speed).ToString("X2");

            commandString += ((int)mode).ToString("X2");

            // Closing for command
            commandString += "FF0f";

            return commandString;
        }

        public void SetColor(int red, int green, int blue, int warmWhite)
        {

            ledControl.SendGroupCommand(BuildColorString(red, green, blue, warmWhite));
        }

        public void SetColor(LedColor color)
        {

            ledControl.SendGroupCommand(BuildColorString(color.red, color.green, color.blue, color.warmWhite));
        }

        public void SetColor(string ipAddress, LedColor color)
        {

            ledControl.SendCommand(BuildColorString(color.red, color.green, color.blue, color.warmWhite),ipAddress);
        }

        public void SetColor(string ipAddress, int red, int green, int blue, int warmWhite)
        {

            ledControl.SendCommand(BuildColorString(red, green, blue, warmWhite), ipAddress);
        }

        public void CustomFades(LedColor[] colors, FlashMode mode, int speed)
        {
            ledControl.SendGroupCommand(BuildCustomFadeCommand(colors, mode, speed));
        }

        static string convertDecToTwoDigitHex(int number)
        {
            return string.Format("{0:x1}{1:x1}", (number & 0xff00) >> 8, number & 0xff);
        }

        public void SetRandomColor(double brightness)
        {
            LedColor color = new LedColor();
            color.SetRandomColor(brightness/100.0);
            SetColor(color);
        }

        public void SetRandomColor()
        {
            LedColor color = new LedColor();
            color.SetRandomColor((double).5);
            SetColor(color);
        }

        public void SetBrightness(int brightness)
        {
            string commandString = "818A8B";

            foreach (string ipAddress in ledControl.ipAddresses)
            {
                LEDStatus status = ledControl.GetStatus(commandString, ipAddress);

                int biggest;

                if (status.red > status.green)
                    biggest = status.red;
                else
                    biggest = status.green;

                if (status.blue > biggest)
                    biggest = status.blue;

                double overallBrightness = (float)biggest;

                double redAmount = (brightness / 100.0) * (255 * status.red
                    / overallBrightness);
                double greenAmount = (brightness / 100.0) * 255 * status.green / overallBrightness;
                double blueAmount = (brightness / 100.0) * 255 * status.blue / overallBrightness;
                double whiteAmount = (status.white > 0) ? (brightness / 100.0) * 255 : 0;

                SetColor(ipAddress, (int)redAmount, (int)greenAmount, (int)blueAmount, (int)whiteAmount);
            }
        }

    }
}
