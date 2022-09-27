using System;
using System.Collections.Generic;
using System.Text;

using CellularAutomata;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
namespace CellularAutomataSandbox
{
    public class Window : GameWindow
    {
        Simulation _simulation;
        public Window() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            _simulation = new Simulation();
        }

        protected override void OnLoad()
        {
            _simulation.Load();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _simulation.Render();
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            _simulation.Update();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            _simulation.Resize(e.Size.X, e.Size.Y);
        }
    }
}
