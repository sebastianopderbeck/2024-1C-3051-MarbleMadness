using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Plank{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model PulpitoModel{get; set;}
        public Matrix[] PulpitoWorlds{get; set;}
        public Effect Effect { get; set; }

        public Plank(){            
            PulpitoWorlds = new Matrix[]{};
        }

        public void agregarPulpito(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.01f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoPulpito = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            PulpitoWorlds = PulpitoWorlds.Concat(nuevoPulpito).ToArray();
        }

        public void LoadContent(ContentManager Content){
            PulpitoModel = Content.Load<Model>(ContentFolder3D + "shared/Octopus");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in PulpitoModel.Meshes)
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
            foreach (var mesh in PulpitoModel.Meshes)
            {
                
                for(int i=0; i < PulpitoWorlds.Length; i++){
                    Matrix _pulpitoWorld = PulpitoWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _pulpitoWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}