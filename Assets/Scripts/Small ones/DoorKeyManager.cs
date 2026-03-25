using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DoorKeyManager : MonoBehaviour
{
    public string Type; // Door or Key

    public bool KeyGot;

    public TrapManager DoorTrapManager;
    public SpriteRenderer Sprite;

    public Sprite DoorOpenSprite;
    public Light2D DoorLight;

    public ObjectStats DoorStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KeyGot = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeyGet()
    {
        if (Type == "Door")
        {
            KeyGot = true;
            Sprite.sprite = DoorOpenSprite;
            DoorStats.Type = "Trap";
            DoorLight.intensity = 2;
        }
        else if (Type == "Key")
        {
            KeyGot = true;
            DoorTrapManager.gameObject.GetComponent<DoorKeyManager>().KeyGet();
        }
    }
}
