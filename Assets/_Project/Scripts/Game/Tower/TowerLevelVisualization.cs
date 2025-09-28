﻿using DG.Tweening;
using UnityEngine;

namespace FunnyBlox
{
    public class TowerLevelVisualization : MonoBehaviour
    {
        [SerializeField] private GameObject[] _vizualForLevel;
        [SerializeField] private BuildingInfo _buildingInfo;
        [SerializeField] private Vector2[] _pixelOffsets;

        [SerializeField] private Renderer[] renderer;

        public void Setup(BuildingInfo buildingInfo) => _buildingInfo = buildingInfo;

        public void UpdateOwner(ETowerOwnerType ownerType, TweenCallback onComplete)
        {
            float offset = ownerType switch
            {
                ETowerOwnerType.Enemy => 0.04f,
                ETowerOwnerType.Neutral => 0.07f,
                _ => 0f
            };

            foreach (Renderer r in renderer)
            {
                if (r.materials.Length == 1)
                    r.material.SetTextureOffset("_TextureSample1", new Vector2(offset, 0f));
                else
                    r.materials[1].SetTextureOffset("_TextureSample1", new Vector2(offset, 0f));
            }

            transform.DOScale(Vector3.zero, 0.15f).SetEase(Ease.InQuint).OnComplete(() =>
            {
                transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutBack).OnComplete(onComplete);
            });
            
            if (ownerType == ETowerOwnerType.Neutral)
                _buildingInfo.DisableAllConnections();
            else
                _buildingInfo.EnableConnection(0);
        }

        public void UpdateVisual(int level, TweenCallback onComplete)
        {
            for (int i = 0; i < _vizualForLevel.Length; i++)
            {
                var index = i;
                _vizualForLevel[i].transform.DOScale(Vector3.zero, 0.15f).SetEase(Ease.InQuint).OnComplete(() =>
                {
                    _vizualForLevel[index].SetActive(index == level);
                    _vizualForLevel[index].transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutBack)
                        .OnComplete(onComplete);
                    if (index == level)
                        _buildingInfo.AnchorToTransform.SetPixelOffset(_pixelOffsets[index]);
                });
                _buildingInfo.EnableConnection(level);
            }
        }

        public void UpdateHitPoints(int hitPoints) => _buildingInfo.SetHp(hitPoints);
    }
}