using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using WeaponScripts.ProjectilePool;

namespace CommonScripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _gameOverText;

        private void Awake()
        {
            ServiceLocator.RegisterController(this);
        }

        public IEnumerator WaitAndRestart(bool won)
        {
            _gameOverPanel.SetActive(true);
            _gameOverText.text = GetGameOverText(won);
            yield return new WaitForSeconds(3f);
            ReloadScene();
            yield return null;
        }

        private string GetGameOverText(bool won)
        {
            var text = won ? "Victory" : "Defeat";
            return text;
        }

        private void ReloadScene()
        {
            SceneManager.LoadSceneAsync("SampleScene");
        }

        private void OnDestroy()
        {
            ServiceLocator.Clear();
            Destroy(gameObject);
        }
    }
}