using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP.Objects
{
    public class Porg
    {

        public const string ContentFolder3D = "Models/";

        public Model PorgModel {  get; set; }
        public Matrix[] PorgWorlds { get; set; }

        public Porg() {
            this.PorgWorlds = new Matrix[] { };
        }

        public void AgregarPorg(Vector3 Position)
        {
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoPorg = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            PorgWorlds = PorgWorlds.Concat(nuevoPorg).ToArray();
        }

        public void LoadContent(ContentManager Content)
        {
            PorgModel = Content.Load<Model>(ContentFolder3D + "nivel4/Porg");
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            for (int i = 0; i < PorgWorlds.Length; i++)
            {
                Matrix _rampaWorld = PorgWorlds[i];
                PorgModel.Draw(_rampaWorld, view, projection);
            }
        }


    }
}
