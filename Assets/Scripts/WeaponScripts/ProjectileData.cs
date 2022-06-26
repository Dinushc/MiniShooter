namespace WeaponScripts
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Projectile", menuName = "ProjectileData", order = 53)]
    public class ProjectileData : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private ProjectileType _projectileType;

        public ProjectileType GetProjectileType
        {
            get
            {
                return _projectileType;
            }
        }

        public GameObject ProjectilePrefab
        {
            get
            {
                return _projectilePrefab;
            }
        }

        public float Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value;
            }
        }
        
        public float LifeTime
        {
            get
            {
                return _lifeTime;
            }

            set
            {
                _lifeTime = value;
            }
        }
    }
}