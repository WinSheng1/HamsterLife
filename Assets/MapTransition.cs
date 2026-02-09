using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private RoomId targetRoom;
    [SerializeField] private Transform targetSpawnPoint;
    private CameraBoundsManager boundsManager;
    private void Awake()
    {
        boundsManager = FindFirstObjectByType<CameraBoundsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boundsManager.SetRoom(targetRoom);
            collision.transform.position = targetSpawnPoint.position;
        }   
    }
}
