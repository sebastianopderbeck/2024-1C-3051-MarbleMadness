using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP
{

    public class Asteroide
    {

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model AsteroideModel { get; set; }
        public Matrix[] AsteroideWorlds { get; set; }
        public Effect Effect { get; set; }

        public Asteroide()
        {
            AsteroideWorlds = new Matrix[] { };
        }

        public void AgregarAsteroide(Vector3 Position)
        {
            Matrix escala = Matrix.CreateScale(0.05f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoRobot = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            AsteroideWorlds = AsteroideWorlds.Concat(nuevoRobot).ToArray();
        }

        public void LoadContent(ContentManager Content)
        {
            AsteroideModel = Content.Load<Model>(ContentFolder3D + "shared/Asteroide");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in AsteroideModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.Green.ToVector3());
            foreach (var mesh in AsteroideModel.Meshes)
            {

                for (int i = 0; i < AsteroideWorlds.Length; i++)
                {
                    Matrix _cartelWorld = AsteroideWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _cartelWorld);
                    mesh.Draw();
                }

            }
        }
    }
}