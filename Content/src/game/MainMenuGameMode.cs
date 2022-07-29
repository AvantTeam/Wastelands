using System;
using Myra;
using Myra.Graphics2D.UI;
using wastelands.src.graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace wastelands.src.game
{
    public class MainMenuGameMode : GameMode
    {
        private Desktop desktop;
        private Texture2D bg;
        private SpriteBatch spriteBatch;

        public override void Initialize()
        {
            desktop = new Desktop();
        }

        public override void LoadContent(ContentManager content, GraphicsDevice device)
        {
            spriteBatch = new SpriteBatch(device);
            bg = content.Load<Texture2D>("sprites/UI/bg");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, GraphicsDevice device)
        {
            float scaling = Math.Max((float)device.Viewport.Width / (float)bg.Width, (float)device.Viewport.Height / (float)bg.Height);
            Vector2 position = new Vector2((device.Viewport.Width - bg.Width * scaling) / 2f, device.Viewport.Height - bg.Height * scaling);
            Draww.DrawSpriteRaw(spriteBatch, bg, position, scaling, Color.White);
        }
    }
}
