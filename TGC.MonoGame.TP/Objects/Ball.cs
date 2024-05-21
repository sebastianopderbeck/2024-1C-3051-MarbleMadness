using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TGC.MonoGame.TP.Collisions;
using TGC.MonoGame.niveles;

namespace TGC.MonoGame.TP{
    
    public class Ball{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model BallModel{get; set;}
        public Matrix BallWorld{get; set;}
        
        public Effect Effect { get; set; }

        private Matrix BallScale { get; set; }
        public Vector3 BallPosition { get; set; }
        public Matrix BallRotation { get; set; }
        private BallMaterial BallType { get; set; }
        private Vector3 BallFrontDirection { get; set; }
        private const float BallRotatingVelocity = 0.06f;
        private const float BallSideSpeed = 30f;
        private const float BallJumpSpeed = 30f;
        private const float EPSILON = 0.00001f;
        public int index { get; set; }

        public Vector3 BallVelocity { get; set; }
        public Vector3 BallAcceleration { get; set; }
        public Vector3 DireccionBola { get; set; }
        private const float Gravity = 40f;

        public FollowCamera BallCamera { get; set; }

        // A boolean indicating if the Robot is on the ground
        private bool OnGround { get; set; }

        // Colliders
        // Collider de la esfera
        private BoundingCylinder BallSphere { get; set; }
        public BoundingBox[] Colliders { get; set; }

        private static bool Compare(float a, float b)
        {
            return MathF.Abs(a - b) < float.Epsilon;
        }

        enum BallMaterial
        {
            Standard,
            Stone,
            Gum
        }

        public Ball(Vector3 posicionInicial){

            // Al comienzo no debe tocar el piso
            OnGround = false;

            // Posicion e Inicializacion de la matriz Ball
            BallPosition = posicionInicial;
            BallScale = Matrix.CreateScale(0.024f);

            // Creacion del colisionador BallSphere
            BallSphere = new BoundingCylinder(posicionInicial, 3f, 3f);
            BallRotation = Matrix.Identity;
            BallFrontDirection = Vector3.Backward;

            // Atributos para el movimiento de la Bola
            BallAcceleration = Vector3.Down * Gravity;
            BallVelocity = Vector3.Zero;
            
        }

        public void LoadContent(ContentManager Content){

            // Se cargan modelos y efectos de la bola
            BallModel = Content.Load<Model>(ContentFolder3D + "sphere/sphere");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");
            
            foreach (var mesh in BallModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }

            //BallSphere = BoundingVolumesExtensions.CreateSphereFrom(BallModel);
            var extents = BoundingVolumesExtensions.CreateAABBFrom(BallModel);
            var height = extents.Max.Y - extents.Min.Y;

            BallPosition += height * 0.5f * Vector3.Up * BallScale.M22;

            BallSphere.Center = BallPosition;
            BallWorld = BallScale * Matrix.CreateTranslation(BallPosition);

        }

        public void Update(GameTime gameTime){

            var deltaTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);

            var keyboardState = Keyboard.GetState();

            /*if (keyboardState.IsKeyDown(Keys.D))
            {
                BallRotation *= Matrix.CreateRotationY(-BallRotatingVelocity);
                BallFrontDirection = Vector3.Transform(Vector3.Backward, BallRotation);
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                BallRotation *= Matrix.CreateRotationY(BallRotatingVelocity);
                BallFrontDirection = Vector3.Transform(Vector3.Backward, BallRotation);
            }*/

            if (keyboardState.IsKeyDown(Keys.Space) && OnGround)
                BallVelocity += Vector3.Up * BallJumpSpeed;

            if (keyboardState.IsKeyDown(Keys.W))
                BallVelocity += BallFrontDirection * BallSideSpeed;
            else if (keyboardState.IsKeyDown(Keys.S))
                BallVelocity -= BallFrontDirection * BallSideSpeed;

