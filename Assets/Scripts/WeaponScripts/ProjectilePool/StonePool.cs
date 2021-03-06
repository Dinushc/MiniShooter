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
        private LinkedList<StoneProjectile> _stones;

        private void Awake()
        {
            ServiceLocator.RegisterController(this);
        }

        void Start()
        {
            _creator = GetComponent<ProjectileCreator>();
            _stones = new LinkedList<StoneProjectile>();
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
            if (_stones.Count == 0)
            {
                CreateProjectile();
            }
            StoneProjectile result = _stones.First.Value;
            result.gameObject.SetActive(true);
            _stones.RemoveFirst();
            return result;
        }

        public void Push(BaseProjectile stoneProjectile)
        {
            var stone = (StoneProjectile) stoneProjectile;
            if (!_stones.Contains(stone))
            {
                stone.gameObject.SetActive(false);
                _stones.AddLast(stone);
            }
            else
            {
                Debug.LogError("BulletPool.Push: error: already has this stone");
            }
        }
    }
}