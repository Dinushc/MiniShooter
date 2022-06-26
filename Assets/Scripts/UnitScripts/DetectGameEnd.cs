using System;
using CommonScripts;
using UnityEngine;

namespace UnitScripts
{
    public class DetectGameEnd : MonoBehaviour
    {
        private GameController _gameController;
        private bool _failed = false;


        private void Start()
        {
            _gameController = ServiceLocator.GetController<GameController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
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