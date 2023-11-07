using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MovingSlab : MonoBehaviour
{
    [SerializeField] private float slabMoveSpeed = 1f;

    public static MovingSlab CurrentSlab { get; private set; }
    public static MovingSlab LastSlab { get; private set; }
    public MoveDirection MoveDirection { get; set; }

    private SlabManager slabManager;

    private void OnEnable()
    {
        if (LastSlab == null)
        {
            LastSlab = GameObject.Find("StartSlab").GetComponent<MovingSlab>();
        }

        CurrentSlab = this;

        GetComponent<Renderer>().material.color = GetRandomColor();

        transform.localScale = new Vector3(LastSlab.transform.localScale.x, transform.localScale.y, LastSlab.transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
    }

    internal void StopMoveSlab()
    {
        slabMoveSpeed = 0f;

        float hangover = (MoveDirection == MoveDirection.Up)
        ? transform.position.z - LastSlab.transform.position.z
        : transform.position.x - LastSlab.transform.position.x;

        if (Mathf.Abs(hangover) >= LastSlab.transform.localScale.z)
        {
            LastSlab = null;
            CurrentSlab = null;
            SceneManager.LoadScene("Game");
        }

        float direction = hangover > 0 ? 1f : -1f;

        SplitSpeedOnZ(hangover, direction);

        LastSlab = this;
    }

    private void SplitSpeedOnZ(float hangover, float direction)
    {
        float newZSize = LastSlab.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZPosition = LastSlab.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCubeZ(fallingBlockZPosition, fallingBlockSize);
    }

    private void SplitSpeedOnX(float hangover, float direction)
    {
        float newXSize = LastSlab.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = LastSlab.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.x);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCubeX(fallingBlockXPosition, fallingBlockSize);
    }

    private void SpawnDropCubeZ(float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

        Destroy(cube.gameObject, 1f);
    }

    private void SpawnDropCubeX(float fallingBlockXPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
        cube.transform.position = new Vector3(fallingBlockXPosition, transform.position.y, transform.position.z);

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

        Destroy(cube.gameObject, 1f);
    }

    private void Update()
    {
        if (MoveDirection == MoveDirection.Up)
        {
            transform.position += transform.forward * slabMoveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.right * slabMoveSpeed * Time.deltaTime;
        }
    }
}
