using CommonScripts;
using UnityEngine;
using WeaponScripts.Projectiles;
using WeaponScripts.ProjectilePool;

namespace WeaponScripts
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private Transform _startFirePlace;
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private Light _muzzleFlashLight;
        [SerializeField] private Transform _playerCamera;
        [SerializeField] private Transform _gun;
        [SerializeField] private Transform _target;
        [SerializeField] private bool _isMine;

        private ProjectileDestination _destination;
        private RocketPool _rocketPool;
        private StonePool _stonePool;
        private float _timer;

        private void Start()
        {
            _muzzleFlash?.Stop();
            _muzzleFlashLight.enabled = false;
            _destination = new ProjectileDestination();
            _rocketPool = ServiceLocator.GetController<RocketPool>();
            _stonePool = ServiceLocator.GetController<StonePool>();
        }

        private void Update()
        {
            if (_isMine)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Fire();
                }   
            }
            else
            {
                BotFireLogic();
            }
        }

        public void Fire()
        {
            var destination = _destination.GetBulletDestination(_playerCamera, _startFirePlace);
            GetCreatedBullet(!_isMine).transform.LookAt(destination);
            ShowMuzzleFlash();
        }

        private BaseProjectile GetCreatedBullet(bool isBot)
        {
            if (isBot)
            {
                var stone = (StoneProjectile)_stonePool.Pop();
                return GetSettedProjectile(stone);
            }
            else
            {
                var rocket = (RocketProjectile)_rocketPool.Pop();
                return GetSettedProjectile(rocket);
            }
        }

        private BaseProjectile GetSettedProjectile(BaseProjectile projectile)
        {
            projectile.transform.position = _startFirePlace.position;
            projectile.transform.rotation = Quaternion.identity;
            return projectile;
        }

        private void ShowMuzzleFlash()
        {
            _muzzleFlash.Play();
            _muzzleFlashLight.enabled = true;
        }

        private void BotFireLogic()
        {
            if (_timer > 1f)
            {
                var stone = (StoneProjectile) GetCreatedBullet(!_isMine);
                stone.SetTarget(_target);
                ShowMuzzleFlash();
                _timer = 0f;
            }

            _timer += Time.deltaTime;
            _gun.LookAt(_target);
        }
    }
}