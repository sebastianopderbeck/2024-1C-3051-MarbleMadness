﻿using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP
{

    public class PowerUpsStar
    {

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model PowerUpsModel { get; set; }
        public Matrix[] PowerUpsWorlds { get; set; }

        public Effect Effect { get; set; }

        public PowerUpsStar()
        {
            PowerUpsWorlds = new Matrix[] { };
        }

        public void agregarPowerUp(Vector3 Position)
        {
            Matrix escala = Matrix.CreateScale(30f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoPulpito = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            PowerUpsWorlds = PowerUpsWorlds.Concat(nuevoPulpito).ToArray();
        }

        public void LoadContent(ContentManager Content)
        {
            PowerUpsModel = Content.Load<Model>(ContentFolder3D + "shared/PowerUpStar");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in PowerUpsModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.Green.ToVector3());
            foreach (var mesh in PowerUpsModel.Meshes)
            {

                for (int i = 0; i < PowerUpsWorlds.Length; i++)
                {
                    Matrix _cartelWorld = PowerUpsWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _cartelWorld);
                    mesh.Draw();
                }

            }
        }
    }
}
