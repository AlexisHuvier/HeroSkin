﻿using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;

namespace HeroSkin.Utils.Renderer
{
    public static class SkinRendererScene
    {
        private static readonly Shader shader = Shader.GetSkinRendererShader();
        private static Texture texture;

        private static readonly float[] vertices =
        {
             1f,  1f, -1f, 1f, 1f,
             1f, -1f, -1f, 1f, 0f,
            -1f, -1f, -1f, 0f, 0f,
            -1f,  1f, -1f, 0f, 1f,

             1f,  1f,  1f, 1f, 1f,
             1f, -1f,  1f, 1f, 0f,
            -1f, -1f,  1f, 0f, 0f,
            -1f,  1f,  1f, 0f, 1f,

             1f, -1f,  1f, 1f, 0f,
             1f, -1f, -1f, 0f, 0f,
             1f,  1f, -1f, 0f, 1f,
             1f,  1f,  1f, 1f, 1f,

            -1f, -1f,  1f, 1f, 0f,
            -1f, -1f, -1f, 0f, 0f,
            -1f,  1f, -1f, 0f, 1f,
            -1f,  1f,  1f, 1f, 1f,

            -1f, -1f,  1f, 0f, 1f,
             1f, -1f,  1f, 1f, 1f,
             1f, -1f, -1f, 1f, 0f,
            -1f, -1f, -1f, 0f, 0f,

            -1f,  1f,  1f, 0f, 0f,
             1f,  1f,  1f, 1f, 0f,
             1f,  1f, -1f, 1f, 1f,
            -1f,  1f, -1f, 0f, 1f
        };

        private static readonly uint[] indices =
        {
            0, 1, 3,
            1, 2, 3,

            4, 5, 7,
            5, 6, 7,

            8,  9, 11,
            9, 10, 11,

            12, 13, 15,
            13, 14, 15,

            16, 17, 19,
            17, 18, 19,

            20, 21, 23,
            21, 22, 23
        };

        private static int vbo;
        private static int vao;
        private static int ebo;

        private static double _time;

        private static Camera camera = new Camera(Vector3.UnitZ * 3, 1f);

        public static void SetTexture(Bitmap bitmap)
        {
            texture = Texture.LoadFromBitmap(bitmap, TextureFilter.Nearest, TextureFilter.Nearest);
            texture.Use(OpenTK.Graphics.OpenGL4.TextureUnit.Texture0);
        }

        public static void Ready()
        {
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.ClearColor(Color4.DarkGray);

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            shader.Use();

            int vertexLocation = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        }

        public static void Render()
        {
            _time += 0.1;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (texture != null)
            {
                GL.BindVertexArray(vao);

                
                texture.Use(OpenTK.Graphics.OpenGL4.TextureUnit.Texture0);
                shader.Use();

                Matrix4 model = Matrix4.Identity * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(_time)) * Matrix4.CreateRotationX(0.5f);
                shader.SetMatrix4("model", model);
                shader.SetMatrix4("view", camera.GetViewMatrix());
                shader.SetMatrix4("projection", camera.GetProjectionMatrix());

                GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            }

            GL.Finish();
        }
    }
}
