using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CellularAutomata
{
    public struct Agent
    {
        public Vector2 Position;
        public Vector2 Velocity;

        public static Agent[] Create(int number, Vector2 center, float radius)
        {
            Random rng = new Random();

            Agent[] a = new Agent[number];

            for (int i = 0; i < number; i++)
            {
                double ang = rng.NextDouble() * 2d * Math.PI;
                double dist = rng.NextDouble() * radius;
                a[i].Position = new Vector2((float)(Math.Cos(ang) * dist), (float)(Math.Sin(ang) * dist));
                a[i].Position += center;

                a[i].Velocity = new Vector2((float)(Math.Cos(ang)), (float)(Math.Sin(ang)));
            }

            return a;
        }
    }
}
