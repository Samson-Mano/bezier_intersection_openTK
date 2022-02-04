using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
// This app class structure
using bezier_intersection.heat_map_gdiplus;
using bezier_intersection.drawing_objects_store;
using bezier_intersection.drawing_objects_store.drawing_elements;
using bezier_intersection.global_variables;

namespace bezier_intersection.heatmap_opengl
{
    public class bezier_hm_opengl
    {
       // points_list_store all_mesh_pts = new points_list_store();
        lines_list_store all_contour_lines = new lines_list_store();
        quadrilaterals_list_store all_quad_mesh = new quadrilaterals_list_store();

        double max_v = Double.MinValue;
        double min_v = Double.MaxValue;

        private int ln_id = 0;

        public bezier_hm_opengl()
        {
            // Empty constructor
        }

        public bezier_hm_opengl(List<bezier_points_store> tf_bz_pts_at_t,
                                List<bezier_points_store> ts_bz_pts_at_t)
        {
            // Uneven parameter t splits is not allowed
            if (tf_bz_pts_at_t.Count != ts_bz_pts_at_t.Count)
                return;


          //  all_mesh_pts = new points_list_store();
            int i = 0, j = 0;

            // Set the maximum and minimum
            float[,] pts_array_x = new float[tf_bz_pts_at_t.Count, ts_bz_pts_at_t.Count];
            float[,] pts_array_y = new float[tf_bz_pts_at_t.Count, ts_bz_pts_at_t.Count];
            double[,] f_xy = new double[tf_bz_pts_at_t.Count, ts_bz_pts_at_t.Count];

            for (i = 0; i < tf_bz_pts_at_t.Count; i++)
            {
                float t_x = (((float)i / (float)(tf_bz_pts_at_t.Count - 1)) - 0.5f) * 2.0f;
                for (j = 0; j < ts_bz_pts_at_t.Count; j++)
                {
                    float t_y = (((float)j / (float)(ts_bz_pts_at_t.Count - 1)) - 0.5f) * 2.0f;

                    // f(x,y) is distance between two bezier curves
                    f_xy[i, j] = get_distance(tf_bz_pts_at_t[i], ts_bz_pts_at_t[j]);

                    // Add to the points
                    pts_array_x[i, j] = t_x;
                    pts_array_y[i, j] = t_y;

                    // Fix the maximum and minimum values
                    max_v = Math.Max(max_v, f_xy[i, j]);
                    min_v = Math.Min(min_v, f_xy[i, j]);
                }
            }


            // Create boundary vertices
            //for (i = 0; i < tf_bz_pts_at_t.Count; i++)
            //{
            //    for (j = 0; j < ts_bz_pts_at_t.Count; j++)
            //    {
            //        double z_val = ((f_xy[i, j] - min_v) / (max_v - min_v));
            //        Color z_color = gvariables_static.HSLToRGB(120, (1 - z_val) * 240, 1, 0.35);
            //        all_mesh_pts.add_point(pts_array_x[i, j], pts_array_y[i, j], z_color);
            //    }
            //}

            // Set Contour levels
            int number_of_contour_lvl = 20;
            // double z_level_iter = (this.max_v - this.min_v) / number_of_contour_lvl;
            List<double> contour_lvls = new List<double>();

            for (i = 1; i < number_of_contour_lvl; i++)
            {
                contour_lvls.Add(((double)i / (double)number_of_contour_lvl));
            }


            // rre- initialize the contour lines
            all_quad_mesh = new quadrilaterals_list_store();
            all_contour_lines = new lines_list_store();

            int t_q_id = 0;
            ln_id = 0;
            // Add quadrilateral mesh
            for (i = 1; i < tf_bz_pts_at_t.Count; i++)
            {
                for (j = 1; j < ts_bz_pts_at_t.Count; j++)
                {
                    // Quad mesh
                    // 00
                    double t_00 = pts_array_x[i - 1, j - 1];
                    double s_00 = pts_array_y[i - 1, j - 1];
                    double f_ts_00 = f_xy[i - 1, j - 1];
                    double z_val_00 = ((f_ts_00 - min_v) / (max_v - min_v));
                    Color z_color_00 = gvariables_static.HSLToRGB(120, (1 - z_val_00) * 240, 1, 0.5);

                    // 10
                    double t_10 = pts_array_x[i, j - 1];
                    double s_10 = pts_array_y[i, j - 1];
                    double f_ts_10 = f_xy[i, j - 1];
                    double z_val_10 = ((f_ts_10 - min_v) / (max_v - min_v));
                    Color z_color_10 = gvariables_static.HSLToRGB(120, (1 - z_val_10) * 240, 1, 0.5);

                    // 01
                    double t_01 = pts_array_x[i - 1, j];
                    double s_01 = pts_array_y[i - 1, j];
                    double f_ts_01 = f_xy[i - 1, j];
                    double z_val_01 = ((f_ts_01 - min_v) / (max_v - min_v));
                    Color z_color_01 = gvariables_static.HSLToRGB(120, (1 - z_val_01) * 240, 1, 0.5);

                    // 11
                    double t_11 = pts_array_x[i, j];
                    double s_11 = pts_array_y[i, j];
                    double f_ts_11 = f_xy[i, j];
                    double z_val_11 = ((f_ts_11 - min_v) / (max_v - min_v));
                    Color z_color_11 = gvariables_static.HSLToRGB(120, (1 - z_val_11) * 240, 1, 0.5);

                    // Add Quadrilateral mesh
                    all_quad_mesh.add_quadrilateral(t_q_id,t_00, s_00, z_color_00,
                        t_10, s_10, z_color_10,
                        t_01, s_01, z_color_01,
                        t_11, s_11, z_color_11);
                    t_q_id++;

                    // Add contour for quadrialteral
                    add_controur_line_of_quadrilateral_mesh(contour_lvls,
                        t_00, s_00, t_10, s_10,
                        t_01, s_01, t_11, s_11,
                        z_val_00, z_val_10, z_val_01, z_val_11);
                }
            }


            // Set the openTK objects !!
          //  all_mesh_pts.set_openTK_objects();
            all_contour_lines.set_openTK_objects();
            all_quad_mesh.set_openTK_objects();
        }

