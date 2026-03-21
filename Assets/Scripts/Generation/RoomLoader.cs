using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    public GameObject[] PossibleRooms;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(PossibleRooms[Random.Range(0, PossibleRooms.Length)], gameObject.transform);
    }
}
