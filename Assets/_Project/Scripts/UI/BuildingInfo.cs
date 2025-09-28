using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FunnyBlox
{
    public class BuildingInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text _hpLabel;
        [SerializeField] private List<GameObject> connections;
        [SerializeField] private AnchorToTransform _anchorToTransform;
        public AnchorToTransform AnchorToTransform => _anchorToTransform;

        public void SetTarget(Transform target) => _anchorToTransform.SetTarget(target);
        
        public void SetHp(int hp) => _hpLabel.SetText(hp.ToString());

        public void EnableConnection(int level)
        {
            DisableAllConnections();
            for (var i = 0; i <= level; i++)
            {
                var shouldBeActive = i <= level;
                connections[i].SetActive(shouldBeActive);
            }
        }

        public void DisableAllConnections()
        {
            foreach (var connection in connections) 
                connection.SetActive(false);
        }
    }
}