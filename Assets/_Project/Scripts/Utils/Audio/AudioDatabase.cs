using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FunnyBlox.Utils
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "Deslab/Audio/AudioDatabase", order = 1)]
    public class AudioDatabase : SerializedScriptableObject
    {
        public Dictionary<string, AudioClip> audioDatabase = new();

#if UNITY_EDITOR

        //[HorizontalGroup("Split", 0.5f)]
        [Button(ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
        private void UpdateDatabase()
        {
            string[] keys = audioDatabase.Keys.ToArray();
            Debug.LogError("AudioDatabase has been successfully updated!");
            EnumBuilder.Build("AudioDatabaseEnum", keys);
        }
#endif
    }
}
