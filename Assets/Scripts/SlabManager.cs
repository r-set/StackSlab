using UnityEngine;

public class SlabManager : MonoBehaviour
{
    [SerializeField] private SpawnerSlab spawnerZ;
    [SerializeField] private SpawnerSlab spawnerX;
    public bool spawnZNext = true;

    public void SpawnNextSlab()
    {
        if (spawnZNext)
        {
            spawnerZ.SpawnSlab();
        }
        else
        {
            spawnerX.SpawnSlab();
        }

        spawnZNext = !spawnZNext;
    }

}