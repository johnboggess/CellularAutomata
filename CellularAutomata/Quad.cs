using CellularAutomata.Shaders;
using ObjectTK.Buffers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CellularAutomata
{
    public class Quad
    {
        VertexArray _quad;
        public Quad(RenderShader renderShader)
        {
            Vector3[] vertices = new Vector3[] { new Vector3(-1, -1, 0), new Vector3(-1, 1, 0), new Vector3(1, -1, 0), new Vector3(1, 1, 0) };
            Buffer<Vector3> vertexBuffer = new Buffer<Vector3>();
            vertexBuffer.Init(BufferTarget.ArrayBuffer, vertices);

            Vector2[] uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };
            Buffer<Vector2> uvBuffer = new Buffer<Vector2>();
            uvBuffer.Init(BufferTarget.ArrayBuffer, uv);

            Buffer<int> indexBuffer = new Buffer<int>();
            indexBuffer.Init(BufferTarget.ElementArrayBuffer, new int[] { 0, 1, 2, 3, 2, 1 });

            _quad = new VertexArray();
            _quad.Bind();
            _quad.BindAttribute(renderShader.InPosition, vertexBuffer);
            _quad.BindAttribute(renderShader.InUV, uvBuffer);
            _quad.BindElementBuffer(indexBuffer);
        }

        public void Draw()
        {
            _quad.DrawElements(PrimitiveType.Triangles, 6);
        }
    }
}
