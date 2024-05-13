using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Cartel{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model CartelpointModel{get; set;}
        public Matrix[] CartelpointWorlds{get; set;}
        public Effect Effect { get; set; }

        public Cartel(){
            CartelpointWorlds = new Matrix[]{};
        }

        public void AgregarCartel(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoCartel = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            CartelpointWorlds = CartelpointWorlds.Concat(nuevoCartel).ToArray();
        }

        public void LoadContent(ContentManager Content){
            CartelpointModel = Content.Load<Model>(ContentFolder3D + "shared/alert");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");
            foreach (var mesh in CartelpointModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            Effect.Parameters["View"].SetValue(view); //Cambio View por Eso
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.Red.ToVector3());
            foreach (var mesh in CartelpointModel.Meshes)
            {
                
                for(int i=0; i < CartelpointWorlds.Length; i++){
                    Matrix _cartelWorld = CartelpointWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _cartelWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}
