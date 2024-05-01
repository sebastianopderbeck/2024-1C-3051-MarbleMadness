using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Cardboard{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model CardboardModel{get; set;}
        public Matrix[] CardboardWorlds{get; set;}
        public Effect Effect { get; set; }

        public Cardboard(){
            CardboardWorlds = new Matrix[]{};
        }

        public void AgregarCardboard(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.3f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var newCarboard = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            CardboardWorlds = CardboardWorlds.Concat(newCarboard).ToArray();
        }

        public void LoadContent(ContentManager Content){
            CardboardModel = Content.Load<Model>(ContentFolder3D + "shared/Cardboard");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in CardboardModel.Meshes)
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
            Effect.Parameters["DiffuseColor"].SetValue(Color.Red.ToVector3());
            foreach (var mesh in CardboardModel.Meshes)
            {
                
                for(int i=0; i < CardboardWorlds.Length; i++){
                    Matrix _cardboardWorld = CardboardWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _cardboardWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}