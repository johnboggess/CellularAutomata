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
    [ComputeShaderSource("CellularAutomataShader.CellularAutomata")]
    public class CellularAutomataShader : ComputeProgram
    {
        public ImageUniform FrontBuffer { get; set; }
        public ImageUniform BackBuffer { get; set; }
        public Uniform<Vector2> ClickLocation { get; set; }

        public static CellularAutomataShader Create(Texture2D frontBuffer, Texture2D backBuffer)
        {
            CellularAutomataShader cellularAutomataShader = ProgramFactory.Create<CellularAutomataShader>();
            cellularAutomataShader.Use();

            cellularAutomataShader.FrontBuffer.Bind(0, frontBuffer, TextureAccess.ReadWrite);
            cellularAutomataShader.BackBuffer.Bind(1, backBuffer, TextureAccess.ReadWrite);

            return cellularAutomataShader;
        }
    }
}
