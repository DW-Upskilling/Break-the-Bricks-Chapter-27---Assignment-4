using UnityEngine;

using BreakTheBricks2D.GenericClass.MVC;

namespace BreakTheBricks2D.Component.Ball
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class BallView : View, IBall
    {
        Rigidbody2D rigidbody2DComponent;
        SpriteRenderer spriteRendererComponent;

        private void Awake()
        {
            rigidbody2DComponent = this.gameObject.GetComponent<Rigidbody2D>();
            spriteRendererComponent = this.gameObject.GetComponent<SpriteRenderer>();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject _gameObject = collision.gameObject;

            if(_gameObject.GetComponent<IPaddle>() != null) { }
        }

        protected override void SetController(Controller _controller)
        {
            this.Controller = _controller;
        }
    }
}
