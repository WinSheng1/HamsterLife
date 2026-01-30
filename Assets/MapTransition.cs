using Unity.Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D polygonCollider;
    CinemachineConfiner2D confiner;
    [SerializeField] Direction direction;
    [SerializeField] float transitionOffset;
    private enum Direction { Up, Down, Left, Right }

    private void Awake()
    {

        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confiner.BoundingShape2D = polygonCollider;
            UpdatePlayerPos(collision.gameObject);
        }   
    }

    private void UpdatePlayerPos(GameObject player)
    {
        Vector3 playerPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                playerPos.y += transitionOffset;
                break;
            case Direction.Down:
                playerPos.y += -transitionOffset;
                break;
            case Direction.Left:
                playerPos.x += -transitionOffset;
                break;
            case Direction.Right:
                playerPos.x += transitionOffset;
                break;
        }
        player.transform.position = playerPos;
    }
}
