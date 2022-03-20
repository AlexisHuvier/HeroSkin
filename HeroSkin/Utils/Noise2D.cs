using System;
using System.Linq;

namespace HeroSkin.Utils
{
    public static class Noise2D
    {
        private static Random random = new Random();
        private static int[] permutation;
        private static Vec2[] gradients;

        static Noise2D()
        {
            CalculatePermutation(out permutation);
            CalculateGradients(out gradients);
        }

        public static float Get(int x, int y, int octaves = 1, float amplitude = 1f, float frequency = 1f, int width = 64, int height = 64)
        {
            float result = 0;

            for(int octave = 0; octave < octaves; octave++)
            {
                result += Noise(x * frequency * 1f / width, y * frequency * 1f / height) * amplitude;

                frequency *= 2;
                amplitude /= 2;
            }

            return result;
        }

        public static void Reseed()
        {
            CalculatePermutation(out permutation);
        }

        public static float Noise(float x, float y)
        {
            Vec2 cell = new Vec2((float)Math.Floor(x), (float)Math.Floor(y));
            float total = 0;
            Vec2[] corners =
            {
                new Vec2(0, 0),
                new Vec2(0, 1),
                new Vec2(1, 0),
                new Vec2(1, 1)
            };

            foreach(Vec2 n in corners)
            {
                Vec2 ij = cell + n;
                Vec2 uv = new Vec2(x - ij.x, y - ij.y);

                int index = permutation[(int)ij.x % permutation.Length];
                index = permutation[(index + (int)ij.y) % permutation.Length];

                Vec2 grad = gradients[index % gradients.Length];

                total += Q(uv.x, uv.y) * Vec2.Dot(grad, uv);
            }

            return Math.Max(Math.Min(total, 1f), -1f);
        }

        private static void CalculatePermutation(out int[]p)
        {
            p = Enumerable.Range(0, 256).ToArray();

            for(var i = 0; i < p.Length; i++)
            {
                int source = random.Next(p.Length);
                int t = p[i];
                p[i] = p[source];
                p[source] = t;
            }
        }

        private static void CalculateGradients(out Vec2[] grad)
        {
            grad = new Vec2[256];

            for(int i = 0; i < grad.Length; i++)
            {
                Vec2 gradient;
                do
                    gradient = new Vec2((float)(random.NextDouble() * 2 - 1), (float)(random.NextDouble() * 2 - 1));
                while (gradient.LengthSquared() >= 1);

                gradient = gradient.Normalized();

                grad[i] = gradient;
            }
        }

        private static float Drop(float t)
        {
            t = Math.Abs(t);
            return 1f - t * t * t * (t * (t * 6 - 15) + 10);
        }

        private static float Q(float u, float v) => Drop(u) * Drop(v);
    }
}
