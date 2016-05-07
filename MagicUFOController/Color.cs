using System;
using System.Collections.Generic;
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

    }


}
