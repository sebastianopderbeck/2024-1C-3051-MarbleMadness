using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TGC.MonoGame.TP{
    
    public class Ball{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model BallModel{get; set;}
        public Matrix BallWorld{get; set;}
        public Effect Effect { get; set; }

        public float EscalaBola { get; set; } = 0.024f;
        public Vector3 PosicionBola { get; set; } = Vector3.Zero;
        public Matrix RotacionBola { get; set; } = Matrix.Identity;
        public Vector3 VelocidadBola { get; set; } = Vector3.Zero;
        public Vector3 AceleracionBola { get; set; } = Vector3.Zero;
        public Vector3 DireccionBola { get; set; }
        private Vector3 Gravedad = new (0, -9.81f, 0);

        private bool OnGround { get; set; }
        private static bool Compare(float a, float b)
        {
            return MathF.Abs(a - b) < float.Epsilon;
        }

        public Ball(Vector3 posicionInicial){
            BallWorld = Matrix.Identity * Matrix.CreateScale(EscalaBola) * Matrix.CreateTranslation(posicionInicial);
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
        }

        public void Update(GameTime gameTime){

            var deltaTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            var keyboardState = Keyboard.GetState();
            AceleracionBola = Vector3.Zero;
            Vector3 friccion = -VelocidadBola * 0.07f;


            if (keyboardState.IsKeyDown(Keys.A))
                AceleracionBola += Vector3.Left;
            if (keyboardState.IsKeyDown(Keys.D))
                AceleracionBola += Vector3.Right;
            if (keyboardState.IsKeyDown(Keys.W))
                AceleracionBola += Vector3.Forward;
            if (keyboardState.IsKeyDown(Keys.S))
                AceleracionBola += Vector3.Backward;
            if (keyboardState.IsKeyDown(Keys.Space) && (OnGround == true)) {
                VelocidadBola += Vector3.Up * 300f;
                OnGround = false;
            }
            
            AceleracionBola += friccion;
            AceleracionBola += Gravedad;
            VelocidadBola += AceleracionBola * 180f * deltaTime;
            PosicionBola += VelocidadBola * deltaTime;

            var minimumFloor = MathHelper.Max(0f, PosicionBola.Y);
            PosicionBola = new Vector3(PosicionBola.X, minimumFloor, PosicionBola.Z);

            if (Compare(PosicionBola.Y, 0.0f) && (OnGround == false)) {
                VelocidadBola = new Vector3(VelocidadBola.X, 0f, VelocidadBola.Z);
                OnGround = true;
            }
            

            BallWorld = Matrix.CreateScale(EscalaBola) * Matrix.CreateTranslation(PosicionBola);
            
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
    }
}