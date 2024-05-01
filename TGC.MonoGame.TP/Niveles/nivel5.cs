using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP;
using TGC.MonoGame.TP.Objects;

namespace TGC.MonoGame.niveles{
    public class Nivel5{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model PisoModel { get; set; }
        public Matrix[] PisoWorlds { get; set; }
        public Model ParedModel { get; set; }
        public Matrix[] ParedWorlds { get; set; }
        public Ball Bola { get; set; }
        public Checkpoint Checkpoint { get; set; }
        public Ovni Ovnis { get; set; }
        public Pulpito Pulpito { get; set; }
        public Cartel Carteles { get; set; }
        public PowerUps PowerUps { get; set; }
        public Rampa Rampas { get; set; }
        public Effect Effect { get; set; }

        public const float DistanceBetweenFloor = 12.33f;
        public const float DistanceBetweenWall = 18f;
        public Matrix escala = Matrix.CreateScale(0.03f);
        public Vector3 arriba = new Vector3(0f, 3.6f, 0f);
        public Vector3 alturaPisoPared = new(0f, 3.6f, 0f);
        public Vector3 alturaEscalera = new Vector3(0f, 6f, 0f);
        public Vector3 bajadaEscalera = new Vector3(0f, -6f, 0f);
        public const float distanciaEscaleras = 3f;
        
        // ____ World matrices ____
        //matrices de las plataformas fijas (pisos)
        //matrices tipo lista para que tengan los pisos flotantes
         

        public Nivel5() {

            Ovnis = new Ovni();
            Bola = new Ball(new (0f,10f,0f));
            Pulpito = new Pulpito();
            Checkpoint = new Checkpoint();
            Carteles = new Cartel();
            PowerUps = new PowerUps();
            Rampas = new Rampa(); 

            Initialize();
        }
        
