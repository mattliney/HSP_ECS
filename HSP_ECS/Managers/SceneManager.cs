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
        public SystemCollisionAABBPoint mSystemCollisionAABBPoint;

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

            CreateSystems();

            CurrentScene = new GameScene(this, "Maps/Text/map2.txt");

            Updater = CurrentScene.Update;
            Render = CurrentScene.Draw;
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

            Render();

            GraphicsDevice.Clear(Color.CornflowerBlue);
            mInputManager.ProcessInputs();
            mSystemManager.Action(mEntityManager.Entities, gameTime);

            SpriteBatch.End();
        }

        public void ChangeScene(SceneType pScene, string pFileName)
        {
            mEntityManager.ClearList();
            mInputManager.ClearPlayer();
            mSystemCollisionAABBPoint.ClearPhysicsObjects();
            mSystemCollisionAABBAABB.ClearPhysicsObjects();

            if (pScene == SceneType.TitleScene)
            {
                CurrentScene = new TitleScene(this);
                Updater = CurrentScene.Update;
                Render = CurrentScene.Draw;
            }
            else if (pScene == SceneType.MenuScene)
            {
                CurrentScene = new MenuScene(this);
                Updater = CurrentScene.Update;
                Render = CurrentScene.Draw;
            }
            else if (pScene == SceneType.EditorScene)
            {

            }
            else if (pScene == SceneType.GameScene)
            {
                CurrentScene = new GameScene(this, pFileName);
                Updater = CurrentScene.Update;
                Render = CurrentScene.Draw;
            }
        }

        public SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }
        }

        public int ScreenWidth
        {
            get { return mScreenWidth; }
        }

        public int ScreenHeight
        {
            get { return mScreenHeight; }
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

            mSystemCollisionAABBPoint = new SystemCollisionAABBPoint(mCollisionManager);
            mSystemManager.AddSystem(mSystemCollisionAABBPoint);
        }
    }
}