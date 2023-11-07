using UnityEngine;

public class StopSlab : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (MovingSlab.CurrentSlab != null)
            {
                MovingSlab.CurrentSlab.StopMoveSlab();

                // Call OnSlabStopped on the SpawnerSlab to allow the next spawn
                FindObjectOfType<SpawnerSlab>().SpawnSlab();
            }
            // Removed the call to SpawnSlab to prevent immediate spawning
        }
    }
}
