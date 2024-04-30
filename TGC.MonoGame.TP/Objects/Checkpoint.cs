using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Checkpoint{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model CheckpointModel{get; set;}
        public Matrix[] CheckpointWorlds{get; set;}
        public Effect Effect { get; set; }

        public Checkpoint(){
            CheckpointWorlds = new Matrix[]{};
        }

        public void AgregoCheckpoint(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.02f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoCheckpoint = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            CheckpointWorlds = CheckpointWorlds.Concat(nuevoCheckpoint).ToArray();
        }

        public void LoadContent(ContentManager Content){
            CheckpointModel = Content.Load<Model>(ContentFolder3D + "shared/Checkpoint");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in CheckpointModel.Meshes)
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
            foreach (var mesh in CheckpointModel.Meshes)
            {
                
                for(int i=0; i < CheckpointWorlds.Length; i++){
                    Matrix _checkpointWorld = CheckpointWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _checkpointWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}