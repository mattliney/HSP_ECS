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
        private int mScreenWidth = 1088;
        private int mScreenHeight = 768;

        //Resource Loader
        public ResourceLoaderHelper mResourceLoader;

        //Delegates
        public delegate void RenderDelegate();
        public delegate void UpdateDelegate(GameTime pGameTime);
        public RenderDelegate Render;
        public UpdateDelegate Updater;
        public Scene CurrentScene;

        //Managers
        SystemManager mSystemManager;
        EntityManager mEntityManager;
        InputManager mInputManager;

        //Systems
        SystemRender mSystemRender;
        SystemCamera mSystemCamera;
        SystemPhysics mSystemPhysics;

        public SceneManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = mScreenHeight;
            _graphics.PreferredBackBufferWidth = mScreenWidth;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            mResourceLoader = new ResourceLoaderHelper(this);
            mSystemManager = new SystemManager();
            mEntityManager = new EntityManager();
            mInputManager = new InputManager(this);

            CameraHelper.CameraInit(mScreenWidth);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentScene = new GameScene(this);
            mResourceLoader.LoadTexture("grass");
            mResourceLoader.LoadTexture("grassbottom");
            mResourceLoader.LoadTexture("grasstop");
            MapLoaderHelper.LoadTextMap("Maps/Text/map2.txt", mEntityManager, mResourceLoader);

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
            mSystemManager.Action(mEntityManager.Entities, gameTime);
            mInputManager.ProcessInputs();

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
            mSystemCamera = new SystemCamera();
            mSystemManager.AddSystem(mSystemCamera);
            mSystemPhysics = new SystemPhysics();
            mSystemManager.AddSystem(mSystemPhysics);
        }
    }
}