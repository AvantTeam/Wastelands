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
using System.IO;

namespace wastelands
{
    public class Wastelands : Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch uiBatch;

        public Texture2D mouseSprite;
        public GameTime _lastUpdatedGameTime;

        public Wastelands()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public Texture2D TakeScreenshot()
        {
            int w, h;
            w = GraphicsDevice.PresentationParameters.BackBufferWidth;
            h = GraphicsDevice.PresentationParameters.BackBufferHeight;
            RenderTarget2D screenshot;
            screenshot = new RenderTarget2D(GraphicsDevice, w, h, false, SurfaceFormat.Color, DepthFormat.None);
            GraphicsDevice.SetRenderTarget(screenshot);
            Draw(_lastUpdatedGameTime != null ? _lastUpdatedGameTime : new GameTime());
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Present();

            return screenshot;
        }

        protected override void LoadContent()
        {
            Log.Clear();
            uiBatch = new SpriteBatch(GraphicsDevice);

            GraphicsDevice.Clear(Color.Black);

            Tex.Load(Content);
            Sounds.Load(Content);
            TileLoader.LoadAll(Content, graphics.GraphicsDevice);
            MapTileLoader.LoadAll(Content);
            Localizer.LoadLocals(Content);
            Effects.LoadEffects(Content);

            mouseSprite = Content.Load<Texture2D>("sprites/UI/cursor");

            foreach(GameMode mode in GameModes.gameModes.Values)
            {
                mode.LoadContent(Content, GraphicsDevice);
            }

            Log.Write("Content Loaded.");
        }

        protected override void Initialize()
        {
            Window.Position = new Point(0, 0);
            Window.IsBorderless = true;

            IsMouseVisible = false;

            graphics.IsFullScreen = true;
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

            Vars.kbCurrentState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Vars.kbCurrentState.IsKeyDown(Keys.Escape))
            {
                Exit();
                Vars.saveManager.Save();
                Log.Write("Game Terminated.");
            }

            if (Vars.kbPreviousState.IsKeyUp(Keys.P) && Vars.kbCurrentState.IsKeyDown(Keys.P))
            {
                using (Stream stream = File.Create(Path.Combine(Vars.gamePath, "screenshot.png")))
                {
                    using (Texture2D screenshot = TakeScreenshot())
                    {
                        screenshot.SaveAsPng(stream, screenshot.Width, screenshot.Height);
                    }
                }
            }

            Vars.mousePosition = Mouse.GetState().Position.ToVector2();
            Vars.relativeMousePosition = Vars.mousePosition - Vars.camera.position;

            GameManager.gameMode.Update(gameTime);

            Vars.camera.Update();

            Vars.kbPreviousState = Vars.kbCurrentState;
        }

        protected override void Draw(GameTime gameTime)
        {
            uiBatch.Begin(samplerState: SamplerState.PointClamp);

            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);

            GameManager.gameMode.Draw(gameTime, GraphicsDevice);

            if(Vars.IsMouseInsideWindow()) Draww.DrawSpriteRaw(uiBatch, mouseSprite, Mouse.GetState().Position.ToVector2(), 1, Color.White);
            uiBatch.End();
        }
    }
}