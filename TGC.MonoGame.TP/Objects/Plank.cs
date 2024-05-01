using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Plank{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model PlankModel{get; set;}
        public Matrix[] PlankWorlds{get; set;}
        public Effect Effect { get; set; }

        public Plank(){            
            PlankWorlds = new Matrix[]{};
        }

        public void AgregarPlank(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.01f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoPlank = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            PlankWorlds = PlankWorlds.Concat(nuevoPlank).ToArray();
        }

        public void LoadContent(ContentManager Content){
            PlankModel = Content.Load<Model>(ContentFolder3D + "shared/thin");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in PlankModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.Yellow.ToVector3());
            foreach (var mesh in PlankModel.Meshes)
            {
                
                for(int i=0; i < PlankWorlds.Length; i++){
                    Matrix _plankWorld = PlankWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _plankWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}