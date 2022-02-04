using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace bezier_intersection
{
    public class points_storeG
    {
        private int _pt_id;
        private double _x;
        private double _y;

        private const int pt_diameter = 6;
        private Pen pt_pen = new Pen(Color.Brown, 2);

        public PointF get_pt { get { return new PointF((float)this._x, (float)this._y); } }

        private string str_pt_id { get { return this._pt_id.ToString(); } }

        private string str_pt_coord { get { return "(" + this._x.ToString("F1") + ", " + this._y.ToString("F1") + ")"; } }

        private string str_pt { get { return str_pt_id + str_pt_coord; } }

        public points_storeG(int t_pt_id, double t_x, double t_y, Color c_p)
        {
            // Constructor
            this._pt_id = t_pt_id;
            this._x = t_x;
            this._y = t_y;
            this.pt_pen = new Pen(c_p, 2);
        }

        public void paint_end_points(Graphics gr0)
        {
            // Paint the ellipse for end points
            float tx = (float)this._x - (pt_diameter * 0.5f);
            float ty = (float)this._y - (pt_diameter * 0.5f);
            gr0.FillEllipse(pt_pen.Brush, tx, ty, pt_diameter, pt_diameter);

            string str1 = "[" + tx.ToString() + ", " + ty.ToString() + "]";
            gr0.DrawString(str1, new Font("Cambria Math", 12), pt_pen.Brush, new PointF(tx, ty));
        }

        public void paint_control_points(Graphics gr0)
        {
            // Paint the rectangle for control points
            float tx = (float)this._x - (pt_diameter * 0.5f);
            float ty = (float)this._y - (pt_diameter * 0.5f);
            gr0.FillRectangle(pt_pen.Brush, tx, ty, pt_diameter, pt_diameter);

        }


        public bool is_clicked(PointF click_pt)
        {
            if (((click_pt.X - 6) < this._x && (click_pt.X + 6) > this._x) &&
                ((click_pt.Y - 6) < this._y && (click_pt.Y + 6) > this._y))
            {

                return true;
            }
            return false;
        }
    }
}
