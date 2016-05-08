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

        LedApi api;

        public LedCommandProcessor(string ipAddresses)
        {
            api = new LedApi(ipAddresses);
        }

        public void ParseLine(string[] commands)
        {
            if (commands.Length == 1)
            {
                InvalidCommand("No Command");
                return;
            }
                
            switch (commands[1])
            {

                case "SETCOLOR":
                    if (commands.Length!=6)
                    {
                        InvalidCommand("Invalid Arguments. Syntax is SETCOLOR RED GREEN BLUE WHITE");
                        return;
                    }
                    api.SetColor(Convert.ToInt32(commands[2]), Convert.ToInt32(commands[3]), Convert.ToInt32(commands[4]), Convert.ToInt32(commands[5]));
                    break;
                case "SETBRIGHTNESS":
                    if (commands.Length!=3)
                    {
                        InvalidCommand("Invalid Arguments. Syntax is SETBRIGHTNESS LEVEL where level is between 0 and 1");
                        return;
                    }
                    api.SetBrightness(Convert.ToInt32(commands[2]));
                    break;
                case "TURNON":
                    api.TurnOn();
                    break;
                case "TURNOFF":
                    api.TurnOff();
                    break;
                case "GETSTATUS":
                    api.GetStatus(commands[2]);
                    break;
                case "SETRANDOMCOLOR":
                    if (commands.Length>2)
                        api.SetRandomColor(Convert.ToDouble(commands[2]));
                    else
                        api.SetRandomColor();
                    break;
                case "CUSTOMFADES":
                    if (commands.Length != 5)
                    {
                        InvalidCommand("Invalid Arguments. Syntax is CUSTOMFADES COLORSET FADEMODE SPEED");
                        return;
                    }
                    ProcessFade(commands[2],commands[3],commands[4]);
                    break;
                default:
                    InvalidCommand("Invalid Agrument");
                    break;
            }
           
        }
            
            private void ProcessFade(string colorString, string flashString, string speed)
            {
                    MagicUFOController.LedApi.FlashMode flash = new LedApi.FlashMode();
                    
                    LedColor[] colors = BuildColors(colorString);

                    switch (flashString) {
                        case "GRADUAL":
                            flash = LedApi.FlashMode.Gradual;
                            break;
                        case "JUMPING":
                            flash = LedApi.FlashMode.Jumping;
                            break;
                        case "STROBE":
                            flash = LedApi.FlashMode.Strobe;
                            break;
                    }

                    api.CustomFades(colors, flash, Convert.ToInt32(speed));
            }


            // Color sets (RGBW) are broken out by ;
            // Each color within a set is broken out by ,
            private LedColor[] BuildColors(string colorStrings)
            {
                    char colorGroupDelimeter = ';';
                    char colorDelimeter = ',';
                    string[] colorSets = colorStrings.Split(colorGroupDelimeter);
                    LedColor[] colors = new LedColor[colorSets.Length];
                    for (int i=0;i<colorSets.Length;i++)
                    {
                        string[] colorComponents = colorSets[i].Split(colorDelimeter);
                        colors[i] = new LedColor(Convert.ToInt32(colorComponents[0]), Convert.ToInt32(colorComponents[1]), Convert.ToInt32(colorComponents[2]), Convert.ToInt32(colorComponents[3]));
                    }
                    return colors;
            }


            internal static void InvalidCommand(string message)
            {
                Console.WriteLine(message + ". For syntax help check out https://github.com/mikmort/MagicUFOLed/edit/master/Readme.md");
            }
  
    }
}
