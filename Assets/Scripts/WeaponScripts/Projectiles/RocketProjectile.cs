using System.Threading.Tasks;
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
             if(!_explosion.isPlaying)
                transform.Translate(Vector3.forward * _data.Speed * Time.deltaTime);
             _lifeTime -= Time.deltaTime;
             if (_lifeTime <= 2f)
             {
                 Explosion();
             }

             if (_lifeTime <= float.Epsilon)
             {
                 PushRocketToPool();
             }
         }

         private void OnTriggerEnter(Collider other)
         {
             Collide(other);
         }

         private void Explosion()
         {
             _explosion.gameObject.SetActive(true);
             _explosion.Play();
             _explodePhysics.Explode(transform, 1400f);
         }

         private void PushRocketToPool()
         {
             _lifeTime = _data.LifeTime;
             _pool.Push(this);
         }
     
         private async void ExplodeAndDestroy(Collider other)
         {
             Explosion();
             TryDestroyGameObject(other.gameObject, other.tag);
             await Task.Delay(1000);
             if(!_pool.CheckContainsInPool(this))
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
             if (other.CompareTag("Player"))
                 return;
             ExplodeAndDestroy(other);
         }
     }
}