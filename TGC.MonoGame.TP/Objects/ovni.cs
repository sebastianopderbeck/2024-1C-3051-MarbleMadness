using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Ovni{

        public const string ContentFolder3D = "Models/";
        public Model OvniModel{get; set;}
        public Matrix[] OvniWorlds{get; set;}
        
        //falta hacer el constructor ovni
        public Ovni(){
            OvniWorlds = new Matrix[]{};
        }

        public void agregarOvni(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoOvni = new Matrix[]{
                Matrix.CreateTranslation(Position) * escala,
            };
            OvniWorlds = OvniWorlds.Concat(nuevoOvni).ToArray();
        }

        public void LoadContent(ContentManager Content){
            OvniModel = Content.Load<Model>(ContentFolder3D + "shared/UFO");
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            for(int i = 0 ; i < OvniWorlds.Length; i++){
                Matrix _ovniWorld = OvniWorlds[i];
                OvniModel.Draw(_ovniWorld, view, projection);
            }
        }
    }
}