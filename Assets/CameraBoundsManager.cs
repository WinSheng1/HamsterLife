using UnityEngine;
using Unity.Cinemachine;

public class CameraBoundsManager : MonoBehaviour
{
    [System.Serializable]
    public struct RoomBoundary {
        public RoomId roomId;
        public PolygonCollider2D collider;
    }

    [SerializeField] RoomBoundary[] boundaries;
    private CinemachineConfiner2D confiner;

    void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    public void SetRoom(RoomId roomId)
    {
        for (int i = 0; i < boundaries.Length; i++)
        {
            if (boundaries[i].roomId == roomId)
            {
                confiner.BoundingShape2D = boundaries[i].collider;
                return;
            }
        }
        Debug.LogWarning("RoomId not found: " + roomId);
    }

    public RoomId GetCurrentRoom()
    {
        for (int i = 0; i < boundaries.Length; i++)
        {
            if (confiner.BoundingShape2D == boundaries[i].collider)
            {
                return boundaries[i].roomId;
            }
        }
        Debug.LogWarning("Current bounding shape does not match any room.");
        return default(RoomId);
    }
}
