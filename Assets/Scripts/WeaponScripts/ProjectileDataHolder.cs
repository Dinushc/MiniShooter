namespace WeaponScripts
{
    using UnityEngine;
    
    public class ProjectileDataHolder : MonoBehaviour
    {
        private ProjectileData[] _projectileDataArray;
        private string _folderPath = "SO/Projectiles";

        private void Awake()
        {
            LoadProjectileData();
        }

        private void LoadProjectileData()
        {
            _projectileDataArray = Resources.LoadAll<ProjectileData>(_folderPath);
            CheckTypesArray(_projectileDataArray);
        }

        private void CheckTypesArray(ProjectileData[] array)
        {
            if (array.Length == 0)
            {
                Debug.LogError(array.ToString() + " is empty");
            }
        }

        public ProjectileData[] GetProjectilesData()
        {
            return _projectileDataArray;
        }
    }
}