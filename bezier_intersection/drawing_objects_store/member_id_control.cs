using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bezier_intersection.drawing_objects_store
{
    public class member_id_control
    {
        // public HashSet<int> pt_ids { get; private set; }
        private SortedSet<int> all_pt_ids = new SortedSet<int>();
        private SortedSet<int> all_line_ids = new SortedSet<int>();

        public enum Element_type
        {
            point,
            line,
            ellipse,
            quadrilateral,
            triangle
        }

        public member_id_control()
        {
            // Empty constructor
        }

        public int get_unique_id(Element_type type)
        {
            // Get unique Id based on the element type
            if (type == Element_type.point)
            {
                // Element is point
                return  get_ids(all_pt_ids);
            }
            else if (type == Element_type.line)
            {
                // Element is line
                return get_ids(all_line_ids);
            }

            return -1;
        }

        private int get_ids(SortedSet<int> all_ids)
        {
            // Sort ascending
            all_ids.OrderBy(obj => obj);

            for (int i = 0; i < all_ids.Count; i++)
            {
                // Get the missing ordered int
                if(all_ids.ElementAt(i) != i)
                {
                    return i;
                }
            }
            return all_ids.Count;
        }

        public void add_id(Element_type type, int id)
        {
            // Add the unique Id based on the element type
            if (type == Element_type.point)
            {
                // Element is point
                all_pt_ids.Add(id);
            }
            else if (type == Element_type.line)
            {
                // Element is line
                all_line_ids.Add(id);
            }
        }

        public void delete_id(Element_type type, int id)
        {
            // Delete the unique Id based on the element type
            if (type == Element_type.point)
            {
                // Element is point
                all_pt_ids.Remove(id);
            }
            else if (type == Element_type.line)
            {
                // Element is line
                all_line_ids.Remove(id);
            }
        }

    }
}
