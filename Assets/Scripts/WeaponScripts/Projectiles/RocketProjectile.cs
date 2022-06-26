using CommonScripts;
using WeaponScripts.ProjectilePool;

namespace WeaponScripts.Projectiles
{
    using UnityEngine;
     using WeaponScripts;
     
     public class RocketProjectile : BaseProjectile
     {
         [SerializeField] private ParticleSystem _explosion;
         private ExplodePhysics _explodePhysics;
         private float _lifeTime;
         private RocketPool _pool;
         private ProjectileData _data;
     
         private void Start()
         {
             _explodePhysics = new ExplodePhysics();
             _pool = ServiceLocator.GetController<RocketPool>();
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
                 _pool.Push(this);
             }
         }

         private void OnTriggerEnter(Collider other)
         {
             Collide(other);
         }
     
         private void ExplodeAndDestroy(Collider other)
         {
             _explosion.Play();
             _explodePhysics.Explode(transform, 1400f);
             TryDestroyGameObject(other.gameObject, other.tag);
             _pool.Push(this);
         }
     
         private void TryDestroyGameObject(GameObject gObject, string tag)
         {
             if(tag.Equals("Enemy"))
             {
                 Destroy(gObject);
             }
         }
     
         public override void Collide(Collider other)
         {
             _explosion.gameObject.SetActive(true);
             ExplodeAndDestroy(other);
         }
     }
}