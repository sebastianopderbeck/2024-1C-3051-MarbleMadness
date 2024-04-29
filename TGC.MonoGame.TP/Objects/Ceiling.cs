using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TGC.MonoGame.TP.Objects
{
    public class Ceiling
    {

        public const string ContentFolder3D = "Models/";
        public Model CeilingModel { get; set; }
        public Matrix[] CeilingWorlds { get; set; }
        public Ceiling() {
            CeilingWorlds = new Matrix[] { };
        }

        public void AgregarCeiling(Vector3 Position) {
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoCeiling = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            CeilingWorlds = CeilingWorlds.Concat(nuevoCeiling).ToArray();
        }

        public void LoadContent(ContentManager Content) {
            CeilingModel = Content.Load<Model>(ContentFolder3D + "shared/ceiling");
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection) {
            for (int i = 0; i < CeilingWorlds.Length; i++) {
                Matrix _ceilingWorld = CeilingWorlds[i];
                CeilingModel.Draw(_ceilingWorld, view, projection);
            }
        }
    }
}
