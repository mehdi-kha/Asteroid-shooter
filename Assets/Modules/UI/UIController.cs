using System;
using Modules.Game;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Modules.UI
{
    public class UIController : MonoBehaviour
    {
        [Inject] private IGameModel gameModel;

        // Game over
        [SerializeField] private UIDocument gameOverUIDocument;
        private Button restartButton;
        private Label scoreLabel;
        
        // Main menu
        [SerializeField] private UIDocument mainMenuUIDocument;
        private Button startButton;

        private void Start()
        {
            this.gameModel.GameStatus.Subscribe(gameStatus =>
            {
                if (gameStatus == GameStatus.GameOver)
                {
                    this.gameOverUIDocument.rootVisualElement.visible = true;
                    var score = this.gameModel.Score.Value;
                    this.scoreLabel.text = score == 1 ? $"{score} point" : $"{score} points";
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
            this.startButton.clicked += OnStart;
        }

        private void OnRestart()
        {
            this.gameModel.GameStatus.Value = GameStatus.Playing;
            this.gameOverUIDocument.rootVisualElement.visible = false;
        }

        private void OnStart()
        {
            this.gameModel.GameStatus.Value = GameStatus.Playing;
            this.mainMenuUIDocument.rootVisualElement.visible = false;
        }
    }
}