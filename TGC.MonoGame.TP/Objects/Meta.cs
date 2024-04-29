
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP.Objects
{
    public class Meta
    {

        public const string ContentFolder3D = "Models/";

        public Model MetaModel { get; set; }
        public Matrix[] MetaWorlds { get; set; }

        public Meta()
        {
            this.MetaWorlds = new Matrix[] { };
        }

        public void AgregarMeta(Vector3 Position)
        {
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevaMeta = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            MetaWorlds = MetaWorlds.Concat(nuevaMeta).ToArray();
        }

        public void LoadContent(ContentManager Content)
        {
            MetaModel = Content.Load<Model>(ContentFolder3D + "shared/meta");
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            for (int i = 0; i < MetaWorlds.Length; i++)
            {
                Matrix _metaWorld = MetaWorlds[i];
                MetaModel.Draw(_metaWorld, view, projection);
            }
        }

    }
}
