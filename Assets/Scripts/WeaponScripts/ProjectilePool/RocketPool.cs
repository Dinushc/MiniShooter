using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommonScripts;
using UnityEngine;
using WeaponScripts.Projectiles;

namespace WeaponScripts.ProjectilePool
{
    [RequireComponent(typeof(ProjectileCreator))]
    public class RocketPool : MonoBehaviour, IProjectilePool
    {
        [SerializeField] private int _startCount = 0;
        private ProjectileCreator _creator;
        private LinkedList<RocketProjectile> _bullets;

        private void Awake()
        {
            ServiceLocator.RegisterController(this);
        }

        void Start()
        {
            _creator = GetComponent<ProjectileCreator>();
            _bullets = new LinkedList<RocketProjectile>();
            StartCoroutine(WaitNextTick());
        }

        private IEnumerator WaitNextTick()
        {
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < _startCount; i++)
            {
                CreateProjectile();
            }
            yield return null;
        }

        public void CreateProjectile()
        {
            var realRocketProjectile = _creator.CreateProjectileToPool(ProjectileType.Rocket);
            Push(realRocketProjectile);
        }

        public BaseProjectile Pop()
        {
            if (_bullets.Count == 0)
            {
                Debug.Log("pusto???");
                CreateProjectile();
            }
            RocketProjectile result = _bullets.First.Value;
            if (_bullets.First.Equals(_bullets.Last))
            {
                Debug.Log("Detect");
            }
            result.gameObject.SetActive(true);
            _bullets.RemoveFirst();
            Debug.Log("pipitka: " + result);
            return result;
        }

        public void Push(BaseProjectile rocketProjectile)
        {
            var rocket = (RocketProjectile) rocketProjectile;
            if (!_bullets.Contains(rocket))
            {
                rocket.gameObject.SetActive(false);
                _bullets.AddLast(rocket);
            }
            else
            {
                Debug.LogError("BulletPool.Push: error: already has this rocket");
            }
        }

        public bool CheckContainsInPool(RocketProjectile rocket)
        {
            return _bullets.Contains(rocket);
        }
    }
}