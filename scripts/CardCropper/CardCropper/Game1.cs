using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System.IO;

namespace CardCropper
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {   
        public Game1() : base()
        {

        }
        protected override void Initialize()
        {
            base.Initialize();
            var scene = Scene.CreateWithDefaultRenderer(Color.CornflowerBlue);
            string[] files = Directory.GetFiles("Content");
            Texture2D[] textures = new Texture2D[files.Length];
            int content = "Content".Length;
            int height = 219;
            int width = 233;
            int heroHeight = 205;
            string resultDirectory = "results";
            Directory.CreateDirectory(resultDirectory);

            for (int x = 0; x < files.Length; x++)
            {
                int localHeight = files[x].Contains("hero") && !files[x].Contains("monu") ? heroHeight : height;
                Texture2D texture = Content.Load<Texture2D>(Path.GetFileNameWithoutExtension(files[x]));
                Color[] color = new Color[width * localHeight];
                texture.GetData(0, new Rectangle(82, 15, width, localHeight), color, 0, width * localHeight);
                textures[x] = new Texture2D(GraphicsDevice, width, localHeight);
                textures[x].SetData(color);
                Stream stream = File.Create(resultDirectory + "/" + Path.GetFileNameWithoutExtension(files[x]) + "_crop.png");
                textures[x].SaveAsJpeg(stream, width, localHeight);
                stream.Dispose();
            }

            

            var newText = new SpriteRenderer(textures[4]);
            //var newText2 = new SpriteRenderer(textures[1]);
            Entity first = scene.CreateEntity("first text");
            //Entity second = scene.CreateEntity("second text");
            first.AddComponent(newText);
            //second.AddComponent(newText2);
            first.Position = new Vector2(300, 300);
            //second.Position = new Vector2(600, 300);

            Scene = scene;
        }
    }
}
