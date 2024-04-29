
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP.Objects
{
    public class Wall
    {

        public const string ContentFolder3D = "Models/";

        public Model WallModel { get; set; }
        public Matrix[] WallWorlds { get; set; }

        public Wall()
        {
            this.WallWorlds = new Matrix[] { };
        }

        public void AgregarWall(Vector3 Position)
        {
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevaWall = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            WallWorlds = WallWorlds.Concat(nuevaWall).ToArray();
        }

        public void LoadContent(ContentManager Content)
        {
            WallModel = Content.Load<Model>(ContentFolder3D + "shared/wall");
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            for (int i = 0; i < WallWorlds.Length; i++)
            {
                Matrix _wallWorld = WallWorlds[i];
                WallModel.Draw(_wallWorld, view, projection);
            }
        }

    }
}
