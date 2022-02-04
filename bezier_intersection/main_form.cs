using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bezier_intersection.heat_map_gdiplus;
// OpenTK library
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
// This app class structure
using bezier_intersection.opentk_control;
using bezier_intersection.global_variables;
using bezier_intersection.heatmap_opengl;

namespace bezier_intersection
{
    public partial class main_form : Form
    {
        int f_index = -1;
        bezier_store f_bz = new bezier_store();
        bezier_store s_bz = new bezier_store();

        private bool is_mouse_down = false;
        RectangleF main_panel_bounding_box;

        PointF mouse_pt = new PointF(0, 0);
        System.Random rand = new Random();

        // Bezier store GDI+
        // bezier_distance_heat_map bz_heatmap = new bezier_distance_heat_map();

        // Bezier store OpenGL
        bezier_hm_opengl bz_heatmap = new bezier_hm_opengl();

        // Variables to control openTK GLControl
        // glControl wrapper class
        private opentk_main_control g_control;

        // Main zoom variables
        private float zm = 1.0f;

        // Cursor point on the GLControl
        private PointF click_pt;

        // Temporary variables to initiate the zoom to fit animation
        private float param_t = 0.0f;
        private float temp_zm = 1.0f;
        private float temp_tx, temp_ty;

        public main_form()
        {
            InitializeComponent();
        }

        #region "Main Form Control"
        private void main_form_Load(object sender, EventArgs e)
        {
            // Control the size of the picture
            form_size_control();

            float panel_width, panel_height;
            panel_width = main_panel.Width - 40.0f;
            panel_height = main_panel.Height - 40.0f;

            // Draw the bounding box
            float rect_size = Math.Min(panel_height, panel_width);
            // create the boundign box
            main_panel_bounding_box = new RectangleF((-rect_size * 0.5f), (-rect_size * 0.5f), rect_size, rect_size);

            // Side panel visible
            // side_panel.Visible = false;

            main_panel_mt_pic.Refresh();
            // side_panel_mt_pic.Refresh();
        }

        private void form_size_control()
        {
            // Control the size of the picture
            float form_width, form_height;

            form_width = (this.Width / 2.0f) - 40.0f;
            form_height = this.Height - 100.0f;

            // Main panel size and location
            main_panel.Location = new Point(20, 30);
            main_panel.Size = new Size((int)form_width, (int)form_height);

            // Side panel size and location
            // side_panel.Location = new Point(40 + (int)form_width, 30);
            // side_panel.Size = new Size((int)form_width, (int)form_height);

            // Side panel GLControl size and location
            glControl_side_panel.Location = new Point(40 + (int)form_width, 30);
            glControl_side_panel.Size = new Size((int)form_width, (int)form_height);

            glControl_side_panel.Invalidate();
            main_panel_mt_pic.Refresh();
            // side_panel_mt_pic.Refresh();
        }

        private void main_form_SizeChanged(object sender, EventArgs e)
        {
            // update the picture box size
            form_size_control();
        }

        private void main_form_Shown(object sender, EventArgs e)
        {

            System.Random rand = new Random();

            // create first bezier
            create_bezier(ref this.f_bz, rand.Next(3, 7), 1);
            // create second bezier
            create_bezier(ref this.s_bz, rand.Next(3, 7), 2);

            update_heat_map();
            main_panel_mt_pic.Refresh();
        }
        #endregion

        #region "Bezier creation - menubar events"
        private void create_bezier(ref bezier_store bz, int pt_count, int fs)
        {
            // Create Beziers inside the boundary box
            double fx, fy;
            List<points_storeG> temp_pts = new List<points_storeG>();

            Color pt_clr = new Color();
            if (fs == 1)
            {
                // Point color of the first bezier
                pt_clr = Color.DarkGreen;
            }
            else
            {
                // Point color of the second bezier
                pt_clr = Color.Brown;
            }

            for (int i = 0; i < pt_count; i++)
            {
                // Create random temp points based on the bounding width
                fx = rand.Next((int)(main_panel_bounding_box.X * 100), (int)((main_panel_bounding_box.X + main_panel_bounding_box.Width) * 100)) / 100.0f;
                fy = rand.Next((int)(main_panel_bounding_box.Y * 100), (int)((main_panel_bounding_box.Y + main_panel_bounding_box.Height) * 100)) / 100.0f;

                temp_pts.Add(new points_storeG(i, fx, fy, pt_clr));
            }

            bz = new bezier_store(temp_pts, fs);
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create first bezier
            create_bezier(ref this.f_bz, rand.Next(3, 7), 1);
            // create second bezier
            create_bezier(ref this.s_bz, rand.Next(3, 7), 2);

            update_heat_map();
            main_panel_mt_pic.Refresh();
        }

