
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP.Objects
{
    public class Rampa
    {

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";

        public Model RampaModel { get; set; }
        public Matrix[] RampaWorlds { get; set; }
        public Effect Effect { get; set; }

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
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in RampaModel.Meshes)
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
            Effect.Parameters["DiffuseColor"].SetValue(Color.Yellow.ToVector3());
            foreach (var mesh in RampaModel.Meshes)
            {
                
                for(int i=0; i < RampaWorlds.Length; i++){
                    Matrix _rampaWorld = RampaWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _rampaWorld);
                    mesh.Draw();
                }
                
            }
        }

    }
}
