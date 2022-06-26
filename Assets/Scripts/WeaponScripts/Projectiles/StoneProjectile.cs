using System;
using CommonScripts;
using UnitScripts;
using UnityEngine;
using WeaponScripts.ProjectilePool;

namespace WeaponScripts.Projectiles
{
    public class StoneProjectile : BaseProjectile, IHaveTarget
    {
        private ProjectileData _data;
        private StonePool _pool;
        private float _lifeTime;
        private const string PlayerTag = "Player";
        private Vector3 _firstPosition = Vector3.zero;

        private void Start()
        {
            _pool = ServiceLocator.GetController<StonePool>();
            _lifeTime = _data.LifeTime;
        }

        public override void SetProjectileData(ProjectileData data)
        {
            _data = data;
        }
        
        private void Update()
        {
            if (_firstPosition == Vector3.zero)
            {
                _firstPosition = transform.position;
            }
            transform.Translate(Vector3.forward * _data.Speed * Time.deltaTime);
            _lifeTime -= Time.deltaTime;
            if (_lifeTime <= float.Epsilon)
            {
                _lifeTime = _data.LifeTime;
                _pool.Push(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Collide(other);
        }
        
        public override void Collide(Collider collider)
        {
            if (collider.CompareTag(PlayerTag))
            {
                var velocity = _firstPosition - collider.transform.position;
                collider.attachedRigidbody.AddForce(velocity * -5, ForceMode.Impulse);
            }
            _pool.Push(this);
        }

        public void SetTarget(Transform targetTransform)
        {
            transform.LookAt(targetTransform);
        }
    }
}