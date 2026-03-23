using UnityEngine;

public class DoorKeyManager : MonoBehaviour
{
    public string Type; // Door or Key

    public bool KeyGot;

    public TrapManager DoorTrapManager;
    public SpriteRenderer Sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        KeyGot = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
