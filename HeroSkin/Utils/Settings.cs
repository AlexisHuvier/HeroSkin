using System.IO;
using Newtonsoft.Json;

namespace HeroSkin.Utils
{
    public class Settings
    {
        public bool is4PxModel = true;
        public float frequency = 2f;
        public int octavesNumber = 5;
        public float saturationForce = 0.2f;

        public Settings(bool is4PxModel, float frequency, int octavesNumber, float saturationForce)
        {
            this.is4PxModel = is4PxModel;
            this.frequency = frequency;
            this.octavesNumber = octavesNumber;
            this.saturationForce = saturationForce;
        }

        public void Save(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static Settings FromFile(string file, bool is4PxModel = true, float frequency = 2f, int octavesNumber = 5, float saturationForce = 0.2f)
        {
            if(File.Exists(file))
            {
                return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(file));
            }
            Settings set = new Settings(is4PxModel, frequency, octavesNumber, saturationForce);
            set.Save(file);
            return set;
        }
    }
}
