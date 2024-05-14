using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.Versioning;
using DungeonCrawler.Code.GameplayMVC;
using DungeonCrawler.Code.ScreenMVC;
using DungeonCrawler.Code.ShopMVC;

namespace DungeonCrawler.Code
{
    public class GameRoot : Game
    {
        public event EventHandler<GameplayControlsEventArgs> GameplayButtonPressed = delegate { };
        public event EventHandler<ScreenControlsEventArgs> ScreenButtonPressed = delegate { };
        public event EventHandler<ShopControlsEventArgs> ShopButtonPressed = delegate { };
        public event EventHandler GameStateChanged = delegate { };


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private IInterfaceView _interfaceView;

        private IGameplayModel _gameplayModel;
        private IGameplayController _gameplayController;
		private IGameplayVIew _gameplayView;

		private IScreenModel _screenModel;
        private IScreenController _screenController;
		private IScreenView _screenView;

        private IShopModel _shopModel;
        private IShopController _shopController;
        private IShopView _shopView;

		private GameState currentGameState;

        private KeyboardState keyboardState;

        public GameRoot()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            GameplayButtonPressed += HandleGameplayInput;
            ScreenButtonPressed += HandleScreenInput;
            ShopButtonPressed += HandleShopInput;

            _interfaceView = new InterfaceView();

			_gameplayModel = new GameplayModel();
			_gameplayModel.Updated += GameplayModelViewUpdate;
			_gameplayModel.GameStateChanged += GameStateUpdate;

			_gameplayController = new GameplayController();
            _gameplayController.PlayerMoved += GameplayControllerModelUpdate;
            _gameplayController.PressedEsc += SetGameStateOnPause;
            _gameplayController.ItemUsed += UseItem;

            _gameplayView = new GameplayView();


            _screenModel = new ScreenModel();
            _screenModel.Updated += ScreenModelViewUpdate;
            _screenModel.ButtonPressed += ScreenModelGameStateUpdate;

            _screenController = new ScreenController();
            _screenController.ButtonPressed += ScreenControllerModelUpdate;

            _screenView = new ScreenView();


            _shopModel = new ShopModel();
            _shopModel.Updated += ShopModelViewUpdate;

            _shopController = new ShopController();
            _shopController.ItemBought += BuyItem;
            _shopController.PressedEsc += LeaveShop;

            _shopView = new ShopView();

            currentGameState = GameState.MainMenu;
        }


        protected override void Initialize()
        {
            _interfaceView.Initialize(this);

            _gameplayModel.Initialize();

            _gameplayView.Initialize(this);
            _gameplayView.UpdateParameters(_gameplayModel.Map.GetMap(), _gameplayModel.Player, _gameplayModel.ViableFields);

            _screenModel.Initialize();

            _screenView.Initialize(this);
            _screenView.UpdateParameters(_screenModel.CurrentButtons, _screenModel.CurrentButtons[_screenModel.CurrentButtonIndex], currentGameState);

            _shopModel.Initialize(_gameplayModel.Player);

            _shopView.Initialize(this);

            IsMouseVisible = true;

            _graphics.PreferredBackBufferHeight = Constants.height;
            _graphics.PreferredBackBufferWidth = Constants.width;
            _graphics.ApplyChanges();

            base.Initialize();
            //TestClass.EnemiesGenerator();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.GetPressedKeyCount() > 0)
            {
                if (currentGameState == GameState.PlayingLevel)
                {
                    GameplayButtonPressed.Invoke(
                        this, new GameplayControlsEventArgs 
                        { 
                            Map = _gameplayModel.Map, 
                            Player = _gameplayModel.Player, 
                            KeyboardState = keyboardState 
                        });
                }

                else if (currentGameState == GameState.InShop)
                {
                    ShopButtonPressed.Invoke(this, new ShopControlsEventArgs 
                    { 
                        KeyboardState = keyboardState, 
                        ShopModel = _shopModel 
                    });
                }

                else
                {
                    ScreenButtonPressed.Invoke(this, new ScreenControlsEventArgs
                    {
                        KeyboardState = keyboardState,
                        ScreenModel = _screenModel
                    });
                }
            }

            _gameplayController.UpdateState(keyboardState);
            _screenController.UpdateState(keyboardState);
            _shopController.UpdateState(keyboardState); //как-то надо это изменить

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            if (currentGameState == GameState.PlayingLevel)
            {
                _gameplayView.Draw(this, _spriteBatch);
                _interfaceView.Draw(this, _spriteBatch);
            }

            else if (currentGameState == GameState.InShop)
            {
                _shopView.Draw(this, _spriteBatch);
				_interfaceView.Draw(this, _spriteBatch);
			}

            else
            {
                _screenView.Draw(this, _spriteBatch);
            }
           
            _spriteBatch.End();

            base.Draw(gameTime);
        }

