using System;

namespace UnitScripts
{
    using UnityEngine;

    [RequireComponent(typeof(CharacterController))]
    public class BotMoveController : BaseMoveController
    {
        private CharacterController _controller;
        [SerializeField] private float _speed = 1f;
        private float _timer;
        private bool _canFire = true;
        private const string BoardTag = "Board";
        private const string ObstacleTag = "Obstacle";

        protected override void Start()
        {
            base.Start();
            _controller = GetComponent<CharacterController>();
        }

        protected override void TryMoveCharacter()
        {
            var inputAxis = transform.right * 1 + transform.forward * 0;
            _controller.Move(inputAxis * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(BoardTag))
            {
                _speed *= -1;
            }
        }

        private void Update()
        {
            TryMoveCharacter();
        }
    }
}