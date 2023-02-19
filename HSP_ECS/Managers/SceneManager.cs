using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using HSP_ECS.Helpers;
using HSP_ECS.Systems;
using System.Collections.Generic;
using HSP_ECS.Components;

namespace HSP_ECS
{
    public class SceneManager : Game
    {
        //Graphics Variables
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int mScreenWidth = 1080;
        private int mScreenHeight = 720;

        //Resource Loader
        public ResourceLoaderHelper ResourceLoader;

        //Delegates
        public delegate void RenderDelegate();
        public delegate void UpdateDelegate(GameTime pGameTime);
        public RenderDelegate Render;
        public UpdateDelegate Updater;
        public Scene CurrentScene;

        //Managers
        SystemManager mSystemManager;

        //Systems
        SystemRender mSystemRender;

        public SceneManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = mScreenHeight;
            _graphics.PreferredBackBufferWidth = mScreenWidth;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            ResourceLoader = new ResourceLoaderHelper(this);
            mSystemManager = new SystemManager();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentScene = new GameScene(this);

            Updater = CurrentScene.Update;
            Render = CurrentScene.Draw;

            CreateSystems();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Updater(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Texture2D t = ResourceLoader.LoadTexture("corn2");
            //List<Entity> ent = new List<Entity>();
            //Entity e = new Entity("john");
            //e.AddComponent(new ComponentPosition(new Vector2(100, 100)));
            //e.AddComponent(new ComponentSprite(t));
            //ent.Add(e);

            SpriteBatch.Begin();

            GraphicsDevice.Clear(Color.CornflowerBlue);
            //mSystemManager.Action(ent);
            Render();

            SpriteBatch.End();
        }

        public SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }
        }

        public void CreateSystems()
        {
            mSystemRender = new SystemRender(SpriteBatch);
            mSystemManager.AddSystem(mSystemRender);
        }
    }
}