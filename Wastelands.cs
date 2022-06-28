using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using wastelands.src;
using wastelands.src.entities;
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

        public static List<Entity> entities = new List<Entity>();
        public Tilemap tilemap = new Tilemap();

        public NinePatchRenderable test;

        public Texture2D mouseSprite;

        public Wastelands()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // YES i have to load everything in Initialize because in LoadContent it breaks yay
            Tex.Load(Content);
            Sounds.Load(Content);
            TileLoader.LoadAll(Content, graphics.GraphicsDevice);
            MapTileLoader.LoadAll(Content);
            Localizer.LoadLocals(Content);

            Window.Position = new Point(0, 0);
            Window.IsBorderless = false;

            IsMouseVisible = false;

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = (int)Vars.screenSize.X;
            graphics.PreferredBackBufferHeight = (int)Vars.screenSize.Y;
            graphics.ApplyChanges();

            Vars.camera.position = new Vector2(-(Vars.screenSize.X / 2), -(Vars.screenSize.Y / 2));
            Vars.saveManager.Load();
            foreach (Entity entity in entities)
            {
                entity.Init();
            }

            tilemap.AddChunk(MapGen.PlaceRooms(MapGen.Generate(150)), Vector2.Zero);
            base.Initialize();

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Sounds.wawtealdbad);

            Log.Write("Game Initialized.");
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            test = Tex.Get("hud1").createRenderable(400, 64);

            mouseSprite = Content.Load<Texture2D>("sprites/UI/cursor");

            foreach (Entity entity in entities)
            {
                entity.Load(Content);
            }

            Log.Clear();
            Log.Write("Content Loaded.");
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

            foreach (Entity entity in entities)
            {
                entity.Update(gameTime);
            }

            Vars.camera.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (Entity entity in entities)
            {
                Vector2 relPos = entity.position - Vars.camera.position;
                
                entity.Draw(spriteBatch, relPos);
            }

            tilemap.Draw();

            test.Render(0, 0);
            base.Draw(gameTime);

            Draww.DrawSprite(spriteBatch, mouseSprite, Mouse.GetState().Position.ToVector2());
            spriteBatch.End();
        }
    }
}