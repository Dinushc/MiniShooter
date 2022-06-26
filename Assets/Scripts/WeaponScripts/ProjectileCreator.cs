using WeaponScripts.Projectiles;

namespace WeaponScripts
{
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(ProjectileDataHolder))]
    public class ProjectileCreator : MonoBehaviour
    {
        [SerializeField] private ProjectileData[] _projectileData;
        private Dictionary<ProjectileType, ProjectileData> _projectiles;
        private ProjectileDataHolder _projectileDataHolder;

        public Dictionary<ProjectileType, ProjectileData> Projectiles
        {
            get
            {
                return _projectiles;
            }

            set
            {
                _projectiles = value;
            }
        }

        private void Start()
        {
            _projectileData = GetComponent<ProjectileDataHolder>().GetProjectilesData();
            Projectiles = new Dictionary<ProjectileType, ProjectileData>();
            FillProjectilesDictionary();
        }

        private void FillProjectilesDictionary()
        {
            foreach(ProjectileData data in _projectileData)
            {
                Projectiles.Add(data.GetProjectileType, data);
            }
        }

        public BaseProjectile CreateProjectileToPool(ProjectileType type)
        {
            GameObject prefab = InnerCreator(type);
            GameObject projectile = Instantiate(prefab);
            BaseProjectile result = projectile.GetComponent<BaseProjectile>();
            result.SetProjectileData(Projectiles[type]);
            return result;
        }

        private GameObject InnerCreator(ProjectileType type)
        {
            GameObject prefab = null;
            if (Projectiles.ContainsKey(type))
            {
                prefab = Projectiles[type].ProjectilePrefab;
                return prefab;
            }
            else
            {
                Debug.LogError("ProjectileCreator: key was not found " + type);
                return null;
            }
        }
    }
}