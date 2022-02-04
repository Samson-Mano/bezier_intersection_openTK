using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;

namespace bezier_intersection.heat_map_gdiplus
{
    public class bezier_fxy_store
    {
        int _id;

        double t_00, s_00;
        double f_ts_00;

        double t_10, s_10;
        double f_ts_10;

        double t_01, s_01;
        double f_ts_01;

        double t_11, s_11;
        double f_ts_11;

        Color z_center_color;
        Color[] z_surround_colors = new Color[4];

        List<bezier_hm_contour_lines> b_contour_lines = new List<bezier_hm_contour_lines>();

        double _max_f_ts, _min_f_ts;

        public double max_f_ts { get { return this._max_f_ts; } }

        public double min_f_ts { get { return this._min_f_ts; } }

        public bezier_fxy_store(int t_id, bezier_points_store f_bz_pt0, bezier_points_store f_bz_pt1,
                                    bezier_points_store s_bz_pt0, bezier_points_store s_bz_pt1)
        {
            this._id = t_id;
            // Quad mesh
            // 00
            t_00 = f_bz_pt0.t;
            s_00 = s_bz_pt0.t;
            f_ts_00 = get_distance(f_bz_pt0, s_bz_pt0);

            // 10
            t_10 = f_bz_pt1.t;
            s_10 = s_bz_pt0.t;
            f_ts_10 = get_distance(f_bz_pt1, s_bz_pt0);

            // 01
            t_01 = f_bz_pt0.t;
            s_01 = s_bz_pt1.t;
            f_ts_01 = get_distance(f_bz_pt0, s_bz_pt1);

            // 11
            t_11 = f_bz_pt1.t;
            s_11 = s_bz_pt1.t;
            f_ts_11 = get_distance(f_bz_pt1, s_bz_pt1);

            // Maximum and minimum
            this._max_f_ts = Math.Max(Math.Max(Math.Max(f_ts_00, f_ts_10), f_ts_01), f_ts_11);
            this._min_f_ts = Math.Min(Math.Min(Math.Min(f_ts_00, f_ts_10), f_ts_01), f_ts_11);
        }

        public void set_color(double maxv, double minv, List<double> contour_levels)
        {
            double z_lvl00, z_lvl10, z_lvl01, z_lvl11;
            z_lvl00 = ((f_ts_00 - minv) / (maxv - minv));
            z_lvl10 = ((f_ts_10 - minv) / (maxv - minv));
            z_lvl01 = ((f_ts_01 - minv) / (maxv - minv));
            z_lvl11 = ((f_ts_11 - minv) / (maxv - minv));

            Color z_color00, z_color10, z_color01, z_color11;

            z_color00 = HSLToRGB(120, (1 - z_lvl00) * 240, 1, 0.35);
            z_color10 = HSLToRGB(120, (1 - z_lvl10) * 240, 1, 0.35);
            z_color01 = HSLToRGB(120, (1 - z_lvl01) * 240, 1, 0.35);
            z_color11 = HSLToRGB(120, (1 - z_lvl11) * 240, 1, 0.35);

            double z_center_lvl = (z_lvl00 + z_lvl10 + z_lvl01 + z_lvl11) / 4.0f;


            // Set the sorround colors
            this.z_surround_colors = new Color[4] { z_color00,
                                               z_color10,
                                               z_color01,
                                               z_color11};

            // Center color
            this.z_center_color = HSLToRGB(120, (1 - z_center_lvl) * 240, 1, 0.35);

            // Set the contour lines
            b_contour_lines = new List<bezier_hm_contour_lines>();

            foreach (double contour_lvl in contour_levels)
            {
                /*

                 ____3_____
                |          |
                4          2
                |    h     |
                |          |
                 _____1_____

                */
                // Find the triangle to add
                Color z_color_cntour = HSLToRGB(120, (1 - contour_lvl) * 240, 1, 0.35);
                bezier_hm_contour_lines temp_c_line;

                if (is_between_zlevls(contour_lvl,z_lvl00,z_lvl10) ||
                   is_between_zlevls(contour_lvl, z_lvl10, z_lvl11) ||
                    is_between_zlevls(contour_lvl, z_lvl11, z_lvl00))
                {
                    // Side 1 & 2 & h
                    temp_c_line = new bezier_hm_contour_lines(t_00, 
                        s_00, t_10, s_10, t_11, s_11, z_lvl00, z_lvl10, z_lvl11, contour_lvl, z_color_cntour);

                    b_contour_lines.Add(temp_c_line);
                }


                if (is_between_zlevls(contour_lvl, z_lvl11, z_lvl01) ||
                    is_between_zlevls(contour_lvl, z_lvl01, z_lvl00) ||
                    is_between_zlevls(contour_lvl, z_lvl00, z_lvl11))
                {
                    // Side 3 & 4 & h
                    temp_c_line = new bezier_hm_contour_lines(t_11,
                        s_11, t_01, s_01, t_00, s_00, z_lvl11, z_lvl01, z_lvl00, contour_lvl, z_color_cntour);

                    b_contour_lines.Add(temp_c_line);
                }

            }

        }

        private bool is_between_zlevls(double z_l, double z0, double z1)
        {
            if((z_l>z0 && z_l<z1) || (z_l<z0 && z_l>z1))
            {
                return true;
            }
           return false;
        }

        public void paint_mesh(Graphics gr0, float loc, float sz)
        {
            float tx = (float)(loc * t_00);
            float ty = (float)(loc * s_00);

            gr0.SmoothingMode = SmoothingMode.None;

            // Paint Mesh heat map
            using (GraphicsPath mesh_path = new GraphicsPath())
            {
                // Create a path that consists of a single ellipse.
                RectangleF mesh_rect = new RectangleF(tx, ty, sz, sz);
                mesh_path.AddRectangle(mesh_rect);

                // Use the path to construct a brush.
                using (PathGradientBrush mesh_pthGrBrush = new PathGradientBrush(mesh_path))
                {
                    mesh_pthGrBrush.SurroundColors = this.z_surround_colors;
                    mesh_pthGrBrush.CenterColor = this.z_center_color;

                    gr0.FillRectangle(mesh_pthGrBrush, mesh_rect);
                }
            }
            gr0.SmoothingMode = SmoothingMode.AntiAlias;

            // Paint the contour lines
            foreach(bezier_hm_contour_lines cline in b_contour_lines)
            {
                cline.paint_contour_line(gr0, loc);
            }
        }

        private double get_distance(bezier_points_store bz1, bezier_points_store bz2)
        {
            return Math.Sqrt(Math.Pow(bz1.x - bz2.x, 2) + Math.Pow(bz1.y - bz2.y, 2));
        }

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
