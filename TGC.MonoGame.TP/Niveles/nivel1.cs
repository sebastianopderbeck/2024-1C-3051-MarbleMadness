using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.Samples.Collisions;
using TGC.MonoGame.TP;

namespace TGC.MonoGame.niveles{
    public class Nivel1{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderTextures = "Textures/";
        public Model PisoModel { get; set; }
        public Matrix[] PisoWorlds { get; set; }
        public Model ParedModel { get; set; }
        public Matrix[] ParedWorlds { get; set; }
        public Checkpoint Checkpoint { get; set; }
        public NaveSW NaveSW { get; set; }
        public Asteroide Asteroide { get; set; }
        public Ovni Ovnis { get; set; }
        public Pulpito Pulpito { get; set; }
        public Cartel Carteles { get; set; }
        public PowerUpsRocket PowerUpsRocket{ get; set; }
        public TierraLuminosa Tierras { get; set; }
        public Effect Effect { get; set; }
        //ASD
        public Ball Ball { get; set; }
        public FollowCamera Camera { get; set; }
        public Skybox Skybox { get; set; }

       
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
        public BoundingBox[] Colliders { get; set; }
         
        public Nivel1(GraphicsDevice graphicsDevice) {

            Ovnis = new Ovni();
            Pulpito = new Pulpito();
            Checkpoint = new Checkpoint(); 
            NaveSW = new NaveSW();
            Carteles = new Cartel();
            PowerUpsRocket = new PowerUpsRocket();
            Tierras = new TierraLuminosa();
            Asteroide = new Asteroide();
            //NaveEspecial = new NaveEspecial();

            Ball = new Ball(new (0f,30f,0f));
            Camera = new FollowCamera(graphicsDevice, new Vector3(0, 5, 15), Vector3.Zero, Vector3.Up);
          
            Initialize();

        }
        
        private void Initialize() {
          
            PisoWorlds = new Matrix[]{
                // Matrix.Identity * Matrix.CreateScale(0.1f),
                // Matrix.CreateTranslation(411f, 50f, 411f)* Matrix.CreateScale(0.1f),
                
                //base de incio
                escala * Matrix.Identity,
                escala * Matrix.CreateTranslation(Vector3.Right * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Right) * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Backward + Vector3.Right) * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Backward + Vector3.Left) * DistanceBetweenFloor),


