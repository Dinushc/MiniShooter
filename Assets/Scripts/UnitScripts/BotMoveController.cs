namespace UnitScripts
{
 using System;
 using System.Collections;
 using System.Collections.Generic;
 using UnitScripts;
 using UnityEngine;
 
 [RequireComponent(typeof(CharacterController))]
 public class BotMoveController : BaseMoveController
 {
     private CharacterController _controller;
     private float _speed = 1f;
     
     /****************/
     [SerializeField] private GameObject _bulletPrefab;
     [SerializeField] private Transform _startFirePlace;
     [SerializeField] private ParticleSystem _muzzleFlash;
     [SerializeField] private Light _muzzleFlashLight;
     [SerializeField] private Transform _target;
 
     [SerializeField] private Transform _gun;
     private float _timer;
     private bool _canFire = true;
 
     protected override void Start()
     {
         base.Start();
         _controller = GetComponent<CharacterController>();
     }
 
     protected override void TryMoveCharacter()
     {
         if (transform.position.x < -8)
         {
             _speed = -1;
         }
         if (transform.position.x > 6)
         {
             _speed = 1;
         }
         var inputAxis = transform.right * 1 + transform.forward * 0;
         _controller.Move(inputAxis * _speed * Time.deltaTime);
     }
 
     private void Update()
     {
         TryMoveCharacter();
         
         /***********************/
         /*if (_timer > 1f)
         {
             _canFire = true;
         }
 
         if (_canFire)
         {
             var bullet = Instantiate(_bulletPrefab, _startFirePlace.position, Quaternion.identity);
             bullet.transform.LookAt(_target);
             _muzzleFlash?.Play();
             _muzzleFlashLight.enabled = true;
             _canFire = false;
             _timer = 0f;
         }
 
         _timer += Time.deltaTime;
         _gun.LookAt(_target);*/
     }
 }
}