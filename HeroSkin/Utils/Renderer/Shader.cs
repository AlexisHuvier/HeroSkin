using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace HeroSkin.Utils.Renderer
{
    public class Shader
    {
        public readonly int Handle;

        private readonly Dictionary<string, int> _uniformLocation;

        public Shader(string vertSource, string fragSource)
        {
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertSource);
            CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragSource);
            CompileShader(fragmentShader);

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            LinkProgram(Handle);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out int nbUniforms);
            _uniformLocation = new Dictionary<string, int>();
            for (int i = 0; i < nbUniforms; i++)
            {
                string key = GL.GetActiveUniform(Handle, i, out _, out _);
                int location = GL.GetUniformLocation(Handle, key);
                _uniformLocation.Add(key, location);
            }
        }

        public static Shader FromFiles(string vertPath, string fragPath) => new Shader(File.ReadAllText(vertPath), File.ReadAllText(fragPath));

        public void Use() => GL.UseProgram(Handle);
        public int GetAttribLocation(string attribName) => GL.GetAttribLocation(Handle, attribName);

        public int GetUniformLocation(string uniform)
        {
            if (_uniformLocation.TryGetValue(uniform, out int location) == false)
            {
                location = GL.GetUniformLocation(Handle, uniform);
                _uniformLocation.Add(uniform, location);
            }

            return location;
        }

        public void Unload() => GL.DeleteProgram(Handle);

        // Uniform setters - BEGIN

        public void SetInt(string name, int data)
        {
            if (_uniformLocation.ContainsKey(name))
                GL.Uniform1(_uniformLocation[name], data);
        }

        public void SetFloat(string name, float data)
        {
            if (_uniformLocation.ContainsKey(name))
                GL.Uniform1(_uniformLocation[name], data);
        }

        public void SetMatrix4(string name, Matrix4 data)
        {
            if (_uniformLocation.ContainsKey(name))
                GL.UniformMatrix4(_uniformLocation[name], true, ref data);
        }

        public void SetVector3(string name, Vector3 data)
        {
            if (_uniformLocation.ContainsKey(name))
                GL.Uniform3(_uniformLocation[name], data);
        }

        // Uniform setters - END

        private static void CompileShader(int shader)
        {
            GL.CompileShader(shader);
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int code);
        }

        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int code);
        }

        public static Shader GetSkinRendererShader()
        {
            return new Shader(@"
#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;

void main(void) 
{
    texCoord = aTexCoord;
    gl_Position = vec4(aPosition, 1.0);
}
", @"
#version 330

out vec4 outputColor;
in vec2 texCoord;

uniform sampler2D texture0;

void main() 
{
    outputColor = texture(texture0, texCoord);
}
");
        }
    }
}