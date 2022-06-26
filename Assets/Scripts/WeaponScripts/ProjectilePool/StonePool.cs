using System;
using System.Collections;
using System.Collections.Generic;
using CommonScripts;
using UnityEngine;
using WeaponScripts.Projectiles;

namespace WeaponScripts.ProjectilePool
{
    [RequireComponent(typeof(ProjectileCreator))]
    public class StonePool : MonoBehaviour, IProjectilePool
    {
        [SerializeField] private int _startCount = 0;
        private ProjectileCreator _creator;
        private LinkedList<StoneProjectile> _bullets;

        private void Awake()
        {
            ServiceLocator.RegisterController(this);
        }

        void Start()
        {
            _creator = GetComponent<ProjectileCreator>();
            _bullets = new LinkedList<StoneProjectile>();
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
            var realStoneProjectile = _creator.CreateProjectileToPool(ProjectileType.Stone);
            Push(realStoneProjectile);
        }

        public BaseProjectile Pop()
        {
            if (_bullets.Count == 0)
            {
                
                Debug.Log("pusto???");
                CreateProjectile();
            }
            StoneProjectile result = _bullets.First.Value;
            result.gameObject.SetActive(true);
            _bullets.RemoveFirst();
            return result;
        }

        public void Push(BaseProjectile stoneProjectile)
        {
            var rocket = stoneProjectile as StoneProjectile;
            if (!_bullets.Contains(rocket))
            {
                rocket?.gameObject.SetActive(false);
                _bullets.AddLast(rocket);
            }
            else
            {
                Debug.LogError("BulletPool.Push: error: already has this stone");
            }
        }
    }
}