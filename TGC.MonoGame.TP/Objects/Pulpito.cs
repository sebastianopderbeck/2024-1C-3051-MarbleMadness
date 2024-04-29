using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class Pulpito{

        public const string ContentFolder3D = "Models/";
        public Model PulpitoModel{get; set;}
        public Matrix[] PulpitoWorlds{get; set;}

        public Pulpito(){            
            PulpitoWorlds = new Matrix[]{};
        }

        public void agregarPulpito(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoPulpito = new Matrix[]{
                Matrix.CreateTranslation(Position) * escala,
            };
            PulpitoWorlds = PulpitoWorlds.Concat(nuevoPulpito).ToArray();
        }

        public void LoadContent(ContentManager Content){
            PulpitoModel = Content.Load<Model>(ContentFolder3D + "shared/Octopus");
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            for(int i=0; i < PulpitoWorlds.Length; i++){
                Matrix _pulpitoWorld = PulpitoWorlds[i];
                PulpitoModel.Draw(_pulpitoWorld, view, projection);
            }
        }
    }
}