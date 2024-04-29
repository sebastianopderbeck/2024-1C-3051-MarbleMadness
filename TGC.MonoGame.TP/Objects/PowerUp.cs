using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class PowerUps{

        public const string ContentFolder3D = "Models/";
        public Model PowerUpsModel{get; set;}
        public Matrix[] PowerUpsWorlds{get; set;}

        public PowerUps(){            
            PowerUpsWorlds = new Matrix[]{};
        }

        public void agregarPowerUp(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoPulpito = new Matrix[]{
                Matrix.CreateTranslation(Position) * escala,
            };
            PowerUpsWorlds = PowerUpsWorlds.Concat(nuevoPulpito).ToArray();
        }

        public void LoadContent(ContentManager Content){
            PowerUpsModel = Content.Load<Model>(ContentFolder3D + "shared/rocket");
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            for(int i=0; i < PowerUpsWorlds.Length; i++){
                Matrix _pulpitoWorld = PowerUpsWorlds[i];
                PowerUpsModel.Draw(_pulpitoWorld, view, projection);
            }
        }
    }
}