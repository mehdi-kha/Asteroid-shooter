using Modules.Game;
using UniRx;
using UnityEngine;
using Zenject;

namespace Modules.UI
{
    public class UIController : MonoBehaviour
    {
        [Inject] private IGameModel gameModel;

        private void Start()
        {
            this.gameModel.GameStatus.Subscribe(gameStatus =>
            {
                if (gameStatus == GameStatus.GameOver)
                {
                    Debug.Log("Game over");
                }
            });
        }
    }
}