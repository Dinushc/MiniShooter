using WeaponScripts.Projectiles;

namespace WeaponScripts.ProjectilePool
{
    public interface IProjectilePool
    {
        void CreateProjectile();

        BaseProjectile Pop();

        void Push(BaseProjectile rocketProjectile);
    }
}