using UnityEngine;

namespace Gameplay
{
    public class LevelRuntime : MonoBehaviour
    {
        public static LevelRuntime Instance { get; private set; }

        public LevelData activeLevel;

        public Vector2Int PlayerStart { get; private set; }
        public Vector2Int ExitPos { get; private set; }

        private bool[,] _blocked;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void BuildBlockGrid()
        {
            if (activeLevel == null)
            {
                Debug.LogError($"LevelRuntime: {nameof(activeLevel)} has not yet initialized");
                return;
            }
            
            var level = activeLevel;
            _blocked = new bool[level.width, level.height];
            
            PlayerStart = new Vector2Int(0, 0);
            ExitPos = new Vector2Int(level.width - 1, level.height - 1);

            for (var i = 0; i < level.height; i++)
            {
                for (var j = 0; j < level.width; j++)
                {
                    var c = level.Get(j, i);
                    switch (c)
                    {
                        case 'P': PlayerStart = new Vector2Int(j, i); break;
                        case 'E': ExitPos = new Vector2Int(j, i); break;
                        case '#':
                        case 'H': _blocked[j, i] = true; break;
                    }
                }
            }
        }
        
        public bool IsBlocked(Vector2Int gridPos)
        {
            if (activeLevel == null || _blocked == null)
            {
                Debug.LogError($"LevelRuntime: {nameof(activeLevel)} has not yet been initialized");
                return true;
            }

            if (gridPos.x < 0 || gridPos.x >= activeLevel.width)
            {
                Debug.LogError($"GridPos {gridPos} is out of range");
                return true;
            }

            if (gridPos.y < 0 || gridPos.y >= activeLevel.height)
            {
                Debug.LogError($"GridPos {gridPos} is out of range");
                return true;
            }
            
            return _blocked[gridPos.x, gridPos.y];
        }
    }
}