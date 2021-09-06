using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using wastelands.src.entities;
using System.Collections.Generic;
using wastelands.src;

namespace wastelands
{
    public class Wastelands : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static List<Entity> entities = new List<Entity>();
        private Entity[] entityArray; 

        public Wastelands()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.ToggleFullScreen();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            entityArray = entities.ToArray(); //Lists are much slower
            entities.Clear();
            
            for(int i = 0; i < entityArray.Length; i++)
            {
                entityArray[i].Init();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < entityArray.Length; i++)
            {
                entityArray[i].Load();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < entityArray.Length; i++)
            {
                entityArray[i].Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);

            for (int i = 0; i < entityArray.Length; i++)
            {
                Vector2 relPos = entityArray[i].position - Vars.camera.position + (Vars.screenSize / 2f) * (2f - Vars.camera.zoom);

                if(relPos.X >= 0 && relPos.Y >= 0 && relPos.X <= Vars.screenSize.X && relPos.Y <= Vars.screenSize.Y)
                {
                    entityArray[i].Draw(spriteBatch);
                }
            }
        }
    }
}
