using System.Collections.Generic;
using Modules.Game;
using Modules.Inputs;
using Modules.UI;
using NSubstitute;
using NUnit.Framework;
using UniRx;
using UnityEngine;

namespace Modules.Enemies.Tests
{
    [TestFixture]
    public class EnemiesManagerTest
    {
        private EnemiesManager enemiesManager;
        private IEnemiesModel enemiesModel;
        private IGameModel gameModel;
        private IInputManager inputManager;
        private IUIModel uiModel;
        
        [SetUp]
        public void Setup()
        {
            var enemiesManagerGameObject = new GameObject("Enemies Manager");
            this.enemiesManager = enemiesManagerGameObject.AddComponent<EnemiesManager>();

            this.enemiesModel = Substitute.For<IEnemiesModel>();
            this.enemiesModel.VisibleEnemies.Returns(new ReactiveCollection<IEnemy>());
            this.gameModel = Substitute.For<IGameModel>();
            this.gameModel.Score.Returns(new ReactiveProperty<int>());
            this.inputManager = Substitute.For<IInputManager>();
            this.inputManager.Shoot.Returns(new ReactiveCommand<int>());
            this.uiModel = Substitute.For<IUIModel>();
            
            this.enemiesManager.Initialize(this.inputManager, this.enemiesModel, this.uiModel, this.gameModel);
        }

        [Test]
        public void Shoot_Triggered_ScoreIncreased()
        {
            // Mock 1 visible enemy with the value 0
            var score = new ReactiveProperty<int>();
            this.gameModel.Score.Returns(score);
            var enemy = Substitute.For<IEnemy>();
            enemy.Number.Returns(0);
            var enemies = new List<IEnemy> { enemy };
            this.enemiesModel.VisibleEnemies.Returns(new ReactiveCollection<IEnemy>(enemies));
            this.gameModel.Score.Value = 0;

            this.inputManager.Shoot.Execute(0); // Key 0 was pressed for instance, which corresponds to the visible
            
            Assert.AreEqual(1, this.gameModel.Score.Value);
        }

        [Test]
        public void VisibleEnemy_Added_EnemyMoves()
        {
            var enemy = Substitute.For<IEnemy>();
            
            this.enemiesModel.VisibleEnemies.Add(enemy);
            
            enemy.Received().Move();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(enemiesManager);
        }
    }
}
