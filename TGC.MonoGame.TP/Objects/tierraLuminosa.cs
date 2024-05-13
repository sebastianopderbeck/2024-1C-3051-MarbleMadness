using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class TierraLuminosa{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model TierraLuminosaModel{get; set;}
        public Matrix[] TierraLuminosaWorlds{get; set;}
        public Effect Effect { get; set; }

        public TierraLuminosa(){            
            TierraLuminosaWorlds = new Matrix[]{};
        }

        public void agregarTierraLuminosa(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.8f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevaTierraLuminosa = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            TierraLuminosaWorlds = TierraLuminosaWorlds.Concat(nuevaTierraLuminosa).ToArray();
        }

        public void LoadContent(ContentManager Content){
            TierraLuminosaModel = Content.Load<Model>(ContentFolder3D + "shared/TierraLuminosa");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in TierraLuminosaModel.Meshes)
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
            Effect.Parameters["DiffuseColor"].SetValue(Color.Pink.ToVector3());
            foreach (var mesh in TierraLuminosaModel.Meshes)
            {
                
                for(int i=0; i < TierraLuminosaWorlds.Length; i++){
                    Matrix _tierraWorld = TierraLuminosaWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _tierraWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}