            //BallAcceleration += Gravity;
            BallVelocity += BallAcceleration * deltaTime;
            var scaledVelocity = BallVelocity * deltaTime;
            SolveVerticalMovement(scaledVelocity);
            scaledVelocity = new Vector3(scaledVelocity.X, 0f, scaledVelocity.Z);
            SolveHorizontalMovementSliding(scaledVelocity);

            /*if (keyboardState.IsKeyDown(Keys.A))
                 BallAcceleration += Vector3.Left;
             if (keyboardState.IsKeyDown(Keys.D))
                 BallAcceleration += Vector3.Right;
             if (keyboardState.IsKeyDown(Keys.W))
                 BallAcceleration += Vector3.Forward;
             if (keyboardState.IsKeyDown(Keys.S))
                 BallAcceleration += Vector3.Backward;
             if (keyboardState.IsKeyDown(Keys.Space) && (OnGround == true)) {
                 BallVelocity += Vector3.Up * 300f;
                 OnGround = false;
             }

             //BallAcceleration += friccion;
             BallAcceleration += Gravity;
             BallVelocity += BallAcceleration * deltaTime;
             var scaledVelocity = BallVelocity * deltaTime;

             SolveVerticalMovement(scaledVelocity);

             var minimumFloor = MathHelper.Max(0f, BallPosition.Y);
             BallPosition = new Vector3(BallPosition.X, minimumFloor, BallPosition.Z);

             if (Compare(BallPosition.Y, 0.0f) && (OnGround == false)) {
                 BallVelocity = new Vector3(BallVelocity.X, 0f, BallVelocity.Z);
                 OnGround = true;
             }

             var minimumFloor = MathHelper.Max(0f, BallPosition.Y);
             BallPosition = new Vector3(BallPosition.X, minimumFloor, BallPosition.Z);

             if (Compare(BallPosition.Y, 0.0f) && (OnGround == false))
             {
                 BallVelocity = new Vector3(VelocidadBola.X, 0f, VelocidadBola.Z);
                 OnGround = true;

             BallPosition = BallSphere.Center;
             BallVelocity = new Vector3(0f, BallVelocity.Y, 0f);*/

            BallPosition = BallSphere.Center;
            BallVelocity = new Vector3(0f, BallVelocity.Y, 0f);
            BallWorld = BallScale * BallRotation * Matrix.CreateTranslation(BallPosition);

        }
        public void Respawn(/*Checkpoint checkpoint*/)
        {
            if (BallPosition.Y < -100f)
            {
                BallPosition = new(0f, 10f, 0f);
            }
        }

        public void PickUp(/*Texture newTexture, */)
        {

        }

