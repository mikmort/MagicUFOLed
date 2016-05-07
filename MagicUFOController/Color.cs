﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MagicUFOController
{
    class LedColor
    {
        public int red;
        public int blue;
        public int green;
        public int warmWhite;

        public LedColor()
        {
            red = 0;
            blue = 0;
            green = 0;
            warmWhite = 0;
        }

        public LedColor(int ired, int iblue, int igreen, int iwarmWhite)
        {
            red = ired;
            blue = iblue;
            green = igreen;
            warmWhite = iwarmWhite;
        }

        public void SetRandomColor()
        {
            SetRandomColor(.5);
        }

        public void SetRandomColor(double brightness)
        {
            Random rnd = new Random();
            double hue = rnd.NextDouble()*360;

            // Bias for more saturation
            double saturation = GetRandomNumber(100, 1, .3)/100;
            Console.WriteLine(saturation.ToString());
            Color newColor = ColorFromHSV(hue, saturation, brightness);
            red = newColor.R;
            green = newColor.G;
            blue = newColor.B;
            warmWhite = 0;

        }

        private double GetRandomNumber(double max, double min, double probabilityPower = 2)
        {
            Random randomizer = new Random();
            double randomDouble = randomizer.NextDouble();

            double result = Math.Floor(min + (max + 1 - min) * (Math.Pow(randomDouble, probabilityPower)));
            return (double)result;
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

    }


}
