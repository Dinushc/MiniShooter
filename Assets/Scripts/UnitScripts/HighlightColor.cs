using System;
using UnityEngine;

namespace UnitScripts
{
    public class HighlightColor : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private void OnMouseEnter()
        {
            _meshRenderer.material.color = Color.yellow;
        }

        private void OnMouseExit()
        {
            _meshRenderer.material.color = Color.red;
        }
    }
}