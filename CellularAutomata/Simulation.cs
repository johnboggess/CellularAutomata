using CellularAutomata.Shaders;
using ObjectTK.Buffers;
using ObjectTK.Shaders;
using ObjectTK.Textures;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
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
        CellularAutomataShader _cellularAutomataShader;
        BackBufferShader _backBufferShader;

        Texture2D _frontBuffer;
        Texture2D _backBuffer;

        Quad _screen;

        Vector2i _windowSize = new Vector2i();
        Vector2 _mouseClick = new Vector2();
        bool _mouseClicked = false;

        object _clickLock = new object();

        public Simulation()
        {
            log4net.Config.BasicConfigurator.Configure();
        }

        public void Load()
        {
            _frontBuffer = new Texture2D(SizedInternalFormat.Rgba8, 1000, 1000);
            _frontBuffer.SetFilter(TextureMinFilter.Nearest, TextureMagFilter.Nearest);
            _frontBuffer.Bind(TextureUnit.Texture0);

            _backBuffer = new Texture2D(SizedInternalFormat.Rgba8, 1000, 1000);
            _backBuffer.SetFilter(TextureMinFilter.Nearest, TextureMagFilter.Nearest);
            _backBuffer.Bind(TextureUnit.Texture1);

            _renderShader = RenderShader.Create();

            _screen = new Quad(_renderShader);

            _cellularAutomataShader = CellularAutomataShader.Create(_frontBuffer, _backBuffer);
            _backBufferShader = BackBufferShader.Create(_frontBuffer, _backBuffer);
        }

        public void Render()
        {
            _frontBuffer.Bind(TextureUnit.Texture0);
            _backBuffer.Bind(TextureUnit.Texture1);
            _cellularAutomataShader.Use();
            if(System.Threading.Monitor.TryEnter(_clickLock))
            {
                if (_mouseClicked)
                {
                    _cellularAutomataShader.ClickLocation.Set(_mouseClick);
                    _mouseClicked = false;
                }
                System.Threading.Monitor.Exit(_clickLock);
            }
            CellularAutomataShader.Dispatch(100, 100, 1);

            _renderShader.Use();
            _screen.Draw();

            _backBufferShader.Use();
            BackBufferShader.Dispatch(100, 100, 1);
        }

        public void Update(MouseState mouseState)
        {
            if (mouseState.IsButtonDown(MouseButton.Left) && System.Threading.Monitor.TryEnter(_clickLock))
            {
                _mouseClick = new Vector2((mouseState.X / _windowSize.X) * _frontBuffer.Width, (mouseState.Y / _windowSize.Y) * _frontBuffer.Height);
                _mouseClick.Y = _frontBuffer.Height - _mouseClick.Y;
                _mouseClicked = true;
                System.Threading.Monitor.Exit(_clickLock);
            }
        }

        public void Resize(int newWidth, int newHeight)
        {
            GL.Viewport(0, 0, newWidth, newHeight);
            _windowSize = new Vector2i(newWidth, newHeight);
        }
    }
}
