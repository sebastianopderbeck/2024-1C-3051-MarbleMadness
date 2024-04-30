using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TGC.MonoGame.TP.Objects
{
    public class Ceiling
    {

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public Model CeilingModel { get; set; }
        public Matrix[] CeilingWorlds { get; set; }
        public Effect Effect { get; set; }
        public Ceiling() {
            CeilingWorlds = new Matrix[] { };
        }

        public void AgregarCeiling(Vector3 Position) {
            Matrix escala = Matrix.CreateScale(0.03f);
            Vector3 arriba = new Vector3(0f, 50f, 0f);
            var nuevoCeiling = new Matrix[]{
                escala * Matrix.CreateTranslation(Position),
            };
            CeilingWorlds = CeilingWorlds.Concat(nuevoCeiling).ToArray();
        }

        public void LoadContent(ContentManager Content) {
            CeilingModel = Content.Load<Model>(ContentFolder3D + "shared/ceiling");
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            foreach (var mesh in CeilingModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection) {
            Effect.Parameters["View"].SetValue(view); //Cambio View por Eso
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.Green.ToVector3());
            foreach (var mesh in CeilingModel.Meshes)
            {
                
                for(int i=0; i < CeilingWorlds.Length; i++){
                    Matrix _ceilingWorld = CeilingWorlds[i];
                    Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * _ceilingWorld);
                    mesh.Draw();
                }
                
            }
        }
    }
}
