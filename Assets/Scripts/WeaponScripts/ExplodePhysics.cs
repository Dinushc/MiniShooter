using UnityEngine;

namespace WeaponScripts
{
    public class ExplodePhysics
    {
        public void Explode(Transform transform, float explosionForce)
        {
            var radius = 10f;
            Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObjects in collidersToMove)
            {
                Rigidbody rigidBodies = nearbyObjects.GetComponent<Rigidbody>();
                if (rigidBodies != null)
                {
                    Vector3 position = transform.position;
                    Vector3 colliderPosition = nearbyObjects.transform.position;
                    RaycastHit hit;
                    if(Physics.Raycast(position, colliderPosition - position, out hit, Mathf.Infinity))
                    {
                        if(hit.collider == nearbyObjects)
                        {
                            rigidBodies.AddExplosionForce(explosionForce, transform.position, radius);
                        }
                    }
                }
            }
        }
    }
}