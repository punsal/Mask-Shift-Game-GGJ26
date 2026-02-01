using UnityEngine;

namespace Core.Level
{
    /// <summary>
    /// Represents the configuration and structure of a level in the game, stored as a ScriptableObject.
    /// This class defines the grid dimensions, tile size, and ASCII-based map layout for the level.
    /// </summary>
    [CreateAssetMenu(menuName = "Core/LevelData", fileName = "Level_00")]
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
        // 'S' secret passage marker     -> visible only in Reveal (looks like wall)
        // 'H' hidden wall/trap tile     -> visible only in Reveal; blocks movement

        /// <summary>
        /// Retrieves the character at the specified grid coordinates (x, y) from the level ASCII map.
        /// If the coordinates are out of bounds or the map is not defined, returns a default value '.'.
        /// </summary>
        /// <param name="x">The horizontal index of the grid (column).</param>
        /// <param name="y">The vertical index of the grid (row).</param>
        /// <returns>The character representing the tile at the specified grid coordinates.</returns>
        public char Get(int x, int y)
        {
            if (rows == null || rows.Length == 0)
            {
                return '.';
            }

            if (y < 0 || y >= rows.Length)
            {
                return '.';
            }
            
            var row = rows[y] ?? "";
            
            if (x < 0 || x >= row.Length)
            {
                return '.';
            }
            
            return row[x];
        }
    }
}