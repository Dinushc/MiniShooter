using System;
using System.Linq;
using System.Runtime.Serialization;

namespace CommonScripts
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _activeEnemyPrefab;
        [SerializeField] private GameObject _passiveEnemyPrefab;
        [SerializeField] private SpawnPlaceHolder _spawnPositions;
        [SerializeField] private Transform _playerSpawnPosition;
        private List<GameObject> _spawnedCharacters = new List<GameObject>();
            
        private void Start()
        {
            SpawnPlayer();
            SpawnEnemies();
            ServiceLocator.GetController<UnitsHolder>().FillCharactersList(_spawnedCharacters);
        }

        private void SpawnPlayer()
        {
            var player = Instantiate(_playerPrefab, _playerSpawnPosition.position, Quaternion.identity);
            _spawnedCharacters.Add(player);
        }

        private void SpawnEnemies()
        {
            int i = 0;
            var enemyPrefab = _activeEnemyPrefab;
            var spawnPlaces = _spawnPositions.SpawnPlaces;
            if (spawnPlaces.Count <= 0)
            {
                throw new InvalidDataContractException(nameof(spawnPlaces.Count));
            }
                foreach (var place in spawnPlaces)
            {
                enemyPrefab = (i % 2 == 0) ? _activeEnemyPrefab : _passiveEnemyPrefab;
                var enemy = Instantiate(enemyPrefab, place.position, Quaternion.identity);
                enemy.transform.LookAt(_playerSpawnPosition);
                _spawnedCharacters.Add(enemy);
                i++;
            }
        }
    }   
}
