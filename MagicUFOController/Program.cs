using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security.AccessControl;

namespace MagicLedController
{
    class Program
    {

   
        static void Main(string[] args)
        {
            MagicUFOController.LedCommandProcessor ledProccesor = new MagicUFOController.LedCommandProcessor(args[0]);
            ledProccesor.ParseLine(args);
            //ledProccesor.SetBrightness(70);
            //ledProccesor.GetStatus(1);
            //ledProccesor.TurnOn(1);
            //TurnOff(1); 
            //TurnOn(1);
            //SetColor(200, 50, 10, 0);
            //string result = GetStatus(1);
        } 

    }
}
