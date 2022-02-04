using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
// This app class structure
using bezier_intersection.drawing_objects_store;

namespace bezier_intersection.heat_map_gdiplus
{
    public class bezier_distance_heat_map
    {
        List<bezier_points_store> f_bz_pts_at_t = new List<bezier_points_store>();
        List<bezier_points_store> s_bz_pts_at_t = new List<bezier_points_store>();
        List<bezier_fxy_store> f_of_ts = new List<bezier_fxy_store>();

        double max_v = Double.MinValue;
        double min_v = Double.MaxValue;

        public bezier_distance_heat_map()
        {
            // Empty constructor
        }

        public bezier_distance_heat_map(List<bezier_points_store> tf_bz_pts_at_t,
                                        List<bezier_points_store> ts_bz_pts_at_t)
        {
            // Uneven parameter t splits is not allowed
            if (tf_bz_pts_at_t.Count != ts_bz_pts_at_t.Count)
                return;

            int i = 0, j = 0;

            // Add the bezier points
            this.f_bz_pts_at_t = new List<bezier_points_store>();
            this.f_bz_pts_at_t.AddRange(tf_bz_pts_at_t);

            this.s_bz_pts_at_t = new List<bezier_points_store>();
            this.s_bz_pts_at_t.AddRange(ts_bz_pts_at_t);

            // Create a quad mesh
            f_of_ts = new List<bezier_fxy_store>();
            int m_id = 0;

            for (i = 1; i < this.f_bz_pts_at_t.Count; i++)
            {
                for (j = 1; j < this.f_bz_pts_at_t.Count; j++)
                {
                    bezier_fxy_store bfxy = new bezier_fxy_store(m_id,
                    this.f_bz_pts_at_t[i - 1],
                    this.f_bz_pts_at_t[i],
                    this.s_bz_pts_at_t[j - 1],
                    this.s_bz_pts_at_t[j]);

                    // Set the max and min value
                    this.max_v = Math.Max(this.max_v, bfxy.max_f_ts);
                    this.min_v = Math.Min(this.min_v, bfxy.min_f_ts);

                    // Add to the list
                    f_of_ts.Add(bfxy);
                    m_id++;
                }
            }

            // Update the mesh color and create the contour lines

            int number_of_contour_lvl = 20;
            double z_level_iter = (this.max_v - this.min_v) / number_of_contour_lvl;
            List<double> contour_lvls = new List<double>();

            for (i = 1; i < number_of_contour_lvl; i++)
            {
                contour_lvls.Add(((double)i / (double)number_of_contour_lvl));
            }

            for (i = 0; i < this.f_of_ts.Count; i++)
            {
                f_of_ts[i].set_color(this.max_v, this.min_v, contour_lvls);
            }

        }

        public void paint_bezier_heat_map(Graphics gr0, float paint_size)
        {
            float quad_width = paint_size / (float)(f_bz_pts_at_t.Count - 1);

            foreach (bezier_fxy_store bz_fxy in f_of_ts)
            {
                // Paint the mesh at the location
                bz_fxy.paint_mesh(gr0, paint_size, quad_width);
            }
        }
    }
}