        private void ptToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            // Create bezier 3 pts - First bezier
            create_bezier(ref this.f_bz, 3, 1);
            update_heat_map();
            main_panel_mt_pic.Refresh();
        }

        private void ptToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            // Create bezier 4 pts - First bezier
            create_bezier(ref this.f_bz, 4, 1);
            update_heat_map();
            main_panel_mt_pic.Refresh();
        }

        private void ptToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            // Create bezier 5 pts - First bezier
            create_bezier(ref this.f_bz, 5, 1);
            update_heat_map();
            main_panel_mt_pic.Refresh();
        }

        private void ptToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            // Create bezier 6 pts - First bezier
            create_bezier(ref this.f_bz, 6, 1);
            update_heat_map();
            main_panel_mt_pic.Refresh();
        }

        private void ptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create bezier 3 pts - Second bezier
            create_bezier(ref this.s_bz, 3, 2);
            update_heat_map();
            main_panel_mt_pic.Refresh();
        }

        private void ptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Create bezier 4 pts - Second bezier
            create_bezier(ref this.s_bz, 4, 2);
            update_heat_map();
            main_panel_mt_pic.Refresh();
        }

        private void ptToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // Create bezier 5 pts - Second bezier
            create_bezier(ref this.s_bz, 5, 2);
            update_heat_map();
            main_panel_mt_pic.Refresh();
        }

        private void ptToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            // Create bezier 6 pts - Second bezier
            create_bezier(ref this.s_bz, 6, 2);
            update_heat_map();
            main_panel_mt_pic.Refresh();
        }

        private void update_heat_map()
        {
            // Update the heat map GDI+
            //  bz_heatmap = new bezier_distance_heat_map(this.f_bz.bz_pts_at_t, this.s_bz.bz_pts_at_t);

            // Update the heat map OpenGL
            bz_heatmap = new bezier_hm_opengl(this.f_bz.bz_pts_at_t, this.s_bz.bz_pts_at_t);

            glControl_side_panel.Invalidate();
            // side_panel_mt_pic.Refresh();
        }
        #endregion

        #region "First Panel User Interface"
        private void main_panel_Paint(object sender, PaintEventArgs e)
        {
            // main panel paint
            float panel_width, panel_height;
            panel_width = main_panel.Width - 40.0f;
            panel_height = main_panel.Height - 40.0f;

            // Control the graphics
            e.Graphics.TranslateTransform(main_panel.Width * 0.5f, main_panel.Height * 0.5f);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw zero line
            e.Graphics.DrawLine(Pens.Black, -10, 0, 10, 0);
            e.Graphics.DrawLine(Pens.Black, 0, -10, 0, 10);

            // Draw the bounding box
            float rect_size = Math.Min(panel_height, panel_width);

            // Update the Bounding rectangle 
            main_panel_bounding_box = new RectangleF((-rect_size * 0.5f), (-rect_size * 0.5f), rect_size, rect_size);

            e.Graphics.DrawRectangle(Pens.Black, -(rect_size * 0.5f), -(rect_size * 0.5f), rect_size, rect_size);

            // Paint the bezier line
            Graphics gr0 = e.Graphics;
            f_bz.paint_bezier(gr0);
            s_bz.paint_bezier(gr0);
        }

        private void main_panel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            bool is_clicked_on_pt = false;
            is_mouse_down = true;
            // Check whether the first bezier points is clicked
            is_clicked_on_pt = f_bz.is_pts_selected(mouse_pt);
            if (is_clicked_on_pt == true)
            {
                f_index = 1;
                is_mouse_down = true;
                return;
            }

            // check whether the second bezier points is clicked
            is_clicked_on_pt = s_bz.is_pts_selected(mouse_pt);
            if (is_clicked_on_pt == true)
            {
                f_index = 2;
                is_mouse_down = true;
                return;
            }

        }

        private void main_panel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // End of Update
            if (f_index != -1)
            {
                f_index = -1;
                f_bz.end_update();
                s_bz.end_update();
                update_heat_map();
            }
            is_mouse_down = false;
            main_panel_mt_pic.Refresh();
        }

        private void main_panel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouse_pt = new PointF(e.Location.X - (main_panel.Width * 0.5f),
                                    e.Location.Y - (main_panel.Height * 0.5f));

            if (is_mouse_down == true && f_index != -1)
            {
                // First bezier nodes selected
                if (f_index == 1)
                {
                    f_bz.update_point(mouse_pt);
                }

                // Second bezier nodes selected
                if (f_index == 2)
                {
                    s_bz.update_point(mouse_pt);
                }

                // Refresh when mouse down and move
                main_panel_mt_pic.Refresh();
            }

            toolStripStatusLabel_mouseloc.Text = "[" + mouse_pt.X.ToString() + ", " +
                mouse_pt.Y.ToString() + "]";
        }
        #endregion

        #region "Second Panel Visualization"
        private void glControl_side_panel_Paint(object sender, PaintEventArgs e)
        {
            // Paint the drawing area (glControl_main)
            // Tell OpenGL to use MyGLControl
            glControl_side_panel.MakeCurrent();

            // Paint the background
            g_control.paint_opengl_control_background();

            // Display the heat map OpenGL
            bz_heatmap.paint_heatmap();

            // OpenTK windows are what's known as "double-buffered". In essence, the window manages two buffers.
            // One is rendered to while the other is currently displayed by the window.
            // This avoids screen tearing, a visual artifact that can happen if the buffer is modified while being displayed.
            // After drawing, call this function to swap the buffers. If you don't, it won't display what you've rendered.
            glControl_side_panel.SwapBuffers();
        }

        private void glControl_side_panel_Load(object sender, EventArgs e)
        {
            // Load the wrapper class to control the openTK glcontrol
            g_control = new opentk_main_control();

            // Update the size of the drawing area
            g_control.update_drawing_area_size(glControl_side_panel.Width,
                glControl_side_panel.Height);

            // Refresh the controller (doesnt do much.. nothing to draw)
            glControl_side_panel.Invalidate();
        }

        private void glControl_side_panel_SizeChanged(object sender, EventArgs e)
        {
            // glControl size changed
            // Update the size of the drawing area
            g_control.update_drawing_area_size(glControl_side_panel.Width,
                glControl_side_panel.Height);

            // Refresh the painting area
            glControl_side_panel.Invalidate();
        }


        #region "GLControl Mouse events"
        private void glControl_side_panel_MouseEnter(object sender, EventArgs e)
        {
            // set the focus to enable zoom/ pan & zoom to fit
            glControl_side_panel.Focus();
        }

        private void glControl_side_panel_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // User press hold Cntrl key and mouse wheel
            if (gvariables_static.Is_cntrldown == true)
            {
                // Zoom operation commences
                glControl_side_panel.Focus();

                // Get the screen pt before scaling
                PointF screen_pt_b4_scale = g_control.drawing_area_details.get_normalized_screen_pt(e.X, e.Y, zm, g_control.previous_translation.X, g_control.previous_translation.Y);

                if (e.Delta > 0)
                {
                    if (zm < 1000)
                    {
                        zm = zm + 0.1f;
                    }
                }
                else if (e.Delta < 0)
                {
                    if (zm > 0.101)
                    {
                        zm = zm - 0.1f;
                    }
                }

                // Get the screen pt after scaling
                PointF screen_pt_a4_scale = g_control.drawing_area_details.get_normalized_screen_pt(e.X, e.Y, zm, g_control.previous_translation.X, g_control.previous_translation.Y);

                float tx = (-1.0f) * zm * 0.5f * (screen_pt_b4_scale.X - screen_pt_a4_scale.X);
                float ty = (-1.0f) * zm * 0.5f * (screen_pt_b4_scale.Y - screen_pt_a4_scale.Y);

                // Scale the view with intellizoom (translate and scale)
                g_control.scale_intelli_zoom_Transform(zm, tx, ty);

                // Update the zoom value in tool strip status bar
                toolStripStatusLabel_zoom_value.Text = "Zoom: " + (zm * 100) + "%";
                // Refresh the painting area
                glControl_side_panel.Refresh();
            }
        }

        private void glControl_side_panel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Pan operation starts if Ctrl + Mouse Right Button is pressed
            if (gvariables_static.Is_cntrldown == true && e.Button == MouseButtons.Right)
            {
                // save the current cursor point
                click_pt = new PointF(e.X, e.Y);
                // Set the variable to indicate pan operation begins
                gvariables_static.Is_panflg = true;
            }
        }

        private void glControl_side_panel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            PointF temp = g_control.drawing_area_details.get_normalized_screen_pt(e.X, e.Y, zm, g_control.previous_translation.X, g_control.previous_translation.Y);

            toolStripStatusLabel_sidepanel_coord.Text = temp.X.ToString() + ", " + temp.Y.ToString();

            if (gvariables_static.Is_panflg == true)
            {
                // Pan operation is in progress
                float tx = (float)((e.X - click_pt.X) / (g_control.drawing_area_details.max_drawing_area_size * 0.5f));
                float ty = (float)((e.Y - click_pt.Y) / (g_control.drawing_area_details.max_drawing_area_size * 0.5f));

                // Translate the drawing area
                g_control.translate_Transform(tx, -1.0f * ty);

                // Refresh the painting area
                glControl_side_panel.Invalidate();
            }
        }

        private void glControl_side_panel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Pan operation ends once the Mouse Right Button is released
            if (gvariables_static.Is_panflg == true)
            {
                gvariables_static.Is_panflg = false;

                // Pan operation ends (save the translate transformation)
                g_control.save_translate_transform();

                // Refresh the painting area
                glControl_side_panel.Invalidate();
            }
        }
        #endregion

        #region "GLControl Keyboard events"
        private void glControl_side_panel_KeyDown(object sender, KeyEventArgs e)
        {
            // Keydown event
            if (e.Control == true)
            {
                // User press and hold Cntrl key
                gvariables_static.Is_cntrldown = true;

                if (e.KeyCode == Keys.F)
                {
                    // (Ctrl + F) --> Zoom to fit
                    // Set view to Fit to View (Default view) 
                    param_t = 0.0f;
                    timer1.Interval = 10;

                    // Save the current zoom and translation values to temporary variables (for the animation)
                    temp_zm = zm;
                    temp_tx = g_control.previous_translation.X;
                    temp_ty = g_control.previous_translation.Y;

                    // start the scale to fit animation
                    timer1.Start();
                }
            }
        }

        private void glControl_side_panel_KeyUp(object sender, KeyEventArgs e)
        {
            // Keyup event
            gvariables_static.Is_cntrldown = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Scale to Fit animation
            param_t = param_t + 0.05f;

            if (param_t > 1.0f)
            {
                // Set the zoom value to 1.0f
                zm = 1.0f;
                // Scale transformation (Scale to fit)
                g_control.scale_Transform(1.0f);

                param_t = 0.0f;
                // End the animation
                timer1.Stop();

                // Translate transformation (Translate back to the original)
                g_control.translate_Transform(-temp_tx, -temp_ty);
                g_control.save_translate_transform();

                // Refresh the painting area
                glControl_side_panel.Invalidate();
                return;
            }
            else
            {
                // Animate the translation & zoom value
                float anim_zm = temp_zm * (1 - param_t) + (1.0f * param_t);
                float anim_tx = (0.0f * (1 - param_t) - (temp_tx * param_t));
                float anim_ty = (0.0f * (1 - param_t) - (temp_ty * param_t));

                // Scale transformation intermediate
                g_control.scale_Transform(anim_zm);

                // Translate transformation intermediate
                g_control.translate_Transform(anim_tx, anim_ty);

                // Refresh the painting area
                glControl_side_panel.Invalidate();
            }
        }
        #endregion


        //private void side_panel_Paint(object sender, PaintEventArgs e)
        //{
        //    // side panel paint
        //    float panel_width, panel_height;
        //    panel_width = side_panel.Width - 40.0f;
        //    panel_height = side_panel.Height - 40.0f;

        //    // Control the graphics
        //    //e.Graphics.TranslateTransform(main_panel.Width * 0.1f, main_panel.Height * 0.9f);
        //    //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        //    // Draw the bounding box
        //    float rect_size = Math.Min(panel_height, panel_width);

        //    Graphics gr0 = e.Graphics;

        //    Matrix mx = new Matrix(1, 0, 0, -1, 0, 0); // Yaxis orientation flipped to match Cartesian plane
        //    // mx.Translate(side_panel.Width * 0.5f, -side_panel.Height * 0.5f);
        //    mx.Translate(0, -1.0f * side_panel.Height);
        //    mx.Translate((side_panel.Width * 0.5f) - (rect_size * 0.5f),
        //        ((side_panel.Height * 0.5f) - (rect_size * 0.5f)));


        //    gr0.Transform = mx;
        //    gr0.SmoothingMode = SmoothingMode.AntiAlias;

        //    // Draw zero line
        //    gr0.DrawLine(Pens.Black, -10, 0, 10, 0);
        //    gr0.DrawLine(Pens.Black, 0, -10, 0, 10);

        //    gr0.DrawRectangle(Pens.Black, 0, 0, rect_size, rect_size);

        //    bz_heatmap.paint_bezier_heat_map(gr0, rect_size);
        //}


        #endregion
    }
}
