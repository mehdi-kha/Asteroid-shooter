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

        [SerializeField] private UIDocument uiDocument;

        private Button restartButton;
        private Label scoreLabel;

        private void Start()
        {
            this.gameModel.GameStatus.Subscribe(gameStatus =>
            {
                if (gameStatus == GameStatus.GameOver)
                {
                    this.uiDocument.rootVisualElement.visible = true;
                    var score = this.gameModel.Score.Value;
                    this.scoreLabel.text = score == 1 ? $"{score} point" : $"{score} points";
                }
            });

            this.uiDocument.rootVisualElement.visible = false;
            this.scoreLabel = this.uiDocument.rootVisualElement.Q<Label>("score-label");
            this.restartButton = this.uiDocument.rootVisualElement.Q<Button>("restart-button");
            this.restartButton.clicked += OnRestart;
        }

        private void OnRestart()
        {
            this.gameModel.GameStatus.Value = GameStatus.Playing;
            this.uiDocument.rootVisualElement.visible = false;
        }
    }
}