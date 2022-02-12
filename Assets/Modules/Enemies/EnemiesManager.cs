using Modules.Inputs;
using UnityEngine;
using Zenject;
using UniRx;

public class EnemiesManager : MonoBehaviour
{
    [Inject] private IInputManager inputManager;

    void Start()
    {
        inputManager.Shoot.Subscribe(OnShoot);
    }

    private void OnShoot(int number)
    {
        Debug.Log(number);
    }
}
