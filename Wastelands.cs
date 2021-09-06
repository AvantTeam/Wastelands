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

            this.Window.Position = new Point(0, 0);
            this.Window.IsBorderless = true;
            graphics.PreferredBackBufferWidth = (int)Vars.screenSize.X;
            graphics.PreferredBackBufferHeight = (int)Vars.screenSize.Y;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            new EntityTest(new Vector2(300f, 100f));

            new EntityTest(new Vector2(500f, 100f));

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

            Vars.mousePosition = Mouse.GetState().Position.ToVector2();
            Vars.relativeMousePosition = Vars.mousePosition - Vars.camera.position;

            foreach (Entity entity in entities)
            {
                entity.Update(gameTime.TotalGameTime.Ticks / 60f);
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (Entity entity in entities)
            {
                Vector2 relPos = entity.position - Vars.camera.position;

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