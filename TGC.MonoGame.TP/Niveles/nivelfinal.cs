using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP;

namespace TGC.MonoGame.niveles{
    public class NivelFinal{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model PisoModel { get; set; }
        public Matrix[] PisoWorlds { get; set; }
        public Model ParedModel { get; set; }
        public Matrix[] ParedWorlds { get; set; }
        public Plank Planks {get; set; }
        public Ball Bola { get; set; }
        public Checkpoint Checkpoint { get; set; }
        public Ovni Ovnis { get; set; }
        public Pulpito Pulpito { get; set; }
        public Cartel Carteles { get; set; }
        public PowerUps PowerUps{ get; set; }
        public Effect Effect { get; set; }

        public const float DistanceBetweenFloor = 12.33f;
        public const float DistanceBetweenWall = 18f;
        public Matrix escala = Matrix.CreateScale(0.03f);
        public Vector3 arriba = new Vector3(0f, 3.6f, 0f);
        public Vector3 alturaPisoPared = new(0f, 3.6f, 0f);
        public Vector3 alturaEscalera = new Vector3(0f, 6f, 0f);
        public const float distanciaEscaleras = 3f;
        
        // ____ World matrices ____
        //matrices de las plataformas fijas (pisos)
        //matrices tipo lista para que tengan los pisos flotantes
         
        // Nivel final: inicio en plataforma, luego camino fino con saltos con zigzag, luego plataformas que se caen, despeus rampas bajando , poner engranajes en movimiento

        public NivelFinal() {

            Bola = new Ball(new (0f,4f,0f));
            Planks = new Plank();
            

            Initialize();
        }
        
        private void Initialize() {

            PisoWorlds = new Matrix[]{
                
                escala * Matrix.Identity,
            };

            ParedWorlds = new Matrix[]{
                
            };

            Planks.AgregarPlank(new Vector3(0f,0f,0f));
            Planks.AgregarPlank(new Vector3(0f,0f,0f));

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
            Planks.LoadContent(Content);
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
            Planks.Draw(gameTime, view, projection);

        }

    }
}