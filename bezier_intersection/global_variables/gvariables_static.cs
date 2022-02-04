using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace bezier_intersection.global_variables
{
   public static class gvariables_static
    {
        public static Color glcontrol_background_color = Color.White;
        
        // Garphics Control variables
        public static bool Is_panflg = false;
        public static bool Is_cntrldown = false;


        #region "HSL to RGB Fundamental code -Not by Me"
        //---- The below code is from https://www.programmingalgorithms.com/algorithm/hsl-to-rgb?lang=VB.Net
        //0    : blue   (hsl(240, 100%, 50%))
        //0.25 : cyan   (hsl(180, 100%, 50%))
        //0.5  : green  (hsl(120, 100%, 50%))
        //0.75 : yellow (hsl(60, 100%, 50%))
        //1    : red    (hsl(0, 100%, 50%))
        public static Color HSLToRGB(int alpha_i, double hsl_H, double hsl_S, double hsl_L)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;


            if (hsl_S == 0)
            {
                r = g = b = (byte)(hsl_L * 255);
            }
            else
            {
                double v1, v2;
                double hue = hsl_H / 360;

                v2 = (hsl_L < 0.5) ? (hsl_L * (1 + hsl_S)) : ((hsl_L + hsl_S) - (hsl_L * hsl_S));
                v1 = 2 * hsl_L - v2;

                r = (byte)(255 * HueToRGB(v1, v2, hue + (1.0f / 3)));
                g = (byte)(255 * HueToRGB(v1, v2, hue));
                b = (byte)(255 * HueToRGB(v1, v2, hue - (1.0f / 3)));
            }

            return Color.FromArgb(alpha_i, r, g, b);
        }


        private static double HueToRGB(double v1, double v2, double vH)
        {
            if (vH < 0)
                vH += 1;

            if (vH > 1)
                vH -= 1;

            if ((6 * vH) < 1)
                return (v1 + (v2 - v1) * 6 * vH);

            if ((2 * vH) < 1)
                return v2;

            if ((3 * vH) < 2)
                return (v1 + (v2 - v1) * ((2.0f / 3) - vH) * 6);

            return v1;
        }
        #endregion
    }
}
