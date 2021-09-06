using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using wastelands.src.entities;
using System.Collections.Generic;

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

            for (int i = 0; i < entityArray.Length; i++)
            {
                entityArray[i].Draw(spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
