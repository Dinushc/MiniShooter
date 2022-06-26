using UnityEngine;

namespace WeaponScripts.Projectiles
{
    public interface IHaveTarget
    {
        void SetTarget(Transform targetTransform);
    }
}