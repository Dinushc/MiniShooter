using UnityEngine;

namespace WeaponScripts.Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        public virtual void SetProjectileData(ProjectileData data)
        {
            
        }

        public virtual void Collide(Collider collider)
        {
            
        }
    }
}