using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PulpitManager : MonoBehaviour
{
    [SerializeField] GameObject pulpitPrefab;

    Vector2Int currentGridPos = Vector2Int.zero;
    GameObject currentPulpit;

    float minDestroyTime;
    float maxDestroyTime;
    float spawnDelay;

    const float CELL_SIZE = 9f;

    void Start()
    {
        minDestroyTime = GameConfigLoader.Config.pulpit_data.min_pulpit_destroy_time;
        maxDestroyTime = GameConfigLoader.Config.pulpit_data.max_pulpit_destroy_time;
        spawnDelay     = GameConfigLoader.Config.pulpit_data.pulpit_spawn_time;

        SpawnFirstPulpit();
    }

    void SpawnFirstPulpit()
    {
        currentGridPos = Vector2Int.zero;

        currentPulpit = Instantiate(
            pulpitPrefab,
            GridToWorld(currentGridPos),
            Quaternion.identity
        );

        SetLife(currentPulpit);

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Wait for game to start
            if (GameManager.Instance != null)
            {
                while (GameManager.Instance.currentState != GameManager.GameState.Playing)
                {
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnDelay);

            // Only spawn if game is still playing
            if (GameManager.Instance != null && GameManager.Instance.currentState != GameManager.GameState.Playing)
                continue;

            Vector2Int nextPos = GetRandomNeighbour(currentGridPos);

            GameObject newPulpit = Instantiate(
                pulpitPrefab,
                GridToWorld(nextPos),
                Quaternion.identity
            );

            SetLife(newPulpit);

            currentGridPos = nextPos;
            currentPulpit = newPulpit;
        }
    }

    void SetLife(GameObject pulpit)
    {
        float life = Random.Range(minDestroyTime, maxDestroyTime);

        if (pulpit.TryGetComponent(out Pulpit p))
        {
            p.SetLife(life);
        }
    }

Vector2Int GetRandomNeighbour(Vector2Int pos)
{
    // Only valid neighbours in 2D grid
    List<Vector2Int> neighbours = new List<Vector2Int>()
    {
        pos + Vector2Int.up,
        pos + Vector2Int.down,
        pos + Vector2Int.left,
        pos + Vector2Int.right
    };

    return neighbours[Random.Range(0, neighbours.Count)];
}


    Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * CELL_SIZE, 0f, gridPos.y * CELL_SIZE);
    }
}
