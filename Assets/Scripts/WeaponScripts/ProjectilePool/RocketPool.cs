using System.Collections;
using System.Collections.Generic;
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
        private LinkedList<RocketProjectile> _rockets;

        private void Awake()
        {
            ServiceLocator.RegisterController(this);
        }

        void Start()
        {
            _creator = GetComponent<ProjectileCreator>();
            _rockets = new LinkedList<RocketProjectile>();
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
            if (_rockets.Count == 0)
            {
                CreateProjectile();
            }
            RocketProjectile result = _rockets.First.Value;
            result.gameObject.SetActive(true);
            _rockets.RemoveFirst();
            return result;
        }

        public void Push(BaseProjectile rocketProjectile)
        {
            var rocket = (RocketProjectile) rocketProjectile;
            if (!_rockets.Contains(rocket))
            {
                rocket.gameObject.SetActive(false);
                _rockets.AddLast(rocket);
            }
            else
            {
                Debug.LogError("BulletPool.Push: error: already has this rocket");
            }
        }

        public bool CheckContainsInPool(RocketProjectile rocket)
        {
            return _rockets.Contains(rocket);
        }
    }
}