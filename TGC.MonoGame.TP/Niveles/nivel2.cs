using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TGC.MonoGame.TP;
using TGC.MonoGame.TP.Objects;

namespace TGC.MonoGame.niveles
{

    public class Nivel2 {
        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Matrix[] InicioWorlds { get; set; }
        public Model InicioModel { get; set; }
        public Matrix[] Camino1Worlds { get; set; }
        public Model Camino1Model { get; set; }
        public Matrix[] FinalWorlds { get; set; }
        public Checkpoint Checkpoint { get; set; }
        public Ovni Ovni { get; set; }
        public Pulpito Pulpito { get; set; }
        public Cartel Carteles { get; set; }
        public PowerUpsStar PowerUpsStar { get; set; }
        public Effect Effect { get; set; }
        public Portal Portal { get; set; }
        public TierraLuminosa TierraLuminosa { get; set; }

    


        public const float DistanceBetweenFloor = 12.33f;
        public const float DistanceBetweenWall = 18f;
        public Matrix escala = Matrix.CreateScale(0.03f);
        public Vector3 arriba = new Vector3(0f, 3.6f, 0f);
        public Vector3 alturaPisoPared = new(0f, 3.6f, 0f);
        public Vector3 alturaEscalera = new Vector3(0f, 6f, 0f);
        public const float distanciaEscaleras = 2f;

        public Vector3 abajo = new Vector3(0f, 0f, -3f);

        // ____ World matrices ____
        //matrices de las plataformas fijas (pisos)
        //matrices tipo lista para que tengan los pisos flotantes


        public Nivel2()
        {
            Ovni = new Ovni();
            Pulpito = new Pulpito();
            PowerUpsStar = new PowerUpsStar();
            Portal = new Portal();
            Checkpoint = new Checkpoint();
            TierraLuminosa = new TierraLuminosa();


            Initialize();
        }

        private void Initialize()
        {
            //base de inicio
            InicioWorlds = new Matrix[]{
                escala * Matrix.Identity,
                escala * Matrix.CreateTranslation(Vector3.Right * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenFloor ),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Right) * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Backward + Vector3.Right) * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Backward + Vector3.Left) * DistanceBetweenFloor),


            };

            //camino largo
            Camino1Worlds = new Matrix[]
            {
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 2),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 4),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 5),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 6),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 7),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 8),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 9),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 10),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 11),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 12),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 13),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 14),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 15),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 16),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 17),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 18),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 19),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 20),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 21),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 22),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 23),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 24),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor * 25),
            };

            //el pulpito te felicita por llegar tan lejos y te motiva
            Pulpito.agregarPulpito((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor + Vector3.Up * 3);


            Ovni.agregarOvni(Vector3.Forward * DistanceBetweenFloor * 3);
            Ovni.agregarOvni(Vector3.Forward * DistanceBetweenFloor * 5);
            Ovni.agregarOvni(Vector3.Forward * DistanceBetweenFloor * 7);
            Ovni.agregarOvni(Vector3.Forward * DistanceBetweenFloor * 9);
            Ovni.agregarOvni(Vector3.Forward * DistanceBetweenFloor * 11);
            Ovni.agregarOvni(Vector3.Forward * DistanceBetweenFloor * 15);
            Ovni.agregarOvni(Vector3.Forward * DistanceBetweenFloor * 17);
            Ovni.agregarOvni(Vector3.Forward * DistanceBetweenFloor * 19);
            Ovni.agregarOvni(Vector3.Forward * DistanceBetweenFloor * 21);
            Ovni.agregarOvni(Vector3.Forward * DistanceBetweenFloor * 23);

            PowerUpsStar.agregarPowerUp(Vector3.Forward * DistanceBetweenFloor * 13 + arriba); //este impulsa a la pelota para que puedas pasar los ultimos ovnis que son mas rapidos

            Portal.AgregarPortal(Vector3.Forward * DistanceBetweenFloor * 25);
            Checkpoint.AgregoCheckpoint(Vector3.Forward * DistanceBetweenFloor * 25);

            TierraLuminosa.agregarTierraLuminosa(Vector3.Forward * DistanceBetweenFloor * 3 + Vector3.Left * DistanceBetweenFloor * 2f + alturaEscalera);
            TierraLuminosa.agregarTierraLuminosa(Vector3.Forward * DistanceBetweenFloor * 7 + Vector3.Right * DistanceBetweenFloor * 2f + alturaEscalera);
            TierraLuminosa.agregarTierraLuminosa(Vector3.Forward * DistanceBetweenFloor * 11 + Vector3.Left * DistanceBetweenFloor * 2f + alturaEscalera);
            TierraLuminosa.agregarTierraLuminosa(Vector3.Forward * DistanceBetweenFloor * 15 + Vector3.Right * DistanceBetweenFloor * 2f + alturaEscalera);
            TierraLuminosa.agregarTierraLuminosa(Vector3.Forward * DistanceBetweenFloor * 19 + Vector3.Left * DistanceBetweenFloor * 2f + alturaEscalera);
            TierraLuminosa.agregarTierraLuminosa(Vector3.Forward * DistanceBetweenFloor * 23 + Vector3.Right * DistanceBetweenFloor * 2f + alturaEscalera);


        }

        public void LoadContent(ContentManager Content)
        {
            InicioModel = Content.Load<Model>(ContentFolder3D + "shared/Ceiling");
            Camino1Model = Content.Load<Model>(ContentFolder3D + "shared/Ceiling");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in InicioModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
            foreach (var mesh in Camino1Model.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }

            
            Ovni.LoadContent(Content);
            PowerUpsStar.LoadContent(Content);
            Pulpito.LoadContent(Content);
            Checkpoint.LoadContent(Content);
            Portal.LoadContent(Content);
            TierraLuminosa.LoadContent(Content);
            
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {

            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.Gray.ToVector3());
            foreach (var mesh in InicioModel.Meshes)
            {
                
                for(int i=0; i < InicioWorlds.Length; i++){
                    Matrix _inicioWorld = InicioWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _inicioWorld);
                    mesh.Draw();
                }
                
            }
            Effect.Parameters["DiffuseColor"].SetValue(Color.LightGray.ToVector3());
            foreach (var mesh in Camino1Model.Meshes)
            {
                
                for(int i=0; i < Camino1Worlds.Length; i++){
                    Matrix _camino1World = Camino1Worlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _camino1World);
                    mesh.Draw();
                }
                
            }
            Ovni.Draw(gameTime, view, projection);
            Pulpito.Draw(gameTime, view, projection);
            PowerUpsStar.Draw(gameTime, view, projection);
            Checkpoint.Draw(gameTime, view, projection);
            Portal.Draw(gameTime, view, projection);
            TierraLuminosa.Draw(gameTime, view, projection);
        }

    }
}
