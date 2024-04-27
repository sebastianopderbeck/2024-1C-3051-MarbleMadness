using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TGC.MonoGame.TP{
    
    class Ball{

        public const string ContentFolder3D = "Models/";
        public Model BallModel{get; set;}
        public Matrix BallWorld{get; set;}

        public Ball(ContentManager Content){
            BallModel = Content.Load<Model>(ContentFolder3D + "sphere/sphere");

            BallWorld = Matrix.Identity * Matrix.CreateScale(.03f);
        }

        public void Update(GameTime gameTime){
            
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            BallModel.Draw(BallWorld, view, projection);
        }
    }
}