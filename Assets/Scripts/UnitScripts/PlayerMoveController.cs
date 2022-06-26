namespace UnitScripts
{
    using CommonScripts;
    using UnitScripts;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMoveController : BaseMoveController
    {
        private Vector3 _movementInput;
        private Rigidbody _rigidbody;
        [SerializeField] private Transform _feet;
        [Space] 
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [Space] 
        [SerializeField] private LayerMask _mask;

        protected override void Start()
        {
            base.Start();
            _rigidbody = GetComponent<Rigidbody>();
        }

        protected override void TryMoveCharacter()
        {
            if (Physics.CheckSphere(_feet.position, 0.2f, _mask))
            {
                var speed = Input.GetKey(KeyCode.LeftShift) ? _speed * 2 : _speed;
                var moveVector = transform.TransformDirection(_movementInput) * speed;
                _rigidbody.velocity = new Vector3(moveVector.x, _rigidbody.velocity.y, moveVector.z);
            
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);   
                }
            }
        }

        private void Update()
        {
            _movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            TryMoveCharacter();
        }
    }
}