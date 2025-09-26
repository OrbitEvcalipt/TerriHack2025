using UnityEngine;

namespace FunnyBlox.Utils
{
    public class GPUInstancingEnabler : MonoBehaviour
    {
        private void Awake()
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer)
                meshRenderer.SetPropertyBlock(materialPropertyBlock);
            else
            {
                SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
                if (skinnedMeshRenderer)
                    skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
            }
        }
    }
}