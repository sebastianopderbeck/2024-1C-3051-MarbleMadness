using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGamers.Camera
{
    internal class FreeCamera : Camera
    {
        private Matrix CameraRotation { get; set; }
        private const float CameraRotatingVelocity = 0.06f;
        private readonly bool lockMouse;

        private readonly Point screenCenter;
        private bool changed;

        private Vector2 pastMousePosition;
        private float pitch;

        private bool godMode;

        // Angles
        private float yaw = -90f;

        public FreeCamera(float aspectRatio, Vector3 position, Point screenCenter) : this(aspectRatio, position)
        {
            lockMouse = true;
            this.screenCenter = screenCenter;
        }

        public FreeCamera(float aspectRatio, Vector3 position) : base(aspectRatio)
        {
            Position = position;
            pastMousePosition = Mouse.GetState().Position.ToVector2();
            UpdateCameraVectors();
            CalculateView();
            CameraRotation = Matrix.Identity;
        }

        public float MovementSpeed { get; set; } = 100f;
        public float MouseSensitivity { get; set; } = 5f;

        private void CalculateView()
        {
            View = Matrix.CreateLookAt(Position, Position + FrontDirection, UpDirection);
        }

        /// <inheritdoc />
        public override void Update(GameTime gameTime)
        {
            var elapsedTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            changed = false;
            ProcessKeyboard(elapsedTime);
            ProcessMouseMovement(elapsedTime);

            if (changed)
                CalculateView();
        }

        private void ProcessKeyboard(float elapsedTime)
        {
            var keyboardState = Keyboard.GetState();

            var currentMovementSpeed = MovementSpeed;
            if (keyboardState.IsKeyDown(Keys.LeftShift))
                currentMovementSpeed *= 15f;

            if (keyboardState.IsKeyDown(Keys.G))
                if (!godMode) godMode = true;
                else godMode = false;

            if (godMode && keyboardState.IsKeyDown(Keys.Space))
                Position += Vector3.Up;

            if(godMode && keyboardState.IsKeyDown(Keys.LeftShift))
                Position += Vector3.Down;


            if (keyboardState.IsKeyDown(Keys.M))
            {
                CameraRotation *= Matrix.CreateRotationX(CameraRotatingVelocity);
                FrontDirection = Vector3.Transform(Vector3.Backward, CameraRotation);
                changed = true;
            }
            if (keyboardState.IsKeyDown(Keys.N))
            {
                CameraRotation *= Matrix.CreateRotationX(-CameraRotatingVelocity);
                FrontDirection = Vector3.Transform(Vector3.Backward, CameraRotation);
                changed = true;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                CameraRotation *= Matrix.CreateRotationY(CameraRotatingVelocity);
                FrontDirection = Vector3.Transform(Vector3.Backward, CameraRotation);
                changed = true;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                CameraRotation *= Matrix.CreateRotationY(-CameraRotatingVelocity);
                FrontDirection = Vector3.Transform(Vector3.Backward, CameraRotation);
                changed = true;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Position += FrontDirection * currentMovementSpeed * elapsedTime;
                changed = true;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Position += -FrontDirection * currentMovementSpeed * elapsedTime;
                changed = true;
            }
        }

        private void ProcessMouseMovement(float elapsedTime)
        {
            var mouseState = Mouse.GetState();

            if (mouseState.RightButton.Equals(ButtonState.Pressed))
            {
                var mouseDelta = mouseState.Position.ToVector2() - pastMousePosition;
                mouseDelta *= MouseSensitivity * elapsedTime;

                yaw -= mouseDelta.X;
                pitch += mouseDelta.Y;

                if (pitch > 89.0f)
                    pitch = 89.0f;
                if (pitch < -89.0f)
                    pitch = -89.0f;

                changed = true;
                UpdateCameraVectors();

                if (lockMouse)
                {
                    Mouse.SetPosition(screenCenter.X, screenCenter.Y);
                    Mouse.SetCursor(MouseCursor.Crosshair);
                }
                else
                {
                    Mouse.SetCursor(MouseCursor.Arrow);
                }
            }

            pastMousePosition = Mouse.GetState().Position.ToVector2();
        }

        private void UpdateCameraVectors()
        {
            // Calculate the new Front vector
            Vector3 tempFront;
            tempFront.X = MathF.Cos(MathHelper.ToRadians(yaw)) * MathF.Cos(MathHelper.ToRadians(pitch));
            tempFront.Y = MathF.Sin(MathHelper.ToRadians(pitch));
            tempFront.Z = MathF.Sin(MathHelper.ToRadians(yaw)) * MathF.Cos(MathHelper.ToRadians(pitch));

            FrontDirection = Vector3.Normalize(tempFront);

            // Also re-calculate the Right and Up vector
            // Normalize the vectors, because their length gets closer to 0 the more you look up or down which results in slower movement.
            RightDirection = Vector3.Normalize(Vector3.Cross(FrontDirection, Vector3.Up));
            UpDirection = Vector3.Normalize(Vector3.Cross(RightDirection, FrontDirection));
        }
    }
}