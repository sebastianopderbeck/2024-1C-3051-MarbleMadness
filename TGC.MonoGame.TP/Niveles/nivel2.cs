using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP;
using TGC.MonoGame.TP.Objects;

namespace TGC.MonoGame.niveles {

    public class Nivel2 {

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model PisoModel { get; set; }
        public Matrix[] PisoWorlds { get; set; }
        public Model ParedModel { get; set; }
        public Matrix[] ParedWorlds { get; set; }
        //public Ball Bola { get; set; }
        public Ovni Ovni { get; set; }
        public PowerUps PowerUps { get; set; }
        public Checkpoint Checkpoint { get; set; }
        public Rampa Rampa { get; set; }
        public Cartel Cartel { get; set; }
        public Effect Effect { get; set; }

        public const float DistanceBetweenFloor = 12.33f;
        public const float DistanceBetweenWall = 18f;
        public Matrix escala = Matrix.CreateScale(0.03f);
        public Vector3 arriba = new Vector3(0f, 3.6f, 0f);
        public Vector3 alturaPisoPared = new(0f, 3.6f, 0f);
        public Vector3 alturaEscalera = new Vector3(0f, 6f, 0f);
        public const float distanciaEscaleras = 3f;

        public Nivel2()
        {

            //Bola = new Ball(new(0f, 40f, 0f));
            Rampa = new Rampa();
            Checkpoint = new Checkpoint();
            PowerUps = new PowerUps();
            Ovni = new Ovni();
            Cartel = new Cartel();
            Initialize();
        }

        private void Initialize() {


            PisoWorlds = new Matrix[] {
                //piso inicio con tres enemigos
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 11 + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 12 + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 13 + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 14 + alturaEscalera * 3),

                //pisos que vienen despues de la rampa1
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 20 + alturaEscalera * 6),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 21 + alturaEscalera * 6),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + alturaEscalera * 6),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * DistanceBetweenFloor + alturaEscalera * 6),
                
                //pisos que vienen despues de la rampa2
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 4 * DistanceBetweenFloor + alturaEscalera * 9),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 5 * DistanceBetweenFloor + alturaEscalera * 10),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 6 * DistanceBetweenFloor + alturaEscalera * 11),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 7 * DistanceBetweenFloor + alturaEscalera * 11),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 8 * DistanceBetweenFloor + alturaEscalera * 11),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 9 * DistanceBetweenFloor + alturaEscalera * 11),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 10 * DistanceBetweenFloor + alturaEscalera * 11),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 11 * DistanceBetweenFloor + alturaEscalera * 10),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 12 * DistanceBetweenFloor + alturaEscalera * 9),

                //base final del nivel (faltan los demas pisos de los costados)
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 13 * DistanceBetweenFloor + alturaEscalera * 9),
                //escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 9 + Vector3.Backward * DistanceBetweenFloor + alturaEscalera * 3),
                //escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 9 + Vector3.Forward * DistanceBetweenFloor + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 14 * DistanceBetweenFloor + alturaEscalera * 9),
                //escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 10 + Vector3.Backward * DistanceBetweenFloor + alturaEscalera * 3),
                //escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 10 + Vector3.Forward * DistanceBetweenFloor + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 15 * DistanceBetweenFloor + alturaEscalera * 9),
                //escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 11 + Vector3.Backward * DistanceBetweenFloor + alturaEscalera * 3),
                //escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 11 + Vector3.Forward * DistanceBetweenFloor + alturaEscalera * 3),
            };

            ParedWorlds = new Matrix[] {               
                                         
            };

            Rampa.AgregarRampa(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 15 + alturaEscalera * 3); //falta rotarla
            Rampa.AgregarRampa(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * DistanceBetweenFloor + alturaEscalera * 6); //falta rotarla y acomodarla
            
            Checkpoint.AgregoCheckpoint(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 15 * DistanceBetweenFloor + alturaEscalera * 9);
            
            PowerUps.agregarPowerUp(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 14 + alturaEscalera * 3);
            PowerUps.agregarPowerUp(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + alturaEscalera * 6);

            Ovni.agregarOvni(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 12 + alturaEscalera * 3);
            Ovni.agregarOvni(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 7 * DistanceBetweenFloor + alturaEscalera * 11);
            Ovni.agregarOvni(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 8 * DistanceBetweenFloor + alturaEscalera * 11);
            Ovni.agregarOvni(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 9 * DistanceBetweenFloor + alturaEscalera * 11);
            Ovni.agregarOvni(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 22 + Vector3.Backward * 13 * DistanceBetweenFloor + alturaEscalera * 9);

            Cartel.AgregarCartel(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 14 + Vector3.Forward * DistanceBetweenFloor + alturaEscalera * 5);
            Cartel.AgregarCartel(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 23 + alturaEscalera * 8);

        }

        public void LoadContent(ContentManager Content) {
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
            //Bola.LoadContent(Content);
            Rampa.LoadContent(Content);
            Checkpoint.LoadContent(Content);
            PowerUps.LoadContent(Content);
            Ovni.LoadContent(Content);
            Cartel.LoadContent(Content);

        }


        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {

            //PisoModel.Draw(PisoWorlds, view, projection);
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

            //Bola.Draw(gameTime, view, projection);
            Rampa.Draw(gameTime, view, projection);
            Checkpoint.Draw(gameTime, view, projection);
            PowerUps.Draw(gameTime, view, projection);
            Ovni.Draw(gameTime, view, projection);
            Cartel.Draw(gameTime, view, projection);
        }


    }
}