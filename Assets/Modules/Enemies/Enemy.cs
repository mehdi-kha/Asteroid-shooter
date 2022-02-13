using UnityEngine;

namespace Modules.Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public Sprite Sprite
        {
            get => this.spriteRenderer.sprite;
            set
            {
                this.spriteRenderer.sprite = value;
            }
        }

        public int Number { get; set; }

        private float speed = 2;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }
}