using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Robot{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model RobotModel{get; set;}
        public Matrix[] RobotWorlds{get; set;}
        public Effect Effect { get; set; }

        public Robot(){            
            RobotWorlds = new Matrix[]{};
        }

        public void AgregarRobot(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.01f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoRobot = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            RobotWorlds = RobotWorlds.Concat(nuevoRobot).ToArray();
        }

        public void LoadContent(ContentManager Content){
            RobotModel = Content.Load<Model>(ContentFolder3D + "shared/speakerrobot");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in RobotModel.Meshes)
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
            foreach (var mesh in RobotModel.Meshes)
            {
                
                for(int i=0; i < RobotWorlds.Length; i++){
                    Matrix _robotWorld = RobotWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _robotWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}