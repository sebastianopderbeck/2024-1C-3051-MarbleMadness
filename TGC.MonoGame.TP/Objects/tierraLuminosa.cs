using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP{

    public class TierraLuminosa{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model TierraLuminosaModel{get; set;}
        public Matrix[] TierraLuminosaWorlds{get; set;}
        public Effect Effect { get; set; }
        //private float ModelRotation { get; set; }
        private Vector3[] Posicion { get; set; }

        private float _rotation;
        //private Vector3 _rotationAxis;
        private float _rotationSpeed;


        public TierraLuminosa(){            
            TierraLuminosaWorlds = new Matrix[]{};
            Posicion = new Vector3[]{};
            _rotation = 0f;
            _rotationSpeed = 0.01f;
        }

        public void agregarTierraLuminosa(Vector3 Position){
            Matrix escala = Matrix.CreateScale(0.8f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevaTierraLuminosa = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            TierraLuminosaWorlds = TierraLuminosaWorlds.Concat(nuevaTierraLuminosa).ToArray();
            var nuevaPosicion = new Vector3[]{
                Position,
            };
            Posicion = Posicion.Concat(nuevaPosicion).ToArray();
        }

        public void LoadContent(ContentManager Content){
            TierraLuminosaModel = Content.Load<Model>(ContentFolder3D + "shared/TierraLuminosa");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in TierraLuminosaModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
        }

        public void Update(GameTime gameTime, int index){

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _rotation += _rotationSpeed * deltaTime;
            TierraLuminosaWorlds[index] = Matrix.CreateRotationY(_rotation) * Matrix.CreateTranslation(Posicion[index]);
            
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.Pink.ToVector3());
            foreach (var mesh in TierraLuminosaModel.Meshes)
            {
                
                for(int i=0; i < TierraLuminosaWorlds.Length; i++){
                    Matrix _tierraWorld = TierraLuminosaWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _tierraWorld);
                    mesh.Draw();
                }
                
            }
            
        }
    }
}