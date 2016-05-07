using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace MagicUFOController
{ 
    class LedCommandProcessor 
    {
        public MagicUFOController.LedTCPControl ledControl; 


        public LedCommandProcessor(string ArgsIPs) 
        {
            ledControl = new MagicUFOController.LedTCPControl(ArgsIPs);
        }

        public void ParseLine(string[] commands)
        {
            
            switch (commands[1])
            {

                case "SETCOLOR":
                    SetColor(Convert.ToInt32(commands[2]), Convert.ToInt32(commands[3]), Convert.ToInt32(commands[4]), Convert.ToInt32(commands[5]));
                    break;
                case "SETBRIGHTNESS":
                    SetBrightness(Convert.ToInt32(commands[2]));
                    break;
                case "TURNON":
                    TurnOn();
                    break;
                case "TURNOFF":
                    TurnOff();
                    break;
                case "GETSTATUS":
                    GetStatus(commands[2]);
                    break;

            }

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


        public void SetColor(int red, int green, int blue, int warmWhite)
        {
            string commandString = "31" + red.ToString("X2") + green.ToString("X2") + blue.ToString("X2") + warmWhite.ToString("X2");
            commandString += "00" + "0f";
            ledControl.SendGroupCommand(commandString);
        }

        public void SetColor(string ipAddress, int red, int green, int blue, int warmWhite)
        {
            string commandString = "31" + red.ToString("X2") + green.ToString("X2") + blue.ToString("X2") + warmWhite.ToString("X2");
            commandString += "00" + "0f";
            ledControl.SendCommand(commandString, ipAddress);
        }   


        static string convertDecToTwoDigitHex(int number)
        {
            return string.Format("{0:x1}{1:x1}", (number & 0xff00) >> 8, number & 0xff);
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

                SetColor(ipAddress,(int)redAmount, (int)greenAmount, (int)blueAmount, (int)whiteAmount);
            }
        }


        
  

    }
}
