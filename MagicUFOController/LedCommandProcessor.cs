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
            
            switch (commands[1])
            {

                case "SETCOLOR":
                    api.SetColor(Convert.ToInt32(commands[2]), Convert.ToInt32(commands[3]), Convert.ToInt32(commands[4]), Convert.ToInt32(commands[5]));
                    break;
                case "SETBRIGHTNESS":
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
                    ProcessFade(commands[2],commands[3],commands[4]);
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



  

        
  

    }
}
