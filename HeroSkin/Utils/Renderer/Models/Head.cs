using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;

namespace HeroSkin.Utils.Renderer.Models
{
    class Head
    {
        private static readonly float[] vertices =
        {
             0.8f,  0.8f, -0.8f, 0.125f, 0.875f,
             0.8f, -0.8f, -0.8f, 0.125f, 0.750f,
            -0.8f, -0.8f, -0.8f, 0.250f, 0.750f,
            -0.8f,  0.8f, -0.8f, 0.250f, 0.875f,

             0.8f,  0.8f,  0.8f, 0.500f, 0.875f,
             0.8f, -0.8f,  0.8f, 0.500f, 0.750f,
            -0.8f, -0.8f,  0.8f, 0.375f, 0.750f,
            -0.8f,  0.8f,  0.8f, 0.375f, 0.875f,

             0.8f, -0.8f,  0.8f, 0.000f, 0.750f,
             0.8f, -0.8f, -0.8f, 0.125f, 0.750f,
             0.8f,  0.8f, -0.8f, 0.125f, 0.875f,
             0.8f,  0.8f,  0.8f, 0.000f, 0.875f,

            -0.8f, -0.8f,  0.8f, 0.375f, 0.750f,
            -0.8f, -0.8f, -0.8f, 0.250f, 0.750f,
            -0.8f,  0.8f, -0.8f, 0.250f, 0.875f,
            -0.8f,  0.8f,  0.8f, 0.375f, 0.875f,

            -0.8f, -0.8f,  0.8f, 0.375f, 1.000f,
             0.8f, -0.8f,  0.8f, 0.250f, 1.000f,
             0.8f, -0.8f, -0.8f, 0.250f, 0.875f,
            -0.8f, -0.8f, -0.8f, 0.375f, 0.875f,

            -0.8f,  0.8f,  0.8f, 0.250f, 1.000f,
             0.8f,  0.8f,  0.8f, 0.125f, 1.000f,
             0.8f,  0.8f, -0.8f, 0.125f, 0.875f,
            -0.8f,  0.8f, -0.8f, 0.250f, 0.875f
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

        private static readonly Shader shader = Shader.GetSkinRendererShader();

        public static void Setup()
        {
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);

            ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.DynamicDraw);

            shader.Use();

            int vertexLocation = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        }

        public static void Render(Camera camera, double time)
        {
            shader.Use();

            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());

            Matrix4 model = Matrix4.Identity *
            Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(time)) *
            Matrix4.CreateTranslation(new Vector3(0, 3f, 0));
            shader.SetMatrix4("model", model);

            GL.BindVertexArray(vao);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
