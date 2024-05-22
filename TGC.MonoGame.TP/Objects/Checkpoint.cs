using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.Samples.Collisions;

namespace TGC.MonoGame.TP{

    public class Checkpoint{

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model CheckpointModel{get; set;}
        public Matrix[] CheckpointWorlds{get; set;}
        public Vector3[] Posicion { get; set; }
        private Vector3[] PosicionInicial { get; set; }
        public Effect Effect { get; set; }
        private float Scale { get; set; }
        private float _rotation = 0f;
        private const float _rotationSpeed = 0.5f;
        private int MovementDirecion = 1;
        public BoundingBox[] CheckpointBox { get; set; }

        public Checkpoint(){
            CheckpointWorlds = new Matrix[]{};
            Posicion = new Vector3[]{};
            PosicionInicial = new Vector3[]{};
            CheckpointBox = new BoundingBox[]{};
        }

        public void AgregoCheckpoint(Vector3 Position){
            Scale = 0.02f;
            var nuevoCheckpoint = new Matrix[]{
                Matrix.CreateScale(Scale) * Matrix.CreateTranslation(Position),
            };
            CheckpointWorlds = CheckpointWorlds.Concat(nuevoCheckpoint).ToArray();
            var nuevaPosicion = new Vector3[]{
                Position,
            };
            Posicion = Posicion.Concat(nuevaPosicion).ToArray();
            PosicionInicial = PosicionInicial.Concat(nuevaPosicion).ToArray();
            BoundingBox aux = new BoundingBox();
            aux = BoundingVolumesExtensions.FromMatrix(nuevoCheckpoint[0]);
            aux = BoundingVolumesExtensions.Scale(aux, 500f);

            var newBoundingBox = new BoundingBox[]{
                aux,
            };
            CheckpointBox = CheckpointBox.Concat(newBoundingBox).ToArray();
        }

        public void LoadContent(ContentManager Content){
            CheckpointModel = Content.Load<Model>(ContentFolder3D + "shared/Checkpoint");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in CheckpointModel.Meshes)
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

            var MovementAxis = Vector3.Up;
            var speed = 0.15f;

            float distance = Posicion[index].Y - PosicionInicial[index].Y;
            if(MovementDirecion == 1){
                Posicion[index] += MovementAxis * speed * deltaTime;
                if (distance > 0.5f)
                    MovementDirecion = 0;
            }
            else if(MovementDirecion == 0){
                Posicion[index] += -MovementAxis * speed * deltaTime;
                if (distance < 0.2f)
                    MovementDirecion = 1;
            }
            CheckpointWorlds[index] = Matrix.CreateScale(Scale) * 
                                      Matrix.CreateRotationY(_rotation) * 
                                      Matrix.CreateTranslation(Posicion[index]);
            
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.Green.ToVector3());
            foreach (var mesh in CheckpointModel.Meshes)
            {
                
                for(int i=0; i < CheckpointWorlds.Length; i++){
                    Matrix _checkpointWorld = CheckpointWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _checkpointWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}