        private void Initialize() {
            //PisoWorld = Matrix.Identity * Matrix.CreateScale(.05f);
            PisoWorlds = new Matrix[]{
                // Matrix.Identity * Matrix.CreateScale(0.1f),
                // Matrix.CreateTranslation(411f, 50f, 411f)* Matrix.CreateScale(0.1f),
                
                //base de incio (hay que una funcion que se encarte de recibir las posiciones y te devuelve la base esta)
                escala * Matrix.Identity,
                escala * Matrix.CreateTranslation(Vector3.Right * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Right) * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Backward + Vector3.Right) * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Backward + Vector3.Left) * DistanceBetweenFloor),
                //Camino inicial
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor) * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 3) + (Vector3.Right * DistanceBetweenFloor * 1)),
                //Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 3) * escala -- espacio vacio de salto
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 4) + (Vector3.Right * DistanceBetweenFloor * 2)),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 5) + (Vector3.Right * DistanceBetweenFloor * 1) + bajadaEscalera),
                //escaleras
                //escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 5 + distanciaEscaleras) + alturaEscalera),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 6) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 7) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 8) + bajadaEscalera * 2),

                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 9) + (Vector3.Right * DistanceBetweenFloor * 1) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 10) + (Vector3.Right * DistanceBetweenFloor * 2) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 11) + (Vector3.Right * DistanceBetweenFloor * 3) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 12) + (Vector3.Right * DistanceBetweenFloor * 4) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 14) + (Vector3.Right * DistanceBetweenFloor * 3.5f) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 14) + (Vector3.Right * DistanceBetweenFloor * 2.5f) + bajadaEscalera * 2),

                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 9) + (Vector3.Left * DistanceBetweenFloor * 1) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 10) + (Vector3.Left * DistanceBetweenFloor * 2) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 11) + (Vector3.Left * DistanceBetweenFloor * 3) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 12) + (Vector3.Left * DistanceBetweenFloor * 4) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 14) + (Vector3.Left * DistanceBetweenFloor * 3.5f) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 14) + (Vector3.Left * DistanceBetweenFloor * 2.5f) + bajadaEscalera * 2),

                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 14) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 15) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 16) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 17) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 18) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 19) + bajadaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 20) + bajadaEscalera * 2),

                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 26) + alturaEscalera),

                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 27) + (Vector3.Left * DistanceBetweenFloor * 1) + alturaEscalera * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 28) + alturaEscalera * 3),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 29) + (Vector3.Right * DistanceBetweenFloor * 1) + alturaEscalera * 4),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 30) + alturaEscalera * 5),

                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 1) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 2) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 3) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 4) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 5) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 6) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 6) + alturaEscalera * 5),

                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 29) + (Vector3.Left * DistanceBetweenFloor * 7) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 7) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 31) + (Vector3.Left * DistanceBetweenFloor * 7) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 29) + (Vector3.Left * DistanceBetweenFloor * 8) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 8) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 31) + (Vector3.Left * DistanceBetweenFloor * 8) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 29) + (Vector3.Left * DistanceBetweenFloor * 9) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 9) + alturaEscalera * 5),
                escala * Matrix.CreateTranslation((Vector3.Forward * DistanceBetweenFloor * 31) + (Vector3.Left * DistanceBetweenFloor * 9) + alturaEscalera * 5),

               
                
            };

            ParedWorlds = new Matrix[]{
                // Pared inicio
                //Pared Izquierda
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenWall + alturaPisoPared),
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenWall + Vector3.Backward * DistanceBetweenFloor + alturaPisoPared),
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenWall + Vector3.Forward * DistanceBetweenFloor + alturaPisoPared),
                // Pared derecha
                escala * Matrix.CreateTranslation(Vector3.Right * DistanceBetweenWall + alturaPisoPared),
                escala * Matrix.CreateTranslation(Vector3.Right * DistanceBetweenWall + Vector3.Backward * DistanceBetweenFloor + alturaPisoPared),
                escala * Matrix.CreateTranslation(Vector3.Right * DistanceBetweenWall + Vector3.Forward * DistanceBetweenFloor + alturaPisoPared),
                // Pared atras
                escala * Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenWall + alturaPisoPared),
                escala * Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenWall + Vector3.Right * DistanceBetweenFloor + alturaPisoPared),
                escala * Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenWall + Vector3.Left * DistanceBetweenFloor + alturaPisoPared),
                // Pared adelante
                escala * Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenWall + Vector3.Right * DistanceBetweenFloor + alturaPisoPared),
                escala * Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenWall + Vector3.Left * DistanceBetweenFloor + alturaPisoPared),

                // Pared final
                //Pared Fondo
                //escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 11 + Vector3.Forward * DistanceBetweenFloor + alturaEscalera * 3)
                  ///  * Matrix.CreateTranslation(Vector3.Left * 6f  + alturaPisoPared),
                //escala * Matrix.CreateTranslation((Vector3.Forward * (DistanceBetweenFloor * 7 + 150 ) + Vector3.Left * DistanceBetweenFloor * 11) + alturaEscalera * 3)
                    //* Matrix.CreateTranslation(Vector3.Left * 6f  + alturaPisoPared),
                //escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 11 + Vector3.Backward * DistanceBetweenFloor + alturaEscalera * 3)
                    //* Matrix.CreateTranslation(Vector3.Left * 6f  + alturaPisoPared),
                //Pared Derecha  
            };
            Ovnis.agregarOvni((Vector3.Forward * DistanceBetweenFloor * 7) + bajadaEscalera * 2);
            Ovnis.agregarOvni((Vector3.Forward * DistanceBetweenFloor * 11) + (Vector3.Right * DistanceBetweenFloor * 3) + bajadaEscalera * 2);
            Ovnis.agregarOvni((Vector3.Forward * DistanceBetweenFloor * 11) + (Vector3.Left * DistanceBetweenFloor * 3) + bajadaEscalera * 2);
            Ovnis.agregarOvni((Vector3.Forward * DistanceBetweenFloor * 15) + bajadaEscalera * 2);
            Ovnis.agregarOvni((Vector3.Forward * DistanceBetweenFloor * 17) + bajadaEscalera * 2);
            Ovnis.agregarOvni((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 1) + alturaEscalera * 5);
            Ovnis.agregarOvni((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 3) + alturaEscalera * 5);
            Ovnis.agregarOvni((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 5) + alturaEscalera * 5);
            Pulpito.agregarPulpito((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor);
            Checkpoint.AgregoCheckpoint((Vector3.Forward * DistanceBetweenFloor * 30) + (Vector3.Left * DistanceBetweenFloor * 8) + alturaEscalera * 5);
            Checkpoint.AgregoCheckpoint((Vector3.Forward * DistanceBetweenFloor * 14) + bajadaEscalera * 2);
            Carteles.AgregarCartel((Vector3.Forward * DistanceBetweenFloor * 9) + (Vector3.Right * DistanceBetweenFloor * 0.32f) + bajadaEscalera * 1.5f);
            Carteles.AgregarCartel((Vector3.Forward * DistanceBetweenFloor * 20) + (Vector3.Left * DistanceBetweenFloor * 0.75f)+ bajadaEscalera * 1.2f);
            PowerUps.agregarPowerUp((Vector3.Forward * DistanceBetweenFloor * 14) + (Vector3.Right * DistanceBetweenFloor * 3.5f) + bajadaEscalera * 2);
            PowerUps.agregarPowerUp((Vector3.Forward * DistanceBetweenFloor * 14) + (Vector3.Left * DistanceBetweenFloor * 3.5f) + bajadaEscalera * 2);
            PowerUps.agregarPowerUp((Vector3.Forward * DistanceBetweenFloor * 19) + bajadaEscalera * 2);
            Rampas.AgregarRampa((Vector3.Forward * DistanceBetweenFloor * 21.8f) + bajadaEscalera * 2);
        }

        public void LoadContent(ContentManager Content){
            PisoModel = Content.Load<Model>(ContentFolder3D + "shared/Ceiling");
            ParedModel = Content.Load<Model>(ContentFolder3D + "shared/Wall");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in PisoModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
            foreach (var mesh in ParedModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
            Bola.LoadContent(Content);
            Checkpoint.LoadContent(Content);
            Ovnis.LoadContent(Content);
            Pulpito.LoadContent(Content);
            Carteles.LoadContent(Content);
            PowerUps.LoadContent(Content);
            Rampas.LoadContent(Content);
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){

            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.Violet.ToVector3());
            foreach (var mesh in PisoModel.Meshes)
            {
                
                for(int i=0; i < PisoWorlds.Length; i++){
                    Matrix _pisoWorld = PisoWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _pisoWorld);
                    mesh.Draw();
                }
                
            }
            Effect.Parameters["DiffuseColor"].SetValue(Color.Purple.ToVector3());
            foreach (var mesh in ParedModel.Meshes)
            {
                
                for(int i=0; i < ParedWorlds.Length; i++){
                    Matrix _pisoWorld = ParedWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _pisoWorld);
                    mesh.Draw();
                }
                
            }

            Bola.Draw(gameTime, view, projection);
            Checkpoint.Draw(gameTime, view, projection);
            Ovnis.Draw(gameTime, view, projection);
            Pulpito.Draw(gameTime, view, projection);
            Carteles.Draw(gameTime, view, projection);
            PowerUps.Draw(gameTime, view, projection);
            Rampas.Draw(gameTime, view, projection);

        }

    }
}