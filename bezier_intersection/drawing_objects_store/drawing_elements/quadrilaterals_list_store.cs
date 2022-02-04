using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
// OpenTK library
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
// This app class structure
using bezier_intersection.opentk_control.opentk_buffer;

namespace bezier_intersection.drawing_objects_store.drawing_elements
{
    public class quadrilaterals_list_store
    {
        public List<quadrilateral_store> all_quads { get; private set; }
        private points_list_store all_quad_pts;

        private uint[] _quad_indices = new uint[0];

        // OpenTK variables
        private VertexBuffer quadpts_VertexBufferObject;
        private List<VertexBufferLayout> quad_BufferLayout;
        private VertexArray quad_VertexArrayObject;
        private IndexBuffer quad_ElementBufferObject;

        public quadrilaterals_list_store()
        {
            // Empty constructor
            // Initialize all points
            all_quads = new List<quadrilateral_store>();
            all_quad_pts = new points_list_store();
        }

        public void set_openTK_objects()
        {

            // Set the quadrialateral indices
            int j = 0;
            this._quad_indices = new uint[all_quads.Count * 6];

            foreach (quadrilateral_store quad in all_quads)
            {
                /*
                2__________3
                |          |
                |          |
                |     Q    |
                |          |
                0__________1
                */
                // 0, 1, 2
                // First index (First point)
                this._quad_indices[j] = (uint)quad.pt00.pt_id;
                j++;

                // Second index (Second point)
                this._quad_indices[j] = (uint)quad.pt10.pt_id;
                j++;

                // Third index (Third point)
                this._quad_indices[j] = (uint)quad.pt01.pt_id;
                j++;

                // 3, 2, 1
                // Fourth index (Fourth point)
                this._quad_indices[j] = (uint)quad.pt11.pt_id;
                j++;

                // Third index (Third point)
                this._quad_indices[j] = (uint)quad.pt01.pt_id;
                j++;

                // Second index (Second point)
                this._quad_indices[j] = (uint)quad.pt10.pt_id;
                j++;

            }

            // Set the openTK objects for the points
            this.all_quad_pts.set_openTK_objects();

            //1.  Get the vertex buffer
            this.quadpts_VertexBufferObject = this.all_quad_pts.point_VertexBufferObject;

            //2. Create and add to the buffer layout
            quad_BufferLayout = new List<VertexBufferLayout>();
            quad_BufferLayout.Add(new VertexBufferLayout(3, 7)); // Vertex layout
            quad_BufferLayout.Add(new VertexBufferLayout(4, 7)); // Color layout  

            //3. Setup the vertex Array (Add vertexBuffer binds both the vertexbuffer and vertexarray)
            quad_VertexArrayObject = new VertexArray();
            quad_VertexArrayObject.Add_vertexBuffer(this.quadpts_VertexBufferObject, quad_BufferLayout);

            // 4. Set up element buffer
            quad_ElementBufferObject = new IndexBuffer(this._quad_indices, this._quad_indices.Length);
            quad_ElementBufferObject.Bind();
        }

        public void paint_all_quadrialterals()
        {
            // Call set_openTK_objects()
            // Bind before painting
            quad_VertexArrayObject.Add_vertexBuffer(this.quadpts_VertexBufferObject, quad_BufferLayout);
            quad_ElementBufferObject.Bind();

            // Open GL paint quadrialateral
            GL.DrawElements(PrimitiveType.Triangles, this._quad_indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public void add_quadrilateral(int q_id, double pt00_x, double pt00_y, Color pt_clr00,
                                     double pt10_x, double pt10_y, Color pt_clr10,
                                     double pt01_x, double pt01_y, Color pt_clr01,
                                     double pt11_x, double pt11_y, Color pt_clr11)
        {
            // Add points
            all_quad_pts.add_point(((q_id * 4) + 0), pt00_x, pt00_y, pt_clr00);
            point_store pt00 = all_quad_pts.get_last_added_pt;

            all_quad_pts.add_point(((q_id * 4) + 1), pt01_x, pt01_y, pt_clr01);
            point_store pt01 = all_quad_pts.get_last_added_pt;

            all_quad_pts.add_point(((q_id * 4) + 2), pt10_x, pt10_y, pt_clr10);
            point_store pt10 = all_quad_pts.get_last_added_pt;

            all_quad_pts.add_point(((q_id * 4) + 3), pt11_x, pt11_y, pt_clr11);
            point_store pt11 = all_quad_pts.get_last_added_pt;

            // Add Quad
            all_quads.Add(new quadrilateral_store(q_id, pt00, pt10, pt01, pt11));
        }

    }
}