		#region GameplayMVC
		public void GameStateUpdate(object sender, GameplayEventArgs e)
		{
			if (e.GameState == GameState.InShop)
			{
				currentGameState = e.GameState;
				_shopModel.UpdateShop();
                _interfaceView.UpdateParameters(_gameplayModel.Player, _gameplayModel.Map.Level);
			}
			else
			{
				currentGameState = e.GameState;
				_screenModel.UpdateScreenModel(currentGameState);
			}
		}

        public void UseItem(object sender, EventArgs e)
        {
            _gameplayModel.UseItem();
        }

		public void GameplayControllerModelUpdate(object sender, EventArgs e)
        {
            _gameplayModel.Update();
        }

        public void GameplayModelViewUpdate(object sender, GameplayEventArgs e)
        {
            _gameplayView.UpdateParameters(e.Map, e.Player, e.ViableMapFields);
            _interfaceView.UpdateParameters(e.Player, e.Level);
        }
		#endregion

		#region ScreenMVC
		public void ScreenModelViewUpdate(object sender, ScreenEventArgs e)
        {
            _screenView.UpdateParameters(e.CurrentButtons, e.ChoosedButton, e.GameState);
        }

        public void ScreenControllerModelUpdate(object sender, EventArgs e)
        {
            _screenModel.PressButton();
        }

        public void SetGameStateOnPause(object sender, EventArgs e)
        {
            currentGameState = GameState.Pause;
            _screenModel.UpdateScreenModel(currentGameState);
        }

        public void ScreenModelGameStateUpdate(object sender, ScreenEventArgs e)
        {
            if (e.PressedButton == ButtonType.exit)
            {
                if (currentGameState == GameState.MainMenu)
                {
                    Exit();
                }
                else
                {
                    currentGameState = GameState.MainMenu;
					_screenModel.UpdateScreenModel(currentGameState);
				}
            }
            else
            {
                switch(e.PressedButton)
                {
                    case ButtonType.start:
                        currentGameState = GameState.PlayingLevel;
                        _gameplayModel.Restart();
                        break;
					case ButtonType.restart:
                        currentGameState = GameState.PlayingLevel;
						_gameplayModel.Restart();
						break;
					case ButtonType.controls:
                        currentGameState = GameState.Controls;
						_screenModel.UpdateScreenModel(currentGameState);
						break;
                    case ButtonType.resume:
                        currentGameState = GameState.PlayingLevel;
						_screenModel.UpdateScreenModel(currentGameState);
						break;
                }
            }
        }
		#endregion

		#region ShopMVC

        public void ShopModelViewUpdate(object sender, ShopEventArgs e)
        {
            _shopView.UpdateParameters(e.Items, e.CurrentItemIndex, e.ItemsPrice);
            _interfaceView.UpdatePlayer(e.Player);
        }

        public void BuyItem(object sender, EventArgs e)
        {
            _shopModel.BuyItem();
        }

        public void LeaveShop(object sender, EventArgs e)
        {
            currentGameState = GameState.PlayingLevel;
            _gameplayView.UpdateParameters(_gameplayModel.Map.GetMap(), _gameplayModel.Player, _gameplayModel.ViableFields);
            //_interfaceUpdate()
        }

		#endregion

		public void HandleGameplayInput(object sender, GameplayControlsEventArgs e)
        {
            _gameplayController.HandleInput(e.Map, e.Player, e.KeyboardState);
        }

        public void HandleScreenInput(object sender, ScreenControlsEventArgs e)
        {
            _screenController.HandleInput(e.KeyboardState, e.ScreenModel);
        }

        public void HandleShopInput(object sender, ShopControlsEventArgs e)
        {
            _shopController.HandleInput(e.KeyboardState, e.ShopModel);
        }
    }
}