        private void SolveVerticalMovement(Vector3 scaledVelocity)
        {
            // If the Robot has vertical velocity
            if (scaledVelocity.Y == 0f)
                return;

            // Start by moving the Cylinder
            BallSphere.Center += Vector3.Up * scaledVelocity.Y;
            // Set the OnGround flag on false, update it later if we find a collision
            OnGround = false;


            // Collision detection
            var collided = false;
            var foundIndex = -1;
            for (var index = 0; index < Colliders.Length; index++)
            {
                if (!BallSphere.Intersects(Colliders[index]).Equals(BoxCylinderIntersection.Intersecting))
                    continue;
                
                // If we collided with something, set our velocity in Y to zero to reset acceleration
                BallVelocity = new Vector3(BallVelocity.X, 0f, BallVelocity.Z);

                // Set our index and collision flag to true
                // The index is to tell which collider the Robot intersects with
                collided = true;
                foundIndex = index;
                break;
            }


            // We correct based on differences in Y until we don't collide anymore
            // Not usual to iterate here more than once, but could happen
            while (collided)
            {
                var collider = Colliders[foundIndex];
                var colliderY = BoundingVolumesExtensions.GetCenter(collider).Y;
                var sphereY = BallSphere.Center.Y;
                var extents = BoundingVolumesExtensions.GetExtents(collider);

                float penetration;
                // If we are on top of the collider, push up
                // Also, set the OnGround flag to true
                if (sphereY > colliderY)
                {
                    penetration = colliderY + extents.Y - sphereY + BallSphere.HalfHeight;
                    OnGround = true;
                }

                // If we are on bottom of the collider, push down
                else
                    penetration = -sphereY - BallSphere.HalfHeight + colliderY - extents.Y;

                // Move our Cylinder so we are not colliding anymore
                BallSphere.Center += Vector3.Up * penetration;
                collided = false;

                // Check for collisions again
                for (var index = 0; index < Colliders.Length; index++)
                {
                    if (!BallSphere.Intersects(Colliders[index]).Equals(BoxCylinderIntersection.Intersecting))
                        continue;

                    // Iterate until we don't collide with anything anymore
                    collided = true;
                    foundIndex = index;
                    break;
                }
            }
            
        }
        private void SolveHorizontalMovementSliding(Vector3 scaledVelocity)
        {
            // Has horizontal movement?
            if (Vector3.Dot(scaledVelocity, new Vector3(1f, 0f, 1f)) == 0f)
                return;
            
            // Start by moving the Cylinder horizontally
            BallSphere.Center += new Vector3(scaledVelocity.X, 0f, scaledVelocity.Z);

            // Check intersection for every collider
            for (var index = 0; index < Colliders.Length; index++)
            {
                if (!BallSphere.Intersects(Colliders[index]).Equals(BoxCylinderIntersection.Intersecting))
                    continue;

                // Get the intersected collider and its center
                var collider = Colliders[index];
                var colliderCenter = BoundingVolumesExtensions.GetCenter(collider);

                bool stepClimbed = SolveStepCollision(collider, index);

                // If the Robot collided with a step and climbed it, stop here
                // Else go on
                if (stepClimbed)
                    return;

                // Get the cylinder center at the same Y-level as the box
                var sameLevelCenter = BallSphere.Center;
                sameLevelCenter.Y = colliderCenter.Y;

                // Find the closest horizontal point from the box
                var closestPoint = BoundingVolumesExtensions.ClosestPoint(collider, sameLevelCenter);

                // Calculate our normal vector from the "Same Level Center" of the cylinder to the closest point
                // This happens in a 2D fashion as we are on the same Y-Plane
                var normalVector = sameLevelCenter - closestPoint;
                var normalVectorLength = normalVector.Length();

                // Our penetration is the difference between the radius of the Cylinder and the Normal Vector
                // For precission problems, we push the cylinder with a small increment to prevent re-colliding into the geometry
                var penetration = BallSphere.HalfHeight - normalVector.Length() + EPSILON;

                // Push the center out of the box
                // Normalize our Normal Vector using its length first
                BallSphere.Center += (normalVector / normalVectorLength * penetration);
            }
            
        }

        private bool SolveStepCollision(BoundingBox collider, int colliderIndex)
        {
            // Get the collider properties to check if it's a step
            // Also, to calculate penetration
            var extents = BoundingVolumesExtensions.GetExtents(collider);
            var colliderCenter = BoundingVolumesExtensions.GetCenter(collider);

            // Is this collider a step?
            // If not, exit
            if (extents.Y >= 6f)
                return false;

            // Is the base of the cylinder close to the step top?
            // If not, exit
            var distanceToTop = MathF.Abs((BallSphere.Center.Y - BallSphere.HalfHeight) - (colliderCenter.Y + extents.Y));
            if (distanceToTop >= 12f)
                return false;

            // We want to climb the step
            // It is climbable if we can reposition our cylinder in a way that
            // it doesn't collide with anything else
            var pastPosition = BallSphere.Center;
            BallSphere.Center += Vector3.Up * distanceToTop;
            for (int index = 0; index < Colliders.Length; index++)
                if (index != colliderIndex && BallSphere.Intersects(Colliders[index]).Equals(BoxCylinderIntersection.Intersecting))
                {
                    // We found a case in which the cylinder
                    // intersects with other colliders, so the climb is not possible
                    BallSphere.Center = pastPosition;
                    return false;
                }

            // If we got here the climb was possible
            // (And the Robot position was already updated)
            return true;
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

            //Gizmos.DrawCylinder(BallSphere.Transform, Color.Yellow);

        }
    }
}