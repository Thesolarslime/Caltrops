using UnityEngine;

public class TrapManager : MonoBehaviour
{
    private ObjectStats Stats;
    private ObjectMovement Movement;

    public Sprite[] TrapSprites;

    public float TrapDelay; // if a trap waits for a bit before doing something, this is how long in seconds
    public int TrapDamage; // how much damage a trap does when it hits

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stats = GetComponent<ObjectStats>();
        Movement = GetComponent<ObjectMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerTrap()
    {

    }
}
