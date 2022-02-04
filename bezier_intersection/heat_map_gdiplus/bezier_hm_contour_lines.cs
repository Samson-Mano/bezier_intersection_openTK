using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;

namespace bezier_intersection.heat_map_gdiplus
{
    public class bezier_hm_contour_lines
    {
        double sx_t;
        double sy_t;
        double ex_t;
        double ey_t;
        Color cline_color;

        public bezier_hm_contour_lines(double pt1_x, double pt1_y,
                                        double pt2_x, double pt2_y,
                                        double pt3_x, double pt3_y,
                                        double w1, double w2, double w3, double z_val, Color z_color)
        {
            this.cline_color = z_color;

            Tuple<double, double> s_t = new Tuple<double, double>(0, 0);
            Tuple<double, double> e_t = new Tuple<double, double>(0, 0);

            // Find the range of z_vals
            if (((w1 - z_val) * (w2 - z_val)) < 0)
            {
                if (((w2 - z_val) * (w3 - z_val)) < 0)
                {
                    // 1 & 3 with 2 as common point
                    // Find start point
                    s_t = contour_linear_interpolation(w2, w1, z_val, pt2_x, pt2_y, pt1_x, pt1_y);
                    // Find end point
                    e_t = contour_linear_interpolation(w2, w3, z_val, pt2_x, pt2_y, pt3_x, pt3_y);
                }
                else if (((w1 - z_val) * (w3 - z_val)) < 0)
                {
                    // 2 & 3 with 1 as common point
                    // Find start point
                    s_t = contour_linear_interpolation(w1, w2, z_val, pt1_x, pt1_y, pt2_x, pt2_y);
                    // Find end point
                    e_t = contour_linear_interpolation(w1, w3, z_val, pt1_x, pt1_y, pt3_x, pt3_y);
                }
            }
            else if (((w2 - z_val) * (w3 - z_val)) < 0)
            {
                if (((w1 - z_val) * (w3 - z_val)) < 0)
                {
                    // 1 & 2 with 3 as common point
                    // Find start point
                    s_t = contour_linear_interpolation(w3, w1, z_val, pt3_x, pt3_y, pt1_x, pt1_y);
                    // Find end point
                    e_t = contour_linear_interpolation(w3, w2, z_val, pt3_x, pt3_y, pt2_x, pt2_y);
                }
            }

            // Start point
            sx_t = s_t.Item1;
            sy_t = s_t.Item2;
            // End point
            ex_t = e_t.Item1;
            ey_t = e_t.Item2;

        }

        public void paint_contour_line(Graphics gr0, float loc)
        {
            float spt_x = (float)(loc * sx_t);
            float spt_y = (float)(loc * sy_t);

            float ept_x = (float)(loc * ex_t);
            float ept_y = (float)(loc * ey_t);

            gr0.DrawLine(new Pen(cline_color, 2), spt_x, spt_y, ept_x, ept_y);
        }

        private Tuple<double, double> contour_linear_interpolation(double w1, double w2, double z_val, double pt1_x, double pt1_y,
            double pt2_x, double pt2_y)
        {
            double z_slope;
            double ptx, pty;

            if ((w1 - z_val) > 0)
            {
                z_slope = (w1 - z_val) / (w1 - w2);

                ptx = (pt1_x * (1 - z_slope)) + (pt2_x * z_slope);
                pty = (pt1_y * (1 - z_slope)) + (pt2_y * z_slope);
            }
            else
            {
                z_slope = (w2 - z_val) / (w2 - w1);

                ptx = (pt2_x * (1 - z_slope)) + (pt1_x * z_slope);
                pty = (pt2_y * (1 - z_slope)) + (pt1_y * z_slope);
            }

            return new Tuple<double, double>(ptx, pty);
        }

    }
}
