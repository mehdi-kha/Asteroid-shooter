using Modules.Enemies.EnemyType;
using Modules.Enemies.NumbersSprites;
using UniRx;
using UnityEngine;

namespace Modules.Enemies
{
    /// <summary>
    ///     An enemy has a number assigned to it, and moved down the screen at a certain speed.
    /// </summary>
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private SpriteRenderer background;

        [SerializeField] private SpriteRenderer numberSprite;

        [SerializeField] private NumbersSpritesScriptableObject numbersSprites;

        private int number;

        public Sprite Sprite
        {
            get => this.background.sprite;
            set { this.background.sprite = value; }
        }

        public int Number
        {
            get => this.number;
            set
            {
                this.number = value;
                this.numberSprite.sprite = numbersSprites.NumbersSprites[value];
            }
        }

        private float speed = 0;
        
        public float BottomWorldPosition { get; set; }
        public EnemyTypeScriptableObject EnemyType { get; set; }

        void Update()
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            if (this.transform.position.y < this.BottomWorldPosition)
            {
                this.BottomReached.Execute(this);
            }
        }

        public IReactiveCommand<IEnemy> BottomReached { get; private set; }

        public void SetPosition(Vector2 wordPosition)
        {
            this.transform.position = wordPosition;
        }

        public void Destroy()
        {
            Destroy(this.gameObject);
        }

        public void Move()
        {
            this.speed = this.EnemyType.Speed;
        }

        public void Stop()
        {
            this.speed = 0;
        }

        public void Awake()
        {
            this.BottomReached = new ReactiveCommand<IEnemy>();
        }
    }
}