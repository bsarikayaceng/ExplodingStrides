using UnityEngine;


public enum RabbitState
{
    Idle,
    Move,
    Dead
}

public class TestTouch : MonoBehaviour
{
    public static TestTouch Instance { get; private set; }

    private InputControl inputControl;
    private Camera cameraMain;
    public Tile targetTile;

    private static RabbitState rabbitState;
    

    private void Awake()
    {
        inputControl = InputControl.Instance;
        cameraMain = Camera.main;
    }

    private void Start()
    {
        rabbitState = RabbitState.Idle;
    }

    private void Update()
    {
        if(targetTile == null) { return; }

        Move(targetTile);
    }

    public void Move(Tile tile)
    {
        targetTile = tile;

        rabbitState = RabbitState.Move;

        var targetDirection = tile.transform.position - transform.position;
        transform.position += targetDirection * Time.deltaTime;
        Debug.Log("<color=cyan> move calisyo</color>");
    }

    public void SetTargetTile(Tile tile)
    {
        targetTile = tile;
    }

    public static void SetTargetPosition(Vector3 position)
    {
        _ = new Vector3(Tile.Instance.x, Tile.Instance.y, 0);
        rabbitState = RabbitState.Move;
    }
}
