using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bezier_intersection.drawing_objects_store.drawing_elements;

namespace bezier_intersection.drawing_objects_store
{
    public class geometry_store
    {
        public points_list_store pts_set { get; private set; }

        public lines_list_store lines_set { get; private set; }

        member_id_control mem_id = new member_id_control();
        

        public geometry_store()
        {
            // Empty constructor
            pts_set = new points_list_store();
        }

        public void add_point(double t_x, double t_y)
        {
                    // Add point
        }

        public void delete_point()
        {
            // Delete point

        }

        public void add_line()
        {
            // Add line

        }

        public void delete_line()
        {
            // Delete line

        }

        public void set_openTK_objects()
        {
            // Set openTK objects for all points
            pts_set.set_openTK_objects();
        }

        public void paint_geometry()
        {
            // Set OpenGl Buffers before calling paint
            // paint all points
            pts_set.paint_all_points();
        }


    }
}
