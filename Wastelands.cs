using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using wastelands.src;
using wastelands.src.graphics;
using wastelands.src.local;
using wastelands.src.map;
using wastelands.src.utils;
using wastelands.src.audio;
using wastelands.src.game;

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

        public bool IsMouseInsideWindow()
        {
            MouseState ms = Mouse.GetState();
            Point pos = new Point(ms.X, ms.Y);
            return GraphicsDevice.Viewport.Bounds.Contains(pos);
        }

        protected override void LoadContent()
        {
            Log.Clear();
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GraphicsDevice.Clear(Color.Black);

            Tex.Load(Content);
            Sounds.Load(Content);
            TileLoader.LoadAll(Content, graphics.GraphicsDevice);
            MapTileLoader.LoadAll(Content);
            Localizer.LoadLocals(Content);

            mouseSprite = Content.Load<Texture2D>("sprites/UI/cursor");

            foreach(GameMode mode in GameModes.gameModes.Values)
            {
                mode.LoadContent(Content);
            }

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

            GameManager.gameMode.Initialize();

            Log.Write("Game Initialized.");
        }
        
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
                Vars.saveManager.Save();
                Log.Write("Game Terminated.");
            }

            Vars.mousePosition = Mouse.GetState().Position.ToVector2();
            Vars.relativeMousePosition = Vars.mousePosition - Vars.camera.position;

            GameManager.gameMode.Update(gameTime);

            Vars.camera.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);

            GameManager.gameMode.Draw(gameTime, GraphicsDevice, spriteBatch);

            if(IsMouseInsideWindow()) Draww.DrawSpriteRaw(spriteBatch, mouseSprite, Mouse.GetState().Position.ToVector2(), 1, Color.White);
            spriteBatch.End();
        }
    }
}