                //Piso inicial
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor) * 2),
        

                //escaleras
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 4),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 5 + distanciaEscaleras) + alturaEscalera),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 6 + distanciaEscaleras * 2) + alturaEscalera * 2),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + alturaEscalera * 3),


                //segundo camindo largo
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 2 + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 3 + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 6 + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 7 + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 8 + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 9 + alturaEscalera * 3),
                

                //base final
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 10 + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 10 + Vector3.Backward * DistanceBetweenFloor + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 10 + Vector3.Forward * DistanceBetweenFloor + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 11 + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 11 + Vector3.Backward * DistanceBetweenFloor + alturaEscalera * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 11 + Vector3.Forward * DistanceBetweenFloor + alturaEscalera * 3),
            };

            ParedWorlds = new Matrix[]{
                // Paredes base de inicio
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


                // Paredes bases final               
                // Pared atras
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 11 + Vector3.Forward * DistanceBetweenFloor + alturaEscalera * 3)
                    * Matrix.CreateTranslation(Vector3.Left * 6f  + alturaPisoPared),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 11 + Vector3.Backward * DistanceBetweenFloor + alturaEscalera * 3)
                    * Matrix.CreateTranslation(Vector3.Left * 6f  + alturaPisoPared),
                // Pared delante
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 9 + Vector3.Backward * DistanceBetweenFloor + alturaEscalera * 3)
                    * Matrix.CreateTranslation(Vector3.Left * 6f  + alturaPisoPared),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 9 + Vector3.Forward * DistanceBetweenFloor + alturaEscalera * 3)
                    * Matrix.CreateTranslation(Vector3.Left * 6f  + alturaPisoPared),
                //Pared Derecha
                Matrix.CreateRotationY(1.5708f)*
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 6.5f + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 10 + Vector3.Backward * DistanceBetweenFloor + alturaEscalera * 3)
                    * Matrix.CreateTranslation(Vector3.Left * 12f + alturaPisoPared),
                Matrix.CreateRotationY(1.5708f)*
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 6.5f + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 9 + Vector3.Backward * DistanceBetweenFloor + alturaEscalera * 3)
                    * Matrix.CreateTranslation(Vector3.Left * 12f + alturaPisoPared),
                //Pared Izquierda
                Matrix.CreateRotationY(1.5708f)*
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 6.5f + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 10 + Vector3.Forward* 2 * DistanceBetweenFloor + alturaEscalera * 3)
                    * Matrix.CreateTranslation(Vector3.Left * 12f + alturaPisoPared),
                Matrix.CreateRotationY(1.5708f)*
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor * 6.5f + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 9 + Vector3.Forward * 2 * DistanceBetweenFloor + alturaEscalera * 3)
                    * Matrix.CreateTranslation(Vector3.Left * 12f + alturaPisoPared), 
            
        };

            Colliders = new BoundingBox[PisoWorlds.Length];
            var _scale = new Vector3(500f, 150f, 500f);
            int index = 0;
            for (; index < PisoWorlds.Length; index++){
                Colliders[index] = BoundingVolumesExtensions.FromMatrix(PisoWorlds[index]);
                Colliders[index] = BoundingVolumesExtensions.Scale(Colliders[index], _scale);
                //Colliders[index] = new BoundingBox(new Vector3(-6.11f, -0.001f, -6.11f), new Vector3(6.11f, 0f, 6.11f));
            }
                

                       
            //Enemigos
            Ovnis.agregarOvni(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 2 + alturaEscalera * 3 + Vector3.Up * 2, 1);
            Ovnis.agregarOvni(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 7 + alturaEscalera * 3 + Vector3.Up * 2, 0);
            Pulpito.agregarPulpito((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor + Vector3.Up * 3);
            
            //Checkpoint
            Checkpoint.AgregoCheckpoint(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 10 + alturaEscalera * 3);

            //carteles
            Carteles.AgregarCartel(Vector3.Forward * DistanceBetweenFloor * 3 + Vector3.Left * DistanceBetweenFloor / 2 + arriba * 2);

            //no logre girar este cartel para que se vea de frente y no de costado
            Carteles.AgregarCartel(Vector3.Forward * (DistanceBetweenFloor * 6.25f + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 4 + alturaEscalera * 3 + arriba * 2);

            //PowerUp
            PowerUpsRocket.agregarPowerUp(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 3 + alturaEscalera * 3 + Vector3.Up * 3);

            //Objetos decorativos
            Tierras.agregarTierraLuminosa(new Vector3(-50f, 25f, -34f));
            Tierras.agregarTierraLuminosa(new Vector3(-50f, 35f, -120));
            Asteroide.AgregarAsteroide(new Vector3(25f, 35f, -90));




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
            Checkpoint.LoadContent(Content);
            NaveSW.LoadContent(Content);
            Ovnis.LoadContent(Content);
            Pulpito.LoadContent(Content);
            Carteles.LoadContent(Content);
            PowerUpsRocket.LoadContent(Content);
            Tierras.LoadContent(Content);
            Asteroide.LoadContent(Content);

            Ball.LoadContent(Content);
            Skybox = new Skybox(ContentFolderTextures + "Skybox/SkyBox", Content);
        }

        public void Update(GameTime gameTime){
            Tierras.Update(gameTime, 0);
            Tierras.Update(gameTime, 1);
            Checkpoint.Update(gameTime, 0);
            Ovnis.Update(gameTime, 0);
            Ovnis.Update(gameTime, 1);

            Ball.Movement(gameTime);
            Ball.SolveGravity(Colliders);
            Ball.UpdatePosition(gameTime);

            CheckPointCollision();
            Ball.Respawn();


            Camera.Update(Ball.BallPosition);


        }
        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice){
            var view = Camera.ViewMatrix;
            var projection = Camera.ProjectionMatrix;

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

            Checkpoint.Draw(gameTime, view, projection);
            NaveSW.Draw(gameTime, view, projection);
            Ovnis.Draw(gameTime, view, projection);
            Pulpito.Draw(gameTime, view, projection);
            Carteles.Draw(gameTime, view, projection);
            PowerUpsRocket.Draw(gameTime, view, projection);
            Tierras.Draw(gameTime, view, projection);
            Asteroide.Draw(gameTime, view, projection);
            //NaveEspecial.Draw(gameTime, view, projection);
            Ball.Draw(gameTime, view, projection);


            var originalRasterizerState = graphicsDevice.RasterizerState;
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            graphicsDevice.RasterizerState = rasterizerState;

            Skybox.Draw(Camera.ViewMatrix, Camera.ProjectionMatrix, Ball.BallPosition);

            graphicsDevice.RasterizerState = originalRasterizerState;


        }

        public void CheckPointCollision(){
            for(int index = 0; index < Checkpoint.CheckpointWorlds.Length; index++){
                if(Ball.Collided(Checkpoint.CheckpointBox[index]))
                    Ball.SetSpawnPoint(Checkpoint.Posicion[index]);
                    
            }
        }

    }
}