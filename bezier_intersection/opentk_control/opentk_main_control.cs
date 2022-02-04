using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
// OpenTK library
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
// This app class structure
using bezier_intersection.opentk_control.opentk_buffer;
using bezier_intersection.opentk_control.opentk_bgdraw;
using bezier_intersection.opentk_control.shader_compiler;
using bezier_intersection.global_variables;

namespace bezier_intersection.opentk_control
{
    public class opentk_main_control
    {
        // variable stores all the shader information
        private shader_control all_shaders = new shader_control();
        // variable to control the boundary rectangle
        private boundary_rectangle_store boundary_rect = new boundary_rectangle_store(false, null);
        // Shader variable
        private Shader _shader;
        // Drawing area control
        private drawing_area_control _drawing_area_details = new drawing_area_control(500, 500);
        // scale to control the units of drawing area
        private float _primary_scale = 1.0f;
        // zoom scale
        private float _zm_scale = 1.0f;
        // Translation details
        private Vector3 _current_translation = new Vector3(0.0f, 0.0f, 0.0f);
        private Vector3 _previous_translation = new Vector3(0.0f, 0.0f, 0.0f);

        public Vector3 previous_translation { get { return this._previous_translation; } }

        public drawing_area_control drawing_area_details { get { return this._drawing_area_details; } }

        public opentk_main_control()
        {
            // main constructor
            // Set the Background color 
            // Color clr_bg = gvariables_static.glcontrol_background_color;
            //GL.ClearColor((float)(clr_bg.R/255), 
            //    (float)(clr_bg.G/255), 
            //    (float)(clr_bg.B/255), 
            //    (float)(clr_bg.A/255));

            GL.ClearColor(1.0f,1.0f,1.0f,1.0f);

            // create the shader
            this._shader = new Shader(all_shaders.get_vertex_shader(shader_control.shader_type.br_shader),
                 all_shaders.get_fragment_shader(shader_control.shader_type.br_shader));
            this._shader.Use();

            // create the boundary
            boundary_rect = new boundary_rectangle_store(true, this._shader);
        }

        public void paint_opengl_control_background()
        {
            // OPen GL works as state machine (select buffer & select the shader)
            // Vertex Buffer (Buffer memory in GPU VRAM)
            // Shader (program which runs on GPU to paint in the screen)
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Bind the shader
            _shader.Use();

            // paint the boundary border
            // boundary_rect.paint_boundary_rectangle();
        }

        public void update_drawing_area_size(int width, int height)
        {
            // update the drawing area size
            this._drawing_area_details = new drawing_area_control(width, height);

            // Update the graphics drawing area
            GL.Viewport(this._drawing_area_details.drawing_area_center_x,
                this._drawing_area_details.drawing_areas_center_y,
                this._drawing_area_details.max_drawing_area_size,
                this._drawing_area_details.max_drawing_area_size);     // Use all of the glControl painting area

            this._primary_scale = this._drawing_area_details.norm_drawing_area_min;


            _shader.SetFloat("gScale", this._primary_scale);
        }


        #region "Zoom and Pan operation of openGL control"
        public void scale_intelli_zoom_Transform(float zm, float tx, float ty)
        {
            this._zm_scale = zm;

            //update the scale
            _shader.SetFloat("gScale", (this._zm_scale * this._primary_scale));

            translate_Transform(tx, ty);
            save_translate_transform();
        }

        public void scale_Transform(float zm)
        {
            this._zm_scale = zm;

            //update the scale
            _shader.SetFloat("gScale", (this._zm_scale * this._primary_scale));
        }

        public void translate_Transform(float trans_x, float trans_y)
        {
            // 2D Translatoin
            _current_translation = new Vector3(trans_x + this._previous_translation.X, 
                trans_y + this._previous_translation.Y, 
                0.0f + this._previous_translation.Z);

            Matrix4 current_transformation = new Matrix4(1.0f, 0.0f, 0.0f, _current_translation.X, 
                0.0f, 1.0f, 0.0f, _current_translation.Y, 
                0.0f, 0.0f, 1.0f, 0.0f, 
                0.0f, 0.0f, 0.0f, 1.0f);

            _shader.SetMatrix4("gTranslation", current_transformation);
        }

        public void save_translate_transform()
        {
            // save the final translation
            _previous_translation = _current_translation;
        }
        #endregion
    }
}
