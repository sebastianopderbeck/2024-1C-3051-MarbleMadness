using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TGC.MonoGame.TP{
    
    public class Ball{

        public const string ContentFolder3D = "Models/";
        public Model BallModel{get; set;}
        public Matrix BallWorld{get; set;}

        public Ball(Vector3 Position){
            BallWorld = Matrix.CreateScale(.024f) * Matrix.CreateTranslation(Position);
        }

        public void LoadContent(ContentManager Content){
            BallModel = Content.Load<Model>(ContentFolder3D + "sphere/sphere");
        }

        public void Update(GameTime gameTime){
            
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            BallModel.Draw(BallWorld, view, projection);
        }
    }
}