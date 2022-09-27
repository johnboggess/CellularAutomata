using ObjectTK;
using ObjectTK.Buffers;
using ObjectTK.Shaders;
using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
using ObjectTK.Textures;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CellularAutomata.Shaders
{
    [ComputeShaderSource("FadeShader.Fade")]
    internal class FadeShader : ComputeProgram
    {
        public ImageUniform Texture { get; set; }

        public static FadeShader Create(Vector2i localGroupSize, Texture2D texture)
        {
            FadeShader fadeShader = ProgramFactory.Create<FadeShader>(Preprocess, localGroupSize.X, localGroupSize.Y);
            fadeShader.Use();
            fadeShader.Texture.Bind(0, texture, TextureAccess.WriteOnly);
            return fadeShader;
        }

        private static string Preprocess(string source, object[] args)
        {
            string sizeXName = "$SizeX$";
            string sizeYName = "$SizeY$";
            source = source.Replace(sizeXName, args[0].ToString());
            return source.Replace(sizeYName, args[1].ToString());
        }
    }
}
