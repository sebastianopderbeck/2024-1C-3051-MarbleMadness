using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Ovni{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model OvniModel{get; set;}
        public Matrix[] OvniWorlds{get; set;}
        public Effect Effect { get; set; }

        //falta hacer el constructor ovni
        public Ovni(){
            OvniWorlds = new Matrix[]{};
        }

        public void agregarOvni(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.055f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoOvni = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            OvniWorlds = OvniWorlds.Concat(nuevoOvni).ToArray();
        }

        public void LoadContent(ContentManager Content){
            OvniModel = Content.Load<Model>(ContentFolder3D + "shared/UFO/UFOEnemigo");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in OvniModel.Meshes)
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
            Effect.Parameters["DiffuseColor"].SetValue(Color.White.ToVector3());
            foreach (var mesh in OvniModel.Meshes)
            {

                for (int i = 0; i < OvniWorlds.Length; i++)
                {
                    Matrix _cartelWorld = OvniWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _cartelWorld);
                    mesh.Draw();
                }

            }
        }
    }
}