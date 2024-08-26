using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    [SerializeField] GameObject ghostPrefab;
    [SerializeField] float spawnRate = 0.1f;

    float spawnTimer = 0f;
    bool emitTrail = false;

    void Update()
    {
        if (emitTrail)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                SpawnGhost();
                spawnTimer = 0f;
            }
        }
    }

    void SpawnGhost()
    {
        GameObject ghost = Instantiate(ghostPrefab, transform.position, transform.rotation);
        ghost.transform.localScale = transform.localScale;
    }

    public void StartEmit()
    {
        emitTrail = true;
    }

    public void StopEmit()
    {
        emitTrail = false;
    }
}
