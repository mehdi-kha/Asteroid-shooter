using Modules.Enemies.NumbersSprites;
using UnityEngine;

namespace Modules.Enemies
{
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

        private float speed = 2;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }
}