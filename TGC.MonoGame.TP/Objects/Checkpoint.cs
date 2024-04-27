using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Checkpoint{

        public const string ContentFolder3D = "Models/";
        public Model CheckpointModel{get; set;}
        public Matrix CheckpointWorld{get; set;}

        public Checkpoint(ContentManager Content){
            CheckpointModel = Content.Load<Model>(ContentFolder3D + "shared/Checkpoint");
            CheckpointWorld = Matrix.Identity * Matrix.CreateScale(.1f);
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            CheckpointModel.Draw(CheckpointWorld, view, projection);
        }
    }
}