
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP.Objects
{
    public class Portal
    {

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";

        public Model portalModel { get; set; }
        public Matrix[] portalWorlds { get; set; }
        public Effect Effect { get; set; }

        public Portal()
        {
            this.portalWorlds = new Matrix[] { };
        }

        public void AgregarPortal(Vector3 Position)
        {
            Matrix escala = Matrix.CreateScale(0.009f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevaRampa = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            portalWorlds = portalWorlds.Concat(nuevaRampa).ToArray();
        }

        public void LoadContent(ContentManager Content)
        {
            portalModel = Content.Load<Model>(ContentFolder3D + "shared/Pedestal");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in portalModel.Meshes)
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
            foreach (var mesh in portalModel.Meshes)
            {
                
                for(int i=0; i < portalWorlds.Length; i++){
                    Matrix _rampaWorld = portalWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _rampaWorld);
                    mesh.Draw();
                }
                
            }
        }

    }
}
