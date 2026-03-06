using System.Collections;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public bool IsControlledByPlayer;
    public bool IsPlayer;
    public bool MovementOnCooldown;
    public float BaseMovementCooldown = 1.1f; // the base time to wait at a speed of 5 before the player can move again
    public float SpeedIncrementOnMovementCooldown = 0.2f; // the amount a difference of 1 in the speed stat alters the cooldown

    public string Facing;

    public ObjectStats Stats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stats = GetComponent<ObjectStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsControlledByPlayer) { PlayerControlsMovement(); }
        else { EnemyMovement(); }
    }

    public void MoveObject(string Direction, int Distance)
    {
        Stats.XPos = (int)transform.position.x;
        Stats.YPos = (int)transform.position.y;
        StartCoroutine(Movement(Direction, Distance));
    }

    private IEnumerator Movement(string Direction, int Distance)
    {
        switch (Direction)
        {
            case "UP":
                transform.position = new Vector3(Stats.XPos, Stats.YPos + 0.1f, 0);
                break;
            case "DOWN":
                transform.position = new Vector3(Stats.XPos, Stats.YPos - 0.1f, 0);
                break;
            case "LEFT":
                transform.position = new Vector3(Stats.XPos - 0.1f, Stats.YPos, 0);
                break;
            case "RIGHT":
                transform.position = new Vector3(Stats.XPos + 0.1f, Stats.YPos, 0);
                break;
        }
        yield return new WaitForSeconds(0.02f);
        switch (Direction)
        {
            case "UP":
                transform.position = new Vector3(Stats.XPos, Stats.YPos + Distance * 0.7f, 0);
                break;
            case "DOWN":
                transform.position = new Vector3(Stats.XPos, Stats.YPos - Distance * 0.7f, 0);
                break;
            case "LEFT":
                transform.position = new Vector3(Stats.XPos - Distance * 0.7f, Stats.YPos, 0);
                break;
            case "RIGHT":
                transform.position = new Vector3(Stats.XPos + Distance * 0.7f, Stats.YPos, 0);
                break;
        }
        yield return new WaitForSeconds(0.03f);
        switch (Direction)
        {
            case "UP":
                transform.position = new Vector3(Stats.XPos, Stats.YPos + Distance * 0.9f, 0);
                break;
            case "DOWN":
                transform.position = new Vector3(Stats.XPos, Stats.YPos - Distance * 0.9f, 0);
                break;
            case "LEFT":
                transform.position = new Vector3(Stats.XPos - Distance * 0.9f, Stats.YPos, 0);
                break;
            case "RIGHT":
                transform.position = new Vector3(Stats.XPos + Distance * 0.9f, Stats.YPos, 0);
                break;
        }
        yield return new WaitForSeconds(0.04f);
        switch (Direction)
        {
            case "UP":
                transform.position = new Vector3(Stats.XPos, Stats.YPos + Distance, 0);
                break;
            case "DOWN":
                transform.position = new Vector3(Stats.XPos, Stats.YPos - Distance, 0);
                break;
            case "LEFT":
                transform.position = new Vector3(Stats.XPos - Distance, Stats.YPos, 0);
                break;
            case "RIGHT":
                transform.position = new Vector3(Stats.XPos + Distance, Stats.YPos, 0);
                break;
        }
        Stats.XPos = (int)transform.position.x;
        Stats.YPos = (int)transform.position.y;
    }

    private void PlayerControlsMovement()
    {
        if (Input.GetKey(KeyCode.A) && !MovementOnCooldown)
        {
            MoveObject("LEFT", 1);
            StartCoroutine(MovementCooldown(BaseMovementCooldown - ((Stats.Speed + Stats.SpeedModifier - 5) * SpeedIncrementOnMovementCooldown)));
        }
        if (Input.GetKey(KeyCode.D) && !MovementOnCooldown)
        {
            MoveObject("RIGHT", 1);
            StartCoroutine(MovementCooldown(BaseMovementCooldown - ((Stats.Speed + Stats.SpeedModifier - 5) * SpeedIncrementOnMovementCooldown)));
        }
    }

    private IEnumerator MovementCooldown(float Time)
    {
        MovementOnCooldown = true;
        yield return new WaitForSeconds(Time);
        MovementOnCooldown = false;
    }

    private void EnemyMovement()
    {

    }
}
