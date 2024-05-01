using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TGC.MonoGame.TP;
using TGC.MonoGame.TP.Objects;

namespace TGC.MonoGame.niveles
{

    public class Nivel4 {
        public const string ContentFolder3D = "Models/";

        public Matrix[] InicioWorlds { get; set; }

        public Model InicioModel { get; set; }

        public Matrix[] Camino1Worlds { get; set; }

        public Ceiling Camino1 { get; set; }
        public Model Camino1Model { get; set; }

        public Matrix[] FinalWorlds { get; set; }

        public Ball Bola { get; set; }
        public Checkpoint Checkpoint { get; set; }
        public Ovni Ovnis { get; set; }
        public Pulpito Pulpito { get; set; }
        public Cartel Carteles { get; set; }
        public PowerUps PowerUps { get; set; }

        public Ceiling Ceiling {  get; set; }

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


        public Nivel4()
        {

            Ovnis = new Ovni();
            Bola = new Ball(new(0f, 20f, 0f));
            Pulpito = new Pulpito();
            Checkpoint = new Checkpoint();
            Carteles = new Cartel();
            PowerUps = new PowerUps();
            Ceiling = new Ceiling();
            Camino1 = new Ceiling();

            Initialize();
        }

        private void Initialize()
        {
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

            Ovnis.agregarOvni(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 2 + alturaEscalera * 3);
            Ovnis.agregarOvni(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 7 + alturaEscalera * 3);
            Pulpito.agregarPulpito((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor);
            Checkpoint.AgregoCheckpoint(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 10 + alturaEscalera * 3);
            Carteles.AgregarCartel(Vector3.Forward * DistanceBetweenFloor * 3 + Vector3.Left * DistanceBetweenFloor / 2 + arriba * 2);
            Carteles.AgregarCartel(Vector3.Forward * (DistanceBetweenFloor * 6.25f + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 4 + alturaEscalera * 3 + arriba * 2);
            PowerUps.agregarPowerUp(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 3 + alturaEscalera * 3);
        }

        public void LoadContent(ContentManager Content)
        {
            InicioModel = Content.Load<Model>(ContentFolder3D + "shared/Ceiling");
            Camino1Model = Content.Load<Model>(ContentFolder3D + "shared/Ceiling");
            //ParedModel = Content.Load<Model>(ContentFolder3D + "shared/Wall");

            Bola.LoadContent(Content);
            Checkpoint.LoadContent(Content);
            Ovnis.LoadContent(Content);
            Pulpito.LoadContent(Content);
            Carteles.LoadContent(Content);
            PowerUps.LoadContent(Content);
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {

            //PisoModel.Draw(PisoWorlds, view, projection);
            for (int i = 0; i < InicioWorlds.Length; i++)
            {
                Matrix _incioWorld = InicioWorlds[i];
                InicioModel.Draw(_incioWorld, view, projection);
            }

            for (int i = 0; i < Camino1Worlds.Length; i++)
            {
                Matrix _incioWorld = Camino1Worlds[i];
                Camino1Model.Draw(_incioWorld, view, projection);
            }

        }

    }
}
