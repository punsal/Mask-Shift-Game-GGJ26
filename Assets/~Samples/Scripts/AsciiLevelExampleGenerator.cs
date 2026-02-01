// ASCIILevelExampleGenerator.cs
// Optional helper: creates a tiny sample level in code if you don't want to author ScriptableObjects yet.
// Drop it on an empty GameObject, press Play, it will create a LevelData instance in memory and spawn it.

using Gameplay;
using UnityEngine;

namespace _Samples
{
    public class AsciiLevelExampleGenerator : MonoBehaviour
    {
        [Header("Testing")]
        [SerializeField] private MaskType initialType;
        
        [Header("Dependencies")]
        [SerializeField] private LevelSpawner spawner;

        private void Awake()
        {
            if (LevelRuntime.Instance == null)
            {
                Debug.LogError("No LevelRuntime found.");
                return;
            }
            
            if (spawner == null)
            {
                Debug.LogError("No LevelSpawner assigned. Searching in scene..");
                spawner = FindFirstObjectByType<LevelSpawner>();
            }

            if (spawner == null)
            {
                Debug.LogError("No LevelSpawner found.");
                return;
            }

            var level = ScriptableObject.CreateInstance<LevelData>();
            level.width = 10;
            level.height = 8;
            level.tileSize = 1f;
            level.rows = new[]
            {
                "..........",
                "..G....#..",
                "..#....#..",
                "..#..S.#..",
                "..#....#..",
                "..#....#E.",
                "..P....H..",
                "..........",
            };

            LevelRuntime.Instance.activeLevel = level;
        }

        private void Start()
        {
            spawner.Spawn();
            
            if (MaskState.Instance == null)
            {
                Debug.LogError("No MaskState found.");
                return;
            }
            MaskState.Instance.SetMask(initialType, true);
        }
    }
}