using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class WarpGate{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model WarpGateModel{get; set;}
        public Matrix[] WarpGateWorlds{get; set;}
        public Effect Effect { get; set; }

        public WarpGate(){
            WarpGateWorlds = new Matrix[]{};
        }

        public void AgregarWarpGate(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.02f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoWarpGate = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            WarpGateWorlds = WarpGateWorlds.Concat(nuevoWarpGate).ToArray();
        }

        public void LoadContent(ContentManager Content){
            WarpGateModel = Content.Load<Model>(ContentFolder3D + "shared/WarpGate");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in WarpGateModel.Meshes)
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
            Effect.Parameters["DiffuseColor"].SetValue(Color.Green.ToVector3());
            foreach (var mesh in WarpGateModel.Meshes)
            {
                
                for(int i=0; i < WarpGateWorlds.Length; i++){
                    Matrix _warpGateWorld = WarpGateWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _warpGateWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}