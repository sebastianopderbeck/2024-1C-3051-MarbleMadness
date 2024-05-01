using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Asteroide{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model AsteroideModel{get; set;}
        public Matrix[] AsteroideWorlds{get; set;}
        public Effect Effect { get; set; }

        public Asteroide(){            
            AsteroideWorlds = new Matrix[]{};
        }

        public void agregarAsteroide(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.01f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoAsteroide = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            AsteroideWorlds = AsteroideWorlds.Concat(nuevoAsteroide).ToArray();
        }

        public void LoadContent(ContentManager Content){
            AsteroideModel = Content.Load<Model>(ContentFolder3D + "shared/Octopus");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in AsteroideModel.Meshes)
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
            foreach (var mesh in AsteroideModel.Meshes)
            {
                
                for(int i=0; i < AsteroideWorlds.Length; i++){
                    Matrix _pulpitoWorld = AsteroideWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _pulpitoWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}