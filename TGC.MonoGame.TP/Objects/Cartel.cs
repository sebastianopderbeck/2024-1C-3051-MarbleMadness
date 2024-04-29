using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Cartel{

        public const string ContentFolder3D = "Models/";
        public Model CartelpointModel{get; set;}
        public Matrix[] CartelpointWorlds{get; set;}
        //falta hacer el constructor checkpoint
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
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            for(int i=0; i < CartelpointWorlds.Length; i++){
                Matrix _cartelWorld = CartelpointWorlds[i];
                CartelpointModel.Draw(_cartelWorld, view, projection);
            }
        }
    }
}