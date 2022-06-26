using System;
using System.Collections.Generic;
using UnityEngine;

namespace CommonScripts
{
    public class UnitsHolder : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _characters;
        private SpawnManager _spawnManager;

        private void Awake()
        {
            ServiceLocator.RegisterController(this);
        }

        public void FillCharactersList(List<GameObject> characters)
        {
            _characters = characters;
        }

        public Transform GetPlayerTransform()
        {
            return _characters[0].transform;
        }
    }
}