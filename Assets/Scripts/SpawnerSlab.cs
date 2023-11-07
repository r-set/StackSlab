using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnerSlab : MonoBehaviour
{
    [SerializeField] private MovingSlab movingSlab;

    [SerializeField] private MoveDirection moveDirection;

    public void SpawnSlab()
    {
        var cube = Instantiate(movingSlab);

        if (MovingSlab.LastSlab != null && MovingSlab.LastSlab.gameObject != GameObject.Find("Start"))
        {
            float newYPosition = MovingSlab.LastSlab.transform.position.y
                                 + MovingSlab.LastSlab.transform.localScale.y / 2
                                 + cube.transform.localScale.y / 2;

            cube.transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
        }
        else
        {
            cube.transform.position = transform.position;
        }

        cube.MoveDirection = moveDirection;

    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, movingSlab.transform.localScale);
    }
}
