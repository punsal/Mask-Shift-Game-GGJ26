using Core.Level;
using UnityEngine;

namespace Gameplay
{
    public class LevelSpawner : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject floorPrefab;
        public GameObject playerPrefab;
        public GameObject wallPrefab;
        [Tooltip("Optional")] public GameObject hiddenWallPrefab;
        public GameObject ghostPrefab;
        public GameObject exitPrefab;
        [Tooltip("Optional")] public GameObject secretPrefab;

        [Header("Parent")]
        [Tooltip("Optional")] public Transform worldRoot;

        public void Spawn()
        {
            if (LevelRuntime.Instance != null)
            {
                LevelRuntime.Instance.BuildBlockGrid();
            }

            var data = LevelRuntime.Instance.activeLevel;

            if (data == null)
            {
                Debug.LogError($"LevelSpawner: {nameof(data)} has not yet been initialized");
                return;
            }

            if (worldRoot == null)
            {
                Debug.Log($"WorldRoot is null, will use {gameObject.name} as worldRoot");
                worldRoot = transform;
            }

            for (var i = worldRoot.childCount - 1; i >= 0; i--)
            {
                Destroy(worldRoot.GetChild(i).gameObject);
            }

            var center = new Vector3(data.width * 0.5f - data.tileSize * 0.5f, data.height * 0.5f - data.tileSize * 0.5f, 0f);
            var floor = Instantiate(floorPrefab, center, Quaternion.identity, worldRoot);
            var floorSprite = floor.GetComponent<SpriteRenderer>();
            if (floorSprite == null)
            {
                Debug.LogWarning($"Given {nameof(floorPrefab)} doesn't have a SpriteRenderer");
            }
            else
            {
                floorSprite.sortingOrder = -1;
                floorSprite.drawMode = SpriteDrawMode.Tiled;
                floorSprite.tileMode = SpriteTileMode.Continuous;
                floorSprite.size = new Vector2(data.width, data.height);
            }
            
            for (var i = 0; i < data.height; i++)
            {
                for (var j = 0; j < data.width; j++)
                {
                    var c = data.Get(i, j);
                    var pos = GridToWorld(data, i, j);
                    switch (c)
                    {
                        case 'E':
                        {
                            SpawnVisibleInMask(
                                exitPrefab,
                                pos,
                                new [] { MaskType.Glasses, MaskType.Reveal, MaskType.Ghost });
                            break;
                        }
                        case '#': // a standard wall -> block everything
                        {
                            SpawnVisibleInMask(
                                wallPrefab, 
                                pos, 
                                new [] { MaskType.Glasses, MaskType.Reveal }); 
                            break;
                        }
                        case 'H':
                        {
                            SpawnVisibleInMask(
                                hiddenWallPrefab != null ? hiddenWallPrefab :  wallPrefab, 
                                pos, 
                                new [] { MaskType.Reveal });
                            break;
                        }
                        case 'S':
                        {
                            SpawnVisibleInMask(
                                secretPrefab != null ? secretPrefab :  wallPrefab,
                                pos,
                                new [] { MaskType.Glasses });
                            break;
                        }
                        case 'G':
                        {
                            SpawnVisibleInMask(
                                ghostPrefab,
                                pos,
                                new [] { MaskType.Ghost });
                            break;
                        }
                    }
                }
            }

            if (playerPrefab != null && LevelRuntime.Instance != null)
            {
                var start = LevelRuntime.Instance.PlayerStart;
                Instantiate(playerPrefab, GridToWorld(data, start.x, start.y), Quaternion.identity, worldRoot);
            }
        }

        private void SpawnVisibleInMask(GameObject prefab, Vector3 pos, MaskType[] masks)
        {
            if (prefab == null)
            {
                Debug.LogError($"LevelSpawner: given prefab to spawn is null.");
                return;
            }
            
            var go = Instantiate(prefab, pos, Quaternion.identity, worldRoot);
            var vis = go.GetComponent<MaskVisibility>();
            if (vis == null)
            {
                vis = go.AddComponent<MaskVisibility>();
            }
            vis.visibleIn = masks;
        }

        private Vector3 GridToWorld(LevelData data, int x, int y)
        {
            var worldY = data.height - (y + 1);
            return new Vector3(x * data.tileSize, worldY * data.tileSize, 0f);
        }
    }
}