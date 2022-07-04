using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using wastelands.src;
using wastelands.src.graphics;
using wastelands.src.local;
using wastelands.src.map;
using wastelands.src.utils;
using wastelands.src.audio;

namespace wastelands
{
    public class Wastelands : Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public Texture2D mouseSprite;

        public Wastelands()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GraphicsDevice.Clear(Color.Black);

            Tex.Load(Content);
            Sounds.Load(Content);
            TileLoader.LoadAll(Content, graphics.GraphicsDevice);
            MapTileLoader.LoadAll(Content);
            Localizer.LoadLocals(Content);

            mouseSprite = Content.Load<Texture2D>("sprites/UI/cursor");

            Log.Clear();
            Log.Write("Content Loaded.");
        }

        protected override void Initialize()
        {
            Window.Position = new Point(0, 0);
            Window.IsBorderless = false;

            IsMouseVisible = false;

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = (int)Vars.screenSize.X;
            graphics.PreferredBackBufferHeight = (int)Vars.screenSize.Y;
            graphics.ApplyChanges();

            Vars.camera.position = new Vector2(-(Vars.screenSize.X / 2), -(Vars.screenSize.Y / 2));
            Vars.saveManager.Load();

            base.Initialize();

            Log.Write("Game Initialized.");
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
                Vars.saveManager.Save();
                Log.Write("Game Terminated.");
            }

            Vars.mousePosition = Mouse.GetState().Position.ToVector2();
            Vars.relativeMousePosition = Vars.mousePosition - Vars.camera.position;

            Vars.camera.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);

            Draww.DrawSprite(spriteBatch, mouseSprite, Mouse.GetState().Position.ToVector2());
            spriteBatch.End();
        }
    }
}