using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TGC.MonoGame.niveles{
    public class Nivel1{

        public const string ContentFolder3D = "Models/";
        public Model PisoModel { get; set; }
        public Matrix[] PisoWorlds { get; set; }
        public Model ParedModel { get; set; }
        public Matrix[] ParedWorlds { get; set;}
        public const float DistanceBetweenFloor = 411f;
        public const float DistanceBetweenWall = 600f;

        
        // ____ World matrices ____
        //matrices de las plataformas fijas (pisos)
        //matrices tipo lista para que tengan los pisos flotantes
         

        public Nivel1() {
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            //PisoWorld = Matrix.Identity * Matrix.CreateScale(.05f);
            PisoWorlds = new Matrix[]{
                // Matrix.Identity * Matrix.CreateScale(0.1f),
                // Matrix.CreateTranslation(411f, 50f, 411f)* Matrix.CreateScale(0.1f),
                
                Matrix.Identity * escala,
                Matrix.CreateTranslation(Vector3.Right * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation(Vector3.Left * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation((Vector3.Forward + Vector3.Right) * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation((Vector3.Backward + Vector3.Right) * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation((Vector3.Backward + Vector3.Left) * DistanceBetweenFloor) * escala,
                ////Camino
                Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor) * 2) * escala,
                //Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 3) * escala,
                Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 4) * escala,
                Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 5) + arriba * 3) * escala,
            };

            ParedWorlds = new Matrix[]{
                //Pared Izquierda
                Matrix.CreateTranslation(Vector3.Left * DistanceBetweenWall + arriba * 2) * escala,
                Matrix.CreateTranslation(Vector3.Left * DistanceBetweenWall + (Vector3.Backward * DistanceBetweenFloor) + arriba * 2) * escala,
                Matrix.CreateTranslation(Vector3.Left * DistanceBetweenWall + (Vector3.Forward * DistanceBetweenFloor) + arriba * 2) * escala,
                // Pared derecha
                Matrix.CreateTranslation(Vector3.Right * DistanceBetweenWall + arriba * 2) * escala,
                Matrix.CreateTranslation(Vector3.Right * DistanceBetweenWall + (Vector3.Backward * DistanceBetweenFloor) + arriba * 2) * escala,
                Matrix.CreateTranslation(Vector3.Right * DistanceBetweenWall + (Vector3.Forward * DistanceBetweenFloor) + arriba * 2) * escala,
                // Pared atras
                Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenWall + arriba * 2) * escala,
                Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenWall + Vector3.Right * DistanceBetweenFloor + arriba * 2) * escala,
                Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenWall + Vector3.Left * DistanceBetweenFloor + arriba * 2) * escala,
                // Pared adelante
                Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenWall + Vector3.Right * DistanceBetweenFloor + arriba * 2) * escala,
                Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenWall + Vector3.Left * DistanceBetweenFloor + arriba * 2) * escala,
            };
        }
        
        private void Initialize() {
            
        }

        public void LoadContent(ContentManager Content){
            PisoModel = Content.Load<Model>(ContentFolder3D + "shared/Ceiling");
            ParedModel = Content.Load<Model>(ContentFolder3D + "shared/Wall");
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){

            //PisoModel.Draw(PisoWorlds, view, projection);
            for(int i=0; i < PisoWorlds.Length; i++){
                Matrix _pisoWorld = PisoWorlds[i];
                PisoModel.Draw(_pisoWorld, view, projection);
            }

            for(int i=0; i < ParedWorlds.Length; i++){
                Matrix _paredWorld = ParedWorlds[i];
                ParedModel.Draw(_paredWorld, view, projection);
                
            }

        }

    }
}