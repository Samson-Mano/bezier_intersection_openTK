using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// OpenTK library
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace bezier_intersection.opentk_control
{
    public class Texture
    {
        private int _m_renderer_id;
        private string file_path;
        //private byte[] m_LocalBuffer;
        //int m_width, m_Height;

        public Texture(string path)
        {
            file_path = path;

            // Generate and Bind texture
            this._m_renderer_id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, this._m_renderer_id);

            // Manadatory Default Parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);


            //StbImage.stbi_set_flip_vertically_on_load(1);
            //// Load the image
            //using (var stream = File.OpenRead(path))
            //{
            //    ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

            //    this.m_LocalBuffer = image.Data;
            //    this.m_width = image.Width;
            //    this.m_Height = image.Height;


            //    //    this.m_LocalBuffer = img_data;
            //    //this.m_width = img_width;
            //    //this.m_Height = img_height;

            //    // Internal format is the format opengl stores the data which is RGBA8
            //    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, this.m_width, this.m_Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, this.m_LocalBuffer);
            //}

            // Unbind
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Bind(int slot = 0)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + slot);
            GL.BindTexture(TextureTarget.Texture2D, this._m_renderer_id);
        }

        public void UnBind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Delete_Texture()
        {
            // Destructor
            GL.DeleteTexture(this._m_renderer_id);
        }
    }
}
