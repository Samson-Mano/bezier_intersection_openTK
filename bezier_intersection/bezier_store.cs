using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace bezier_intersection
{
    public class bezier_store
    {
        // Control pts
        List<points_storeG> _cntrl_pts = new List<points_storeG>();

        // int to track the selected point
        private int _selected_pt_index = -1;

        // Color of the bezier 
        private Pen bz_pen = new Pen(Color.Brown, 2);

        // List of points to use in painting
        private List<PointF> bz_paint_pts = new List<PointF>();

        // Number of param t [0,1]
        int no_of_t_pts = 200;
        private List<bezier_points_store> _bz_pts_at_t = new List<bezier_points_store>();

        public List<bezier_points_store> bz_pts_at_t { get { return this._bz_pts_at_t; } }

        // First or second bezier line
        private int _clr_i;


        public List<points_storeG> cntrl_pts { get { return this._cntrl_pts; } }

        public bezier_store()
        {
            // Empty constructor
        }


        public bezier_store(List<points_storeG> t_pts, int clr_i)
        {
            // Control Points and first or second bezier
            this._cntrl_pts = t_pts;
            this._clr_i = clr_i;

            // Get the bezier points
            end_update();

            // Set the color of bezier line
            if (clr_i == 1)
            {
                bz_pen = new Pen(Color.DarkViolet, 2);
            }
            else
            {
                bz_pen = new Pen(Color.DarkOrange, 2);
            }

        }

        private List<bezier_points_store> get_bezier_polynomial_pts(int n_tpts)
        {
            // Call after setting the control points and number of t points [0,1]
            List<bezier_points_store> polynomial_pt = new List<bezier_points_store>();
            double t_iteration = (1.0f / (double)n_tpts);

            // Create all the control points as PointF
            List<PointF> t_cntrl_point = new List<PointF>();
            foreach (points_storeG pts in cntrl_pts)
            {
                t_cntrl_point.Add(pts.get_pt);
            }

            double t = 0;
            for (int i = 0; i <= n_tpts; i++)
            {
                PointF pt_at_t = getCasterlJauPoint(t_cntrl_point, t_cntrl_point.Count - 1, 0, t);
                bezier_points_store temp_pt = new bezier_points_store(i, t, pt_at_t.X, pt_at_t.Y);
                polynomial_pt.Add(temp_pt);

                // iterate parameter t
                t = t + t_iteration;
            }

            return polynomial_pt;
        }

        private PointF getCasterlJauPoint(List<PointF> cntrl_pts, int sub_pt_count, int i, double t)
        {
            if (sub_pt_count == 0)
            {
                return cntrl_pts[i];
            }

            PointF p1 = getCasterlJauPoint(cntrl_pts, sub_pt_count - 1, i, t);
            PointF p2 = getCasterlJauPoint(cntrl_pts, sub_pt_count - 1, i + 1, t);

            return new PointF((float)((1 - t) * p1.X + (t * p2.X)),
                (float)((1 - t) * p1.Y + (t * p2.Y)));
        }


        public void paint_bezier(Graphics gr0)
        {
            if (cntrl_pts.Count != 0)
            {
                if (_selected_pt_index != -1)
                {
                    paint_bezier_dynamic(gr0);
                    return;
                }

                // Draw the bezier curve
                gr0.DrawCurve(bz_pen, bz_paint_pts.ToArray());

                // Paint the end points
                cntrl_pts[0].paint_end_points(gr0,true);
                cntrl_pts[cntrl_pts.Count - 1].paint_end_points(gr0,false);

                // Paint the control points
                for (int i = 1; i < (cntrl_pts.Count - 1); i++)
                {
                    cntrl_pts[i].paint_control_points(gr0);

                }


            }
        }

        public bool is_pts_selected(PointF pt)
        {
            // Test to lock on to selected points to drag it
            _selected_pt_index = -1;
            for (int i = 0; i < cntrl_pts.Count; i++)
            {
                if (cntrl_pts[i].is_clicked(pt) == true)
                {
                    _selected_pt_index = i;
                    return true;
                }
            }
            return false;
        }


        public void update_point(PointF pt)
        {
            Color pt_clr;

            // Color based on first or second bezier control points
            if (this._clr_i == 1)
            {
                pt_clr = Color.DarkGreen;
            }
            else
            {
                pt_clr = Color.Brown;
            }

            // Update the points
            this._cntrl_pts[_selected_pt_index] = new points_storeG(_selected_pt_index, pt.X, pt.Y, pt_clr);
        }

        public void end_update()
        {
            // End the update of points
            _selected_pt_index = -1;

            // Finish the update by creating the bezier curve
            this._bz_pts_at_t.Clear();
            this._bz_pts_at_t = get_bezier_polynomial_pts(this.no_of_t_pts);

            this.bz_paint_pts.Clear();
            foreach (bezier_points_store bz_pt in this._bz_pts_at_t)
            {
                this.bz_paint_pts.Add(bz_pt.get_pt);
            }
        }

        public void paint_bezier_dynamic(Graphics gr0)
        {
            // Paint the lines to control points
            for (int i = 1; i < cntrl_pts.Count; i++)
            {
                using (Pen cl_pen = new Pen(bz_pen.Color, 2))
                {
                    cl_pen.DashStyle = DashStyle.Dot;
                    gr0.DrawLine(cl_pen, cntrl_pts[i - 1].get_pt, cntrl_pts[i].get_pt);
                }
            }

            // Paint the end points
            this._cntrl_pts[0].paint_end_points(gr0,true);
            this._cntrl_pts[cntrl_pts.Count - 1].paint_end_points(gr0,false);

            // Paint the control points
            for (int i = 1; i < (cntrl_pts.Count - 1); i++)
            {
                this._cntrl_pts[i].paint_control_points(gr0);

            }

            // Paint a quick bezier point
            List<bezier_points_store> dynamic_bz_pts_at_t = new List<bezier_points_store>();
            dynamic_bz_pts_at_t = get_bezier_polynomial_pts(10);

            List<PointF> temp_bz_pts = new List<PointF>();
            foreach (bezier_points_store bz_pts in dynamic_bz_pts_at_t)
            {
                temp_bz_pts.Add(bz_pts.get_pt);
            }

            // Draw the bezier curve
            gr0.DrawCurve(bz_pen, temp_bz_pts.ToArray());
        }

    }

    public class bezier_points_store
    {
        private int _id;
        private double _t;
        private double _x;
        private double _y;

        public int id { get { return this._id; } }

        public double t { get { return this._t; } }

        public double x { get { return this._x; } }

        public double y { get { return this._y; } }

        public PointF get_pt { get { return new PointF((float)this._x, (float)this._y); } }

        public bezier_points_store(int t_id, double t_t, double t_x, double t_y)
        {
            this._id = t_id;
            this._t = t_t;
            this._x = t_x;
            this._y = t_y;
        }
    }


}
