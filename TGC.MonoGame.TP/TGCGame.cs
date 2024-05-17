using System;
using System.Net.Http.Headers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP;
using TGC.MonoGame.niveles;
using TGC.MonoGame.TP.Collisions;
using MonoGamers.Camera;

namespace TGC.MonoGame.TP
{
    public class TGCGame : Game
    {
        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderSounds = "Sounds/";
        public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public const string ContentFolderTextures = "Textures/";

        public TGCGame()
        {
            // Maneja la configuracion y la administracion del dispositivo grafico.
            Graphics = new GraphicsDeviceManager(this);
            
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
            
            // Para que el juego sea pantalla completa se puede usar Graphics IsFullScreen.

            // Carpeta raiz donde va a estar toda la Media.
            Content.RootDirectory = "Content";
            // Hace que el mouse sea visible.
            IsMouseVisible = true;
        }

        private GraphicsDeviceManager Graphics { get; }
        private SpriteBatch SpriteBatch { get; set; }
        private Model Model { get; set; }
        private Effect Effect { get; set; }
        private float Rotation { get; set; }
        private Matrix World { get; set; }
        private Matrix View { get; set; }
        private Matrix Projection { get; set; }
        
        private Ball Ball{ get; set; }
        private BoundingCylinder SphereCollider { get; set; }
        private Nivel1 Nivel1 { get; set; }
        private NivelParte2 NivelSegundaParte { get; set; }
        private Nivel2 Nivel2 { get; set; }
        private Nivel3 Nivel3 { get; set; }
        private NivelFinal NivelFinal { get; set; }
        public FollowCamera Camera { get; set; }
        private Skybox Skybox { get; set; }
        float distance = 20;
        Vector3 cameraPosition;


        public BoundingBox[] Colliders { get; set; }

        public int indexPiso { get; set; }

        private bool ShowGizmos { get; set; } = true;

        //private Checkpoint Checkpoint{ get; set; }

        protected override void Initialize()
        {
            // La logica de inicializacion que no depende del contenido se recomienda poner en este metodo.

            // Apago el backface culling.
            // Esto se hace por un problema en el diseno del modelo del logo de la materia.
            // Una vez que empiecen su juego, esto no es mas necesario y lo pueden sacar.
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;
            // Seria hasta aca.


            // Configuramos nuestras matrices de la escena.
            World = Matrix.Identity;
            
            View = Matrix.CreateLookAt(Vector3.UnitZ * 150, Vector3.Zero, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1, 250);

            Camera = new FollowCamera(GraphicsDevice, new Vector3(0, 5, 15), Vector3.Zero, Vector3.Up);
            Nivel1 = new Nivel1();
            Ball = new Ball(new (0f,30f,0f));

            Ball.Colliders = Nivel1.getCollaiders();
            Ball.index = Nivel1.getCollaidersIndex();
            Ball.BallCamera = Camera;

            base.Initialize();
        }

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo, despues de Initialize.
        ///     Escribir aqui el codigo de inicializacion: cargar modelos, texturas, estructuras de optimizacion, el procesamiento
        ///     que podemos pre calcular para nuestro juego.
        /// </summary>
        protected override void LoadContent()
        {
            // Aca es donde deberiamos cargar todos los contenido necesarios antes de iniciar el juego.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Cargo el modelo del logo.
            Model = Content.Load<Model>(ContentFolder3D + "tgc-logo/tgc-logo");

            // Cargo un efecto basico propio declarado en el Content pipeline.
            // En el juego no pueden usar BasicEffect de MG, deben usar siempre efectos propios.
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            
            // Asigno el efecto que cargue a cada parte del mesh.
            // Un modelo puede tener mas de 1 mesh internamente.
            foreach (var mesh in Model.Meshes)
            {
                // Un mesh puede tener mas de 1 mesh part (cada 1 puede tener su propio efecto).
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }


            Nivel1.LoadContent(Content);
            //NivelSegundaParte.LoadContent(Content);

            //Nivel2.LoadContent(Content);

            //Nivel3.LoadContent(Content);

            //NivelFinal.LoadContent(Content);
            

            Ball.LoadContent(Content);
            Skybox = new Skybox(ContentFolderTextures + "Skybox/SkyBox", Content);

            

            base.LoadContent();
        }


        protected override void Update(GameTime gameTime)
        {

            // Aca deberiamos poner toda la logica de actualizacion del juego.
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            // Capturar Input teclado
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                //Salgo del juego.
                Exit();
            }
            
            // Basado en el tiempo que paso se va generando una rotacion.
            Rotation += Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);

            World = Matrix.CreateScale(0.3f) * Matrix.CreateRotationY(Rotation);
            
            Ball.Update(gameTime);

            Camera.Update(Ball.BallPosition);
            Nivel1.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        ///     Se llama cada vez que hay que refrescar la pantalla.
        ///     Escribir aqui el codigo referido al renderizado.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            // Aca deberiamos poner toda la logia de renderizado del juego.
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
            /*Effect.Parameters["View"].SetValue(Camera.ViewMatrix); //Cambio View por Eso
            Effect.Parameters["Projection"].SetValue(Projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.Blue.ToVector3());

            foreach (var mesh in Model.Meshes)
            {
                Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * World);
                mesh.Draw();
            }*/
            
            Ball.Draw(gameTime, Camera.ViewMatrix, Camera.ProjectionMatrix);
            Nivel1.Draw(gameTime, Camera.ViewMatrix, Camera.ProjectionMatrix);
            //NivelSegundaParte.Draw(gameTime, Camera.ViewMatrix, Camera.ProjectionMatrix);

            //Nivel2.Draw(gameTime, Camera.ViewMatrix, Camera.ProjectionMatrix);

            //Nivel3.Draw(gameTime, Camera.ViewMatrix, Camera.ProjectionMatrix);

            var originalRasterizerState = GraphicsDevice.RasterizerState;
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            Graphics.GraphicsDevice.RasterizerState = rasterizerState;

            Skybox.Draw(Camera.ViewMatrix, Camera.ProjectionMatrix, Ball.BallPosition);

            GraphicsDevice.RasterizerState = originalRasterizerState;

        }

        protected override void UnloadContent()
        {
            Content.Unload();
            base.UnloadContent();
        }
    }
}