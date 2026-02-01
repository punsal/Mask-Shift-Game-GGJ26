using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/LevelData", fileName = "Level_00")]
    public class LevelData : ScriptableObject
    {
        [Header("Grid")]
        public int width = 10;
        public int height = 8;
        public float tileSize = 1;
        
        [Header("ASCII Map (top row first)")]
        [TextArea(6, 20)]
        public string[] rows;

        // Legend (keep it dead simple):
        // '.' floor (walkable)
        // '#' wall (blocks movement)    -> visible only in Reveal
        // 'P' player start              -> treated as floor for tiles
        // 'E' exit                      -> visible only in Reveal
        // 'G' ghost enemy/trap          -> visible only in Ghost
        // 'S' secret passage marker     -> visible only in Reveal (still floor)
        // 'H' hidden wall/trap tile     -> visible only in Reveal; blocks movement

        public char Get(int x, int y)
        {
            if (rows == null || rows.Length == 0) return '.';
            if (y < 0 || y >= rows.Length) return '.';
            var row = rows[y] ?? "";
            if (x < 0 || x >= row.Length) return '.';
            return row[x];
        }
    }
}