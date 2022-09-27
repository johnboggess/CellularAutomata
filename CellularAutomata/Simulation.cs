using CellularAutomata.Shaders;
using ObjectTK.Buffers;
using ObjectTK.Shaders;
using ObjectTK.Textures;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace CellularAutomata
{
    public class Simulation
    {
        RenderShader _renderShader;
        AgentShader _agentShader;
        FadeShader _fadeShader;
        Texture2D _texture;
        Agent[] _agents;
        Agent[] _agents2;
        Quad _screen;

        Vector2i fadeLocalGroupSize = new Vector2i(10, 10);

        public Simulation()
        {
            log4net.Config.BasicConfigurator.Configure();
        }

        public void Load()
        {
            _texture = new Texture2D(SizedInternalFormat.Rgba8, 1000, 1000);
            _texture.SetFilter(TextureMinFilter.Nearest, TextureMagFilter.Nearest);
            _texture.Bind(TextureUnit.Texture0);

            _agents = Agent.Create(100, new Vector2(_texture.Width / 2, _texture.Height / 2), 10);
            _agents2 = Agent.Create(100, new Vector2(_texture.Width / 4, _texture.Height / 2), 100);
            _agentShader = AgentShader.Create(_agents, _agents2, _texture);

            _fadeShader = FadeShader.Create(fadeLocalGroupSize, _texture);

            _renderShader = RenderShader.Create();

            _screen = new Quad(_renderShader);
        }

        public void Render()
        {
            _agentShader.Use();
            AgentShader.Dispatch(1);


            _renderShader.Use();
            _screen.Draw();

            _fadeShader.Use();
            FadeShader.Dispatch(_texture.Width / fadeLocalGroupSize.X, _texture.Height / fadeLocalGroupSize.Y, 1);
        }

        public void Update()
        {

        }

        public void Resize(int newWidth, int newHeight)
        {
            GL.Viewport(0, 0, newWidth, newHeight);
        }
    }
}
