using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Checkpoint{

        public const string ContentFolder3D = "Models/";
        public Model CheckpointModel{get; set;}
        public Matrix[] CheckpointWorlds{get; set;}
        //falta hacer el constructor checkpoint
        public Checkpoint(){
            CheckpointWorlds = new Matrix[]{};
        }

        public void AgregoCheckpoint(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoCheckpoint = new Matrix[]{
                Matrix.CreateTranslation(Position) * escala,
            };
            CheckpointWorlds = CheckpointWorlds.Concat(nuevoCheckpoint).ToArray();
        }

        public void LoadContent(ContentManager Content){
            CheckpointModel = Content.Load<Model>(ContentFolder3D + "shared/Checkpoint");
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            for(int i = 0 ; i < CheckpointWorlds.Length; i++){
                Matrix _checkpointWorld = CheckpointWorlds[i];
                CheckpointModel.Draw(_checkpointWorld, view, projection);
            }
        }
    }
}