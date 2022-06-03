
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectPlataformer
{
    public class LevelGenerator : MonoBehaviour
    {
        private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 50f;
        [FormerlySerializedAs("levelPart_Start")] [SerializeField] private Transform levelPartStart;
        [FormerlySerializedAs("levelPart_1")] [SerializeField] private Transform levelPart1;
        [SerializeField] private player player;

        private Vector3 _lastEndPosition;

        private void Awake()
        {
            _lastEndPosition = levelPartStart.Find("EndPosition").position;
            SpawnLevelPart();
        }

        private void Update()
        {
            if (Vector3.Distance(player.gameObject.transform.position, _lastEndPosition) <
                PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            {
                SpawnLevelPart();
            }
        }

        private void SpawnLevelPart()
        {
            Transform lastLevelPartTransform = SpawnLevelPart(_lastEndPosition);
            _lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        }

        private Transform SpawnLevelPart(Vector3 spawnPosition)
        {
            Transform levelPartTransform = Instantiate(levelPart1, spawnPosition, Quaternion.identity);
            return levelPartTransform;
        }
    }
}