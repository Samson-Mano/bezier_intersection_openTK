using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bezier_intersection.drawing_objects_store.drawing_elements
{
    public class quadrilateral_store
    {
        public int quad_id { get; private set; }

        public point_store pt00 { get; private set; }

        public point_store pt01 { get; private set; }

        public point_store pt10 { get; private set; }

        public point_store pt11 { get; private set; }


        public quadrilateral_store(int t_quad_id, point_store t_pt00, point_store t_pt01, point_store t_pt10, point_store t_pt11)
        {
            // Main constructor
            this.quad_id = t_quad_id;
            this.pt00 = t_pt00;
            this.pt01 = t_pt01;
            this.pt10 = t_pt10;
            this.pt11 = t_pt11;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as quadrilateral_store);
        }

        public bool Equals(quadrilateral_store other_quad)
        {
            // Check 1 (Line ids should not match)
            if (this.Equals(other_quad.quad_id) == true)
            {
                return true;
            }

            // Check 2 (Whether line end points match)
            if (is_point_attached(other_quad.pt00) == true &&
                is_point_attached(other_quad.pt01) == true &&
                is_point_attached(other_quad.pt10) == true &&
                is_point_attached(other_quad.pt11) == true)
            {

                return true;

            }
            return false;
        }

        private bool is_point_attached(point_store pt)
        {
            if (this.pt00.Equals(pt) ||
             this.pt01.Equals(pt) ||
             this.pt10.Equals(pt) ||
             this.pt11.Equals(pt))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.quad_id, this.pt00.pt_id, this.pt01.pt_id, this.pt10.pt_id, this.pt11.pt_id);
        }





    }
}
