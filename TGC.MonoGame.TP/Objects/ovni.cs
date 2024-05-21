using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP
{

    public class Ovni
    {

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model OvniModel { get; set; }
        public Matrix[] OvniWorlds { get; set; }
        public Vector3[] Posicion { get; set; }
        public Vector3[] PosicionInicial { get; set; }
        public int[] MovementDirecion { get; set; } //0 es izquierda, 1 derecha
        public float OvniScale { get; set; } = 0.055f;
        public Effect Effect { get; set; }

        //falta hacer el constructor ovni
        public Ovni()
        {
            OvniWorlds = new Matrix[] { };
            Posicion = new Vector3[] { };
            PosicionInicial = new Vector3[] { };
            MovementDirecion = new int[] { };
        }

        public void agregarOvni(Vector3 Position, int Direction)
        {
            var nuevoOvni = new Matrix[]{
                Matrix.CreateScale(OvniScale) * Matrix.CreateTranslation(Position),
            };
            OvniWorlds = OvniWorlds.Concat(nuevoOvni).ToArray();
            var nuevaPosicion = new Vector3[]{
                Position,
            };
            Posicion = Posicion.Concat(nuevaPosicion).ToArray();
            PosicionInicial = PosicionInicial.Concat(nuevaPosicion).ToArray();
            var direccionInicial = new int[]{
                Direction,
            };
            MovementDirecion = MovementDirecion.Concat(direccionInicial).ToArray();
        }



        public void LoadContent(ContentManager Content)
        {
            OvniModel = Content.Load<Model>(ContentFolder3D + "shared/UFO/UFOEnemigo");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in OvniModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
        }

        public void Update(GameTime gameTime, int index)
        {
            Movement(gameTime, index);
        }

        public void Movement(GameTime gameTime, int index)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var MovementAxis = Vector3.Forward;
            var speed = 5f;

            float distance = Posicion[index].Z - PosicionInicial[index].Z;
            if (MovementDirecion[index] == 1)
            {
                Posicion[index] += MovementAxis * speed * deltaTime;
                if (distance < -7f)
                    MovementDirecion[index] = 0;
            }
            else if (MovementDirecion[index] == 0)
            {
                Posicion[index] += -MovementAxis * speed * deltaTime;
                if (distance > 7f)
                    MovementDirecion[index] = 1;
            }

            OvniWorlds[index] = Matrix.CreateScale(OvniScale) *
                                Matrix.CreateTranslation(Posicion[index]);
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.White.ToVector3());
            foreach (var mesh in OvniModel.Meshes)
            {

                for (int i = 0; i < OvniWorlds.Length; i++)
                {
                    Matrix _cartelWorld = OvniWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _cartelWorld);
                    mesh.Draw();
                }

            }
        }
    }
}