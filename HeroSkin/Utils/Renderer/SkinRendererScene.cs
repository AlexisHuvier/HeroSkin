using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;

namespace HeroSkin.Utils.Renderer
{
    public static class SkinRendererScene
    {
        private static double _time;

        private readonly static Camera camera = new Camera(Vector3.UnitZ * 10, 1f);
        private static Texture texture;

        public static void SetTexture(Bitmap bitmap)
        {
            texture = Texture.LoadFromBitmap(bitmap, TextureFilter.Nearest, TextureFilter.Nearest);
        }

        public static void Ready()
        {
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.ClearColor(Color4.DarkGray);

            Models.Head.Setup();
            Models.Body.Setup();
            Models.RightArm4px.Setup();
            Models.LeftArm4px.Setup();
            Models.LeftLeg4px.Setup();
            Models.RightLeg4px.Setup();
        }

        public static void Render()
        {
            _time += 0.2;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (texture != null)
            {
                texture.Use(OpenTK.Graphics.OpenGL4.TextureUnit.Texture0);
                Models.Head.Render(camera, _time);
                Models.Body.Render(camera, _time);
                Models.RightArm4px.Render(camera, _time);
                Models.LeftArm4px.Render(camera, _time);
                Models.LeftLeg4px.Render(camera, _time);
                Models.RightLeg4px.Render(camera, _time);
            }

            GL.Finish();
        }
    }
}
