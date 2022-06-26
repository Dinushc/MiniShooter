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
            if (collider.CompareTag("Player"))
            {
                collider.attachedRigidbody.AddForce(Vector3.forward * -50, ForceMode.Impulse);
            }
            _pool.Push(this);
        }

        public void SetTarget(Transform targetTransform)
        {
            transform.LookAt(targetTransform);
        }
    }
}