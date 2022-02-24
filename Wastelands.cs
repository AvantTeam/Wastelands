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
        public MapGen mapGen = new MapGen();

        public NinePatchRenderable test;

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
            TileLoader.LoadAll(Content);
            MapTileLoader.LoadAll(Content);
            Localizer.LoadLocals(Content);

            Window.Position = new Point(0, 0);
            Window.IsBorderless = false;

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

            mapGen.Generate(150);
            base.Initialize();

            MediaPlayer.Play(Sounds.wawtealdbad);

            Log.Write("Game Initialized.");
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            test = Tex.Get("hud1").createRenderable(400, 64);

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
                entity.Update(gameTime.TotalGameTime.Ticks / 60f);
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

                if (relPos.X + entity.size.X >= 0 && relPos.Y + entity.size.Y >= 0 && relPos.X - entity.size.X <= Vars.screenSize.X && relPos.Y - entity.size.Y <= Vars.screenSize.Y)
                {
                    entity.Draw(spriteBatch, relPos);
                }
            }

            mapGen.tilemap.Draw();

            test.Render(0, 0);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}