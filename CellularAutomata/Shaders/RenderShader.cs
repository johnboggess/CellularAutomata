using ObjectTK.Shaders;
using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
using ObjectTK.Textures;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace CellularAutomata.Shaders
{
    [VertexShaderSource("RenderShader.Vertex")]
    [FragmentShaderSource("RenderShader.Fragment")]
    public class RenderShader : Program
    {
        [VertexAttrib(3, VertexAttribPointerType.Float)]
        public VertexAttrib InPosition { get; protected set; }

        [VertexAttrib(2, VertexAttribPointerType.Float)]
        public VertexAttrib InUV { get; protected set; }

        public TextureUniform<Texture2D> Texture { get; set; }

        public static RenderShader Create()
        {
            RenderShader _renderProgram = ObjectTK.Shaders.ProgramFactory.Create<RenderShader>();
            _renderProgram.Use();
            _renderProgram.Texture.Set(TextureUnit.Texture0);

            return _renderProgram;
        }
    }
}
