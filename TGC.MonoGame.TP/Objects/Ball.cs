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
        public const string ContentFolderEffects = "Effects/";
        public Model BallModel{get; set;}
        public Matrix BallWorld{get; set;}
        public Effect Effect { get; set; }

        public Ball(Vector3 Position){
            BallWorld = Matrix.CreateScale(.024f) * Matrix.CreateTranslation(Position);
        }

        public void LoadContent(ContentManager Content){
            BallModel = Content.Load<Model>(ContentFolder3D + "sphere/sphere");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");
            foreach (var mesh in BallModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
        }

        public void Update(GameTime gameTime){
            
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            Effect.Parameters["View"].SetValue(view); //Cambio View por Eso
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.Blue.ToVector3());
            foreach (var mesh in BallModel.Meshes)
            {
                Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * BallWorld);
                mesh.Draw();
            }
        }
    }
}