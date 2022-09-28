using ObjectTK.Buffers;
using ObjectTK.Shaders;
using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
using ObjectTK.Textures;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace CellularAutomata.Shaders
{
    [ComputeShaderSource("BackBufferShader.ToBackBuffer")]
    public class BackBufferShader : ComputeProgram
    {
        public ImageUniform FrontBuffer { get; set; }
        public ImageUniform BackBuffer { get; set; }

        public static BackBufferShader Create(Texture2D frontBuffer, Texture2D backBuffer)
        {
            BackBufferShader backBufferShader = ProgramFactory.Create<BackBufferShader>();
            backBufferShader.Use();

            backBufferShader.FrontBuffer.Bind(0, frontBuffer, TextureAccess.ReadWrite);
            backBufferShader.BackBuffer.Bind(1, backBuffer, TextureAccess.ReadWrite);

            return backBufferShader;
        }
    }
}
