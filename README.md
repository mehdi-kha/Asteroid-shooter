# Asteroid shooter
Asteroid shooter is a small 2D game made with Unity. Asteroids fall from the sky, and you have to press the corresponding number on your keyboard in order to destroy them. If one of them reaches the bottom of the screen, the game is over.

1 enemy killed = 1 point added to the score.

You can find the latest Windows release here: https://developer.cloud.unity3d.com/share/share.html?shareId=ZkK_FmK11d
# Developer information
## Unity version
Unity 2021 (2021.2.11f1) is used, so that we can leverage 2 of its features:
- the new pooling system
- the UI builder
## Project architecture
The project is separated into different modules, installed by the Zenject installers (see the MonoInstallers in the MainScene):
- The Enemies module is the heart of the project: it handles enemies creation and destruction. New enemy types can easily be designed and added to the game by creating a new `EnemyTypeScriptableObject` and attaching it to the `EnemiesManager` in the MainScene.
- The Input module converts the user input into relevant game events. New input actions or control schemes (for example, for other platforms) can be defined in `PlayerInputActions`.
- The game module holds the game status (for example, if the game is paused), and the current score.
- The UI module handles the visibility of the menus, and the interaction the user has with them.

![Project's architecture](https://github.com/mehdi-kha/Asteroid-shooter/blob/master/architecture.jpg?raw=true)

## 3rd party plugins and assets
- [UniRx](https://github.com/neuecc/UniRx) is a library that makes it easier to implement observables in Unity.
- [Zenject](https://github.com/modesttree/Zenject) is a plugin that makes dependency injection easier.
- [2D Space Kit](https://assetstore.unity.com/packages/2d/environments/2d-space-kit-27662) for the sprites and backgrounds

## Ideas of future features
- Different kinds of enemies, with different speeds. The score increases differently depending on the type of enemy.
- Certain types of enemies can attack us, decreasing our score. It's in the user's best interest to destroy this more dangerous enemy first, before the other ones visible on the screen.
- Save and display the highest score.
