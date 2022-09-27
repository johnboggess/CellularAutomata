using ObjectTK.Buffers;
using ObjectTK.Shaders;
using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
using ObjectTK.Textures;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace CellularAutomata.Shaders
{
    [ComputeShaderSource("AgentShader.Agent")]
    public class AgentShader : ComputeProgram
    {
        public ImageUniform Texture { get; set; }
        public ShaderStorage Agents { get; set; }
        public ShaderStorage Agents2 { get; set; }

        internal Buffer<Agent> AgentBuffer;

        internal Buffer<Agent> AgentBuffer2;

        public static AgentShader Create(Agent[] agents, Agent[] agents2, Texture2D texture)
        {
            AgentShader agentShader = ProgramFactory.Create<AgentShader>(Preprocess, agents.Length);
            agentShader.Use();
            agentShader.Texture.Bind(0, texture, TextureAccess.ReadWrite);

            agentShader.AgentBuffer = new Buffer<Agent>();
            agentShader.AgentBuffer.Init(BufferTarget.ArrayBuffer, agents);

            agentShader.AgentBuffer2 = new Buffer<Agent>();
            agentShader.AgentBuffer2.Init(BufferTarget.ArrayBuffer, agents2);

            agentShader.Agents.BindBuffer(agentShader.AgentBuffer);
            agentShader.Agents2.BindBuffer(agentShader.AgentBuffer2);

            return agentShader;
        }

        private static string Preprocess(string source, object[] args)
        {
            string agentCountName = "$AgentCount$";
            return source.Replace(agentCountName, args[0].ToString());
        }
    }
}
