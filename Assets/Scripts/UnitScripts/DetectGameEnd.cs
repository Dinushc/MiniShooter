using System;
using CommonScripts;
using UnityEngine;

namespace UnitScripts
{
    public class DetectGameEnd : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        private bool _failed = false;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Finish"))
            {
                StartCoroutine(_gameController.WaitAndRestart(true));
            }
        }

        private void CheckFallDown()
        {
            if (transform.position.y < -2f)
            {
                StartCoroutine(_gameController.WaitAndRestart(false));
                _failed = true;
            }
        }

        private void Update()
        {
            if (_failed)
                return;
            CheckFallDown();
        }
    }
}