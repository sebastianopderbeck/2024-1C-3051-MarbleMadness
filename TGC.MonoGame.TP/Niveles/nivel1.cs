using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP;

namespace TGC.MonoGame.niveles{
    public class Nivel1{

        public const string ContentFolder3D = "Models/";
        public Model PisoModel { get; set; }
        public Matrix[] PisoWorlds { get; set; }
        public Model ParedModel { get; set; }
        public Matrix[] ParedWorlds { get; set; }
        public Ball Bola { get; set; }
        public Checkpoint Checkpoint { get; set; }
        public Ovni Ovnis { get; set; }
        public Pulpito Pulpito { get; set; }
        public Cartel Carteles { get; set; }
        public PowerUps PowerUps{ get; set; }

        public const float DistanceBetweenFloor = 411f;
        public const float DistanceBetweenWall = 600f;
        public Matrix escala = Matrix.CreateScale(0.03f);
        public Vector3 arriba = new Vector3(0f, 50f, 0f);
        
        // ____ World matrices ____
        //matrices de las plataformas fijas (pisos)
        //matrices tipo lista para que tengan los pisos flotantes
         

        public Nivel1() {

            Ovnis = new Ovni();
            Bola = new Ball(new (0f,200f,0f));
            Pulpito = new Pulpito();
            Checkpoint = new Checkpoint();
            Carteles = new Cartel();
            PowerUps = new PowerUps();

            Initialize();
        }
        
        private void Initialize() {
            //PisoWorld = Matrix.Identity * Matrix.CreateScale(.05f);
            PisoWorlds = new Matrix[]{
                // Matrix.Identity * Matrix.CreateScale(0.1f),
                // Matrix.CreateTranslation(411f, 50f, 411f)* Matrix.CreateScale(0.1f),
                
                //base de incio (hay que una funcion que se encarte de recibir las posiciones y te devuelve la base esta)
                Matrix.Identity * escala,
                Matrix.CreateTranslation(Vector3.Right * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation(Vector3.Left * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation((Vector3.Forward + Vector3.Right) * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation((Vector3.Backward + Vector3.Right) * DistanceBetweenFloor) * escala,
                Matrix.CreateTranslation((Vector3.Backward + Vector3.Left) * DistanceBetweenFloor) * escala,
                //Camino inicial
                Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor) * 2) * escala,
                //Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 3) * escala -- espacio vacio de salto
                Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 4) * escala,
                //escaleras
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 5 + 50)) + arriba * 4) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 6 + 100)) + arriba * 8) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150)) + arriba * 12) * escala,
                //segundo camindo largo
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor) + arriba * 12) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 2) + arriba * 12) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 3) + arriba * 12) * escala,
                //Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 4) + arriba * 12) * escala - primer espacio vaio
                //Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 5) + arriba * 12) * escala - segundo espacio vacio
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 6) + arriba * 12) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 7) + arriba * 12) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 8) + arriba * 12) * escala,
                //base final
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 9) + arriba * 12) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 9 + Vector3.Backward * DistanceBetweenFloor) + arriba * 12) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 9 + Vector3.Forward * DistanceBetweenFloor) + arriba * 12) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 10) + arriba * 12) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 10 + Vector3.Backward * DistanceBetweenFloor) + arriba * 12) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 10 + Vector3.Forward * DistanceBetweenFloor) + arriba * 12) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 11) + arriba * 12) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 11 + Vector3.Backward * DistanceBetweenFloor) + arriba * 12) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 11 + Vector3.Forward * DistanceBetweenFloor) + arriba * 12) * escala,
            };

            ParedWorlds = new Matrix[]{
                // Pared inicio
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

                // Pared final
                //Pared Fondo
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 11 + Vector3.Forward * DistanceBetweenFloor) + arriba * 12)
                    * Matrix.CreateTranslation(Vector3.Left * 200f  + arriba * 2) * escala,
                //Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 11) + arriba * 12)
                    //* Matrix.CreateTranslation(Vector3.Left * 200f  + arriba * 2) * escala,
                Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 11 + Vector3.Backward * DistanceBetweenFloor) + arriba * 12)
                    * Matrix.CreateTranslation(Vector3.Left * 200f  + arriba * 2) * escala,
                //Pared Derecha  
            };
            Ovnis.agregarOvni((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 2) + arriba * 12);
            Ovnis.agregarOvni((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 7) + arriba * 12);
            Pulpito.agregarPulpito((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor);
            Checkpoint.AgregoCheckpoint(Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 10 + Vector3.Up * 500f);
            Carteles.AgregarCartel(Vector3.Forward * DistanceBetweenFloor * 3 + Vector3.Left * DistanceBetweenFloor/2 + arriba * 4);
            Carteles.AgregarCartel((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 4 + Vector3.Backward * DistanceBetweenFloor) + arriba * 16);
            PowerUps.agregarPowerUp((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 3) + arriba * 12);
        }

        public void LoadContent(ContentManager Content){
            PisoModel = Content.Load<Model>(ContentFolder3D + "shared/Ceiling");
            ParedModel = Content.Load<Model>(ContentFolder3D + "shared/Wall");
            Bola.LoadContent(Content);
            Checkpoint.LoadContent(Content);
            Ovnis.LoadContent(Content);
            Pulpito.LoadContent(Content);
            Carteles.LoadContent(Content);
            PowerUps.LoadContent(Content);
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

            Bola.Draw(gameTime, view, projection);
            Checkpoint.Draw(gameTime, view, projection);
            Ovnis.Draw(gameTime, view, projection);
            Pulpito.Draw(gameTime, view, projection);
            Carteles.Draw(gameTime, view, projection);
            PowerUps.Draw(gameTime, view, projection);

        }

    }
}