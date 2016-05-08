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
using MagicUFOController;

namespace MagicLedController
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length==0)
                LedCommandProcessor.InvalidCommand("No Arguments");
            else
            {
                MagicUFOController.LedCommandProcessor ledProccesor = new MagicUFOController.LedCommandProcessor(args[0]);
                ledProccesor.ParseLine(args);
            }

        } 

    }
}
