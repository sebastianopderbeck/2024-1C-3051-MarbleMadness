using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class NaveSW{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model NaveSWModel{get; set;}
        public Matrix[] NaveSWWorlds{get; set;}
        public Effect Effect { get; set; }

        public NaveSW(){
            NaveSWWorlds = new Matrix[]{};
        }

        public void AgregarNaveSW(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.08f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var newNaveSW = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            NaveSWWorlds = NaveSWWorlds.Concat(newNaveSW).ToArray();
        }

        public void LoadContent(ContentManager Content){
            NaveSWModel = Content.Load<Model>(ContentFolder3D + "shared/NaveSW");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in NaveSWModel.Meshes)
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
            foreach (var mesh in NaveSWModel.Meshes)
            {
                
                for(int i=0; i < NaveSWWorlds.Length; i++){
                    Matrix _naveSWWorld = NaveSWWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _naveSWWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}