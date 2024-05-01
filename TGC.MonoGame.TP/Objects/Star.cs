
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP.Objects
{
    public class Star
    {

        public const string ContentFolder3D = "Models/";

        public Model StarModel { get; set; }
        public Matrix[] StarWorlds { get; set; }

        public Star()
        {
            this.StarWorlds = new Matrix[] { };
        }

        public void AgregarStar(Vector3 Position)
        {
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevaStar = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            StarWorlds = StarWorlds.Concat(nuevaStar).ToArray();
        }

        public void LoadContent(ContentManager Content)
        {
            StarModel = Content.Load<Model>(ContentFolder3D + "shared/star");
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            for (int i = 0; i < StarWorlds.Length; i++)
            {
                Matrix _starWorld = StarWorlds[i];
                StarModel.Draw(_starWorld, view, projection);
            }
        }

    }
}
