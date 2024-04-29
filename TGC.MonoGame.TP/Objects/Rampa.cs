
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP.Objects
{
    public class Rampa
    {

        public const string ContentFolder3D = "Models/";

        public Model RampaModel { get; set; }
        public Matrix[] RampaWorlds { get; set; }

        public Rampa()
        {
            this.RampaWorlds = new Matrix[] { };
        }

        public void AgregarRampa(Vector3 Position)
        {
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevaRampa = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            RampaWorlds = RampaWorlds.Concat(nuevaRampa).ToArray();
        }

        public void LoadContent(ContentManager Content)
        {
            RampaModel = Content.Load<Model>(ContentFolder3D + "shared/rampa");
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            for (int i = 0; i < RampaWorlds.Length; i++)
            {
                Matrix _rampaWorld = RampaWorlds[i];
                RampaModel.Draw(_rampaWorld, view, projection);
            }
        }

    }
}
