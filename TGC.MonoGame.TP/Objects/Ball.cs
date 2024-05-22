using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TGC.MonoGame.Samples.Collisions;

namespace TGC.MonoGame.TP{
    
    public class Ball{

        enum BallMaterial {
            Standard,
            Stone,
            Gum
        }

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model BallModel { get; set; }
        public Matrix BallWorld { get; set;}
        public Effect Effect { get; set; }
        private BallMaterial BallType { get; set; }
        public float BallScale { get; set; } = 0.024f;
        public Matrix RotacionBola { get; set; } = Matrix.Identity;
        //New
        public Vector3 BallPosition { get; set; }
        private Vector3 BallFrontDirection { get; set; } = Vector3.Forward;
        private Vector3 BallLateralDirection { get; set; } = Vector3.Left;
        private Vector3 BallVelocity { get; set; } = Vector3.Zero;
        private float Force { get; set; } = 2f;
        private float BallMass { get; set; } = 1.0f;
        private float Friction { get; set; } = 2f;
        private float BallJumpSpeed { get; set; } = 100f;
        public float Gravity = 5f;
        public BoundingSphere ballSphere;
        public Vector3 SpawnPoint { get; set; }


        private bool OnGround { get; set; }
        private static bool Compare(float a, float b)
        {
            return MathF.Abs(a - b) < float.Epsilon;
        }

        public Ball(Vector3 posicionInicial){
            SpawnPoint = posicionInicial;
            BallPosition = posicionInicial;
            BallWorld = Matrix.Identity * Matrix.CreateScale(BallScale) * Matrix.CreateTranslation(BallPosition);
            OnGround = false;
        }

        public void LoadContent(ContentManager Content){
            BallModel = Content.Load<Model>(ContentFolder3D + "sphere/sphere");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");
            foreach (var mesh in BallModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }

            //ballSphere = BoundingVolumesExtensions.CreateSphereFrom(BallModel);
            //ballSphere.Center = BallPosition;
            //ballSphere.Radius = 1f;
        }

        public void Update(GameTime gameTime){
            Movement(gameTime);
            Respawn();
            
        }

        public void Movement(GameTime gameTime){
            
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            Vector3 aceleracion = Vector3.Down * Gravity;
            //Vector3 aceleracion = Vector3.Zero;

            if (keyboardState.IsKeyDown(Keys.W))
                aceleracion += BallFrontDirection * (Force / BallMass);
            else if (keyboardState.IsKeyDown(Keys.S))
                aceleracion += -BallFrontDirection * (Force / BallMass);
            if (keyboardState.IsKeyDown(Keys.A))
                aceleracion += BallLateralDirection * (Force / BallMass);
            else if (keyboardState.IsKeyDown(Keys.D))
                aceleracion += -BallLateralDirection * (Force / BallMass);

            if (keyboardState.IsKeyDown(Keys.Space) && OnGround){
                BallVelocity += Vector3.Up * BallJumpSpeed;
                OnGround = false;
            }
                

            BallVelocity += aceleracion - (BallVelocity * Friction) * deltaTime;

            //BallPosition += BallVelocity * deltaTime;


            //BallWorld = Matrix.CreateScale(BallScale) * Matrix.CreateTranslation(BallPosition);
        }

        public void UpdatePosition(GameTime gameTime){
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            BallPosition += BallVelocity * deltaTime;
            BallWorld = Matrix.CreateScale(BallScale) * Matrix.CreateTranslation(BallPosition);
            BallBounding();
        }

        public void SolveGravity(BoundingBox[] colliders){
            for (var index = 0; index < colliders.Length; index++)
            {
                if (!ballSphere.Intersects(colliders[index]))
                    continue;

                BallVelocity = new Vector3(BallVelocity.X, 0f, BallVelocity.Z);
                OnGround = true;

                break;
            }
        }

        public void BallBounding(){
            for (int meshIndex1 = 0; meshIndex1 < BallModel.Meshes.Count; meshIndex1++)
            {
                ballSphere = BallModel.Meshes[meshIndex1].BoundingSphere;
                ballSphere = ballSphere.Transform(BallWorld);
            }
        }
        /*public void SolveGravity(){
            
            var _floor = 1.0f;
            if (BallPosition.Y == _floor)
                return;
            OnGround = false;

            
            
            var minimumFloor = MathHelper.Max(_floor, BallPosition.Y);
            BallPosition = new Vector3(BallPosition.X, minimumFloor, BallPosition.Z);

            if (Compare(BallPosition.Y, _floor) && (OnGround == false)) {
                BallVelocity = new Vector3(BallVelocity.X, 0f, BallVelocity.Z);
                OnGround = true;
            }
        }*/

        public void Respawn(/*Checkpoint checkpoint*/){
            if (BallPosition.Y < -100f){
                BallPosition = SpawnPoint;
                BallVelocity = new (0f, 0f, 0f);
            }
        }

        public void PickUp(/*Texture newTexture, */){

        }

        public void SetSpawnPoint(Vector3 newSpawnPoint){
            SpawnPoint = newSpawnPoint;
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            Effect.Parameters["View"].SetValue(view); //Cambio View por Eso
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.Blue.ToVector3());
            foreach (var mesh in BallModel.Meshes)
            {
                Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * BallWorld);
                mesh.Draw();
            }
        }

        public bool Collided(BoundingBox boundedObject){
            if(ballSphere.Intersects(boundedObject))
                return true;
            return false;
        }
    }
}