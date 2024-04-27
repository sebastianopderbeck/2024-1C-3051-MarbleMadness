using System;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TGC.MonoGame.TP{
    class Nivel1{

        public const string ContentFolder3D = "Models/";
        public Model NivelModel;
        public Matrix NivelWorld;

        public Nivel1(ContentManager Content){
            NivelModel = Content.Load<Model>(ContentFolder3D + "nivel1/nivel1");

            NivelWorld = Matrix.Identity * Matrix.CreateScale(.05f);
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){

            NivelModel.Draw(NivelWorld, view, projection);

        }

    }
}