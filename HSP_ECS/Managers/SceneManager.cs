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
        public SystemManager mSystemManager;
        public EntityManager mEntityManager;
        public InputManager mInputManager;
        public CollisionManager mCollisionManager;

        //Systems
        public SystemRender mSystemRender;
        public SystemCamera mSystemCamera;
        public SystemPhysics mSystemPhysics;
        public SystemCollisionAABBAABB mSystemCollisionAABBAABB;

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
            mCollisionManager = new CollisionManager(mEntityManager);

            CameraHelper.CameraInit(mScreenWidth);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            mResourceLoader.LoadAllTextures();
            CurrentScene = new GameScene(this, "Maps/Text/map2.txt");

            Updater = CurrentScene.Update;
            Render = CurrentScene.Draw;

            CreateSystems();

            // temp
            mSystemCollisionAABBAABB.GetPhysicsObjects(mEntityManager.Entities);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Updater(gameTime);
            mCollisionManager.ProcessCollisions();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
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
            mSystemCollisionAABBAABB = new SystemCollisionAABBAABB(mCollisionManager);
            mSystemManager.AddSystem(mSystemCollisionAABBAABB);
        }
    }
}