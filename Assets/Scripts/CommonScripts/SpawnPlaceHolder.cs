using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CommonScripts
{
    public class SpawnPlaceHolder : MonoBehaviour
    {
        private List<Transform> _spawnPlaces;

        public IReadOnlyList<Transform> SpawnPlaces => _spawnPlaces;
        private void Awake()
        {
            _spawnPlaces = new List<Transform>();
            FillPlaces();
        }

        private void FillPlaces()
        {
            _spawnPlaces = GetComponentsInChildren<Transform>().ToList();
            if(_spawnPlaces.Contains(transform))
                _spawnPlaces.Remove(transform);
        }
    }
}