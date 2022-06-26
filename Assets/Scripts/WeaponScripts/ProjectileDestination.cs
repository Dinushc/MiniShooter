using UnityEngine;

namespace WeaponScripts
{
    public class ProjectileDestination
    {
        public Vector3 GetBulletDestination(Transform cameraTransform, Transform startFirePlace)
        {
            var screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 10);
            Ray ray = cameraTransform.GetComponent<Camera>().ScreenPointToRay(screenCenter);
            var bulletDestination = Vector3.one;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Enemy"))
                {
                    //hitObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                }

                bulletDestination = Vector3.Lerp(startFirePlace.position, hit.point, Time.deltaTime);
            } else
            {
                var destination = new Vector3(ray.direction.x, ray.direction.y + 0.001f, ray.direction.z);
                bulletDestination = Vector3.Lerp(startFirePlace.position, destination * 10000, Time.deltaTime);
            }

            return bulletDestination;
        }
    }
}