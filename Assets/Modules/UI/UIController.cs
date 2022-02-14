using System;
using Modules.Game;
using Modules.Inputs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Modules.UI
{
    public class UIController : MonoBehaviour
    {
        [Inject] private IGameModel gameModel;
        [Inject] private IInputManager inputManager;

        // Game over
        [SerializeField] private UIDocument gameOverUIDocument;
        private Button restartButton;
        private Label scoreLabel;
        
        // Main menu
        [SerializeField] private UIDocument mainMenuUIDocument;
        private Button startButton;
        private Button mainMenuExitButton;
        
        // Pause menu
        [SerializeField] private UIDocument pauseMenuUIDocument;
        private Button continueButton;
        private Button pauseMenuExitButton;

        private void Start()
        {
            this.gameModel.GameStatus.Subscribe(gameStatus =>
            {
                switch (gameStatus)
                {
                    case GameStatus.GameOver:
                        this.gameOverUIDocument.rootVisualElement.visible = true;
                        var score = this.gameModel.Score.Value;
                        this.scoreLabel.text = score == 1 ? $"{score} point" : $"{score} points";
                        break;
                    case GameStatus.Paused:
                        this.pauseMenuUIDocument.rootVisualElement.visible = true;
                        break;
                    default:
                        this.pauseMenuUIDocument.rootVisualElement.visible = false;
                        this.gameOverUIDocument.rootVisualElement.visible = false;
                        break;
                }
            });

            // Game over
            this.gameOverUIDocument.rootVisualElement.visible = false;
            this.scoreLabel = this.gameOverUIDocument.rootVisualElement.Q<Label>("score-label");
            this.restartButton = this.gameOverUIDocument.rootVisualElement.Q<Button>("restart-button");
            this.restartButton.clicked += OnRestart;
            
            // Main menu
            this.mainMenuUIDocument.rootVisualElement.visible = true;
            this.startButton = this.mainMenuUIDocument.rootVisualElement.Q<Button>("start-button");
            this.mainMenuExitButton = this.mainMenuUIDocument.rootVisualElement.Q<Button>("exit-button");
            this.startButton.clicked += OnStartOrContinue;
            this.mainMenuExitButton.clicked += OnGameExit;
            
            // Pause menu
            this.pauseMenuUIDocument.rootVisualElement.visible = false;
            this.continueButton = this.pauseMenuUIDocument.rootVisualElement.Q<Button>("continue-button");
            this.pauseMenuExitButton = this.pauseMenuUIDocument.rootVisualElement.Q<Button>("exit-button");
            this.continueButton.clicked += OnStartOrContinue;
            this.pauseMenuExitButton.clicked += OnGameExit;
            this.inputManager.Pause += OnPause;
        }

        private void OnRestart()
        {
            this.gameModel.GameStatus.Value = GameStatus.Playing;
            this.gameOverUIDocument.rootVisualElement.visible = false;
        }

        private void OnStartOrContinue()
        {
            this.gameModel.GameStatus.Value = GameStatus.Playing;
            this.mainMenuUIDocument.rootVisualElement.visible = false;
            this.pauseMenuUIDocument.rootVisualElement.visible = false;
        }

        private void OnPause()
        {
            var currentStatus = this.gameModel.GameStatus.Value;
            if (currentStatus != GameStatus.Playing && currentStatus != GameStatus.Paused)
            {
                return;
            }
            this.gameModel.GameStatus.Value = currentStatus == GameStatus.Paused ? GameStatus.Playing : GameStatus.Paused;
        }

        private void OnGameExit()
        {
            Application.Quit();
        }
    }
}