        public void paint_heatmap()
        {
            //  all_mesh_pts.paint_all_points();
            all_quad_mesh.paint_all_quadrialterals();
           all_contour_lines.paint_all_lines();
        }

        private void add_controur_line_of_quadrilateral_mesh(List<double> contour_levels, double t_00,
                      double s_00, double t_10, double s_10, double t_01, double s_01, double t_11, double s_11,
                      double z_lvl00, double z_lvl10, double z_lvl01, double z_lvl11)
        {
            // Check whether the z_values lies between the contour levels

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
                Color z_color_cntour = gvariables_static.HSLToRGB(120, (1 - contour_lvl) * 240, 1, 0.35);

                if (is_between_zlevls(contour_lvl, z_lvl00, z_lvl10) ||
                   is_between_zlevls(contour_lvl, z_lvl10, z_lvl11) ||
                    is_between_zlevls(contour_lvl, z_lvl11, z_lvl00))
                {
                    // Side 1 & 2 & h
                    add_controur_line(t_00,
                        s_00, t_10, s_10, t_11, s_11, z_lvl00, z_lvl10, z_lvl11, contour_lvl, z_color_cntour);
                }


                if (is_between_zlevls(contour_lvl, z_lvl11, z_lvl01) ||
                    is_between_zlevls(contour_lvl, z_lvl01, z_lvl00) ||
                    is_between_zlevls(contour_lvl, z_lvl00, z_lvl11))
                {
                    // Side 3 & 4 & h
                    add_controur_line(t_11,
                        s_11, t_01, s_01, t_00, s_00, z_lvl11, z_lvl01, z_lvl00, contour_lvl, z_color_cntour);
                }
            }
        }

        private void add_controur_line(double pt1_x, double pt1_y,
                                        double pt2_x, double pt2_y,
                                        double pt3_x, double pt3_y,
                                        double w1, double w2, double w3, double z_val, Color z_color)
        {
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
            double sx_t = s_t.Item1;
            double sy_t = s_t.Item2;
            // End point
            double ex_t = e_t.Item1;
            double ey_t = e_t.Item2;


            // Add contour line
            all_contour_lines.add_line(ln_id,sx_t, sy_t, z_color, ex_t, ey_t, z_color);
            ln_id++;
        }

        private bool is_between_zlevls(double z_l, double z0, double z1)
        {
            if ((z_l > z0 && z_l < z1) || (z_l < z0 && z_l > z1))
            {
                return true;
            }
            return false;
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

        private double get_distance(bezier_points_store bz1, bezier_points_store bz2)
        {
            return Math.Sqrt(Math.Pow(bz1.x - bz2.x, 2) + Math.Pow(bz1.y - bz2.y, 2));
        }
    }
}
