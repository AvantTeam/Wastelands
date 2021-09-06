using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;
using wastelands.src;
using wastelands.src.entities;

namespace wastelands
{
    public class Wastelands : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static List<Entity> entities = new List<Entity>();

        public Wastelands()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.ToggleFullScreen();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            new EntityTest();
            Console.WriteLine(entities.Count);
            entities.OrderBy(e => e.z);
            
            foreach(Entity entity in entities)
            {
                entity.Init();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (Entity entity in entities)
            {
                entity.Load(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Entity entity in entities)
            {
                entity.Update((float)gameTime.TotalGameTime.Ticks / 60f);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (Entity entity in entities)
            {
                Vector2 relPos = entity.position - Vars.camera.position + (Vars.screenSize / 2f) * (2f - Vars.camera.zoom);

                if (relPos.X + entity.size.X >= 0 && relPos.Y + entity.size.Y >= 0 && relPos.X <= Vars.screenSize.X && relPos.Y + entity.size.Y <= Vars.screenSize.Y)
                {
                    entity.Draw(spriteBatch, relPos);
                }
            }

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}