using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP
{

    public class Luna
    {

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model LunaModel { get; set; }
        public Matrix[] LunaWorlds { get; set; }
        public Effect Effect { get; set; }

        public Luna()
        {
            LunaWorlds = new Matrix[] { };
        }

        public void AgregarLuna(Vector3 Position)
        {
            Matrix escala = Matrix.CreateScale(0.1f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoRobot = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            LunaWorlds = LunaWorlds.Concat(nuevoRobot).ToArray();
        }

        public void LoadContent(ContentManager Content)
        {
            LunaModel = Content.Load<Model>(ContentFolder3D + "shared/Luna");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in LunaModel.Meshes)
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
            foreach (var mesh in LunaModel.Meshes)
            {

                for (int i = 0; i < LunaWorlds.Length; i++)
                {
                    Matrix _cartelWorld = LunaWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _cartelWorld);
                    mesh.Draw();
                }

            }
        }
    }
}