using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class EstrellaMuerte{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model EstrellaMuerteModel{get; set;}
        public Matrix[] EstrellaMuerteWorlds{get; set;}
        public Effect Effect { get; set; }

        public EstrellaMuerte(){            
            EstrellaMuerteWorlds = new Matrix[]{};
        }

        public void agregarEstrellaMuerte(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.01f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevaEstrellaMuerte = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            EstrellaMuerteWorlds = EstrellaMuerteWorlds.Concat(nuevaEstrellaMuerte).ToArray();
        }

        public void LoadContent(ContentManager Content){
            EstrellaMuerteModel = Content.Load<Model>(ContentFolder3D + "shared/Octopus"); //falta agregarlo a la carpeta de contents
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in EstrellaMuerteModel.Meshes)
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
            foreach (var mesh in EstrellaMuerteModel.Meshes)
            {
                
                for(int i=0; i < EstrellaMuerteWorlds.Length; i++){
                    Matrix _tierraWorld = EstrellaMuerteWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _tierraWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}