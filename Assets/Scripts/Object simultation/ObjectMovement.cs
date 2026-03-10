using System.Collections;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public bool IsControlledByPlayer;
    public bool IsPlayer;
    public bool MovementOnCooldown;
    private bool Moving; // true while the move animation is happening
    public float BaseMovementCooldown = 1.1f; // the base time to wait at a speed of 5 before the player can move again
    public float SpeedIncrementOnMovementCooldown = 0.2f; // the amount a difference of 1 in the speed stat alters the cooldown

    private int EnemyPathCount = 0;
    public string[] EnemyPath; // an array with instructions the enemy executes in order one every time it can move.
    // UP, DOWN, LEFT, RIGHT, FORWARD, BACK, FLEFT, FRIGHT, WAIT

    public ObjectStats Stats;

    private string[] Direction = { "UP", "RIGHT", "DOWN", "LEFT", "UP", "RIGHT", "DOWN", "LEFT", "UP", "RIGHT", "DOWN", "LEFT" };
    private Vector2[] DirectionVectors = { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0) };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stats = GetComponent<ObjectStats>();
        Stats.XPos = (int)transform.position.x;
        Stats.YPos = (int)transform.position.y;
        if (Stats.Type == "Enemy") { EnemyMovement(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsControlledByPlayer) { PlayerControlsMovement(); }
    }

    public void MoveObject(string Direction, int Distance)
    {
        RaycastHit2D Hit = Physics2D.Raycast(new Vector2(Stats.XPos, Stats.YPos) + (DirectionVectors[Stats.GetFacingDirection(Direction)] * Distance), DirectionVectors[Stats.GetFacingDirection(Direction)], 0.45f);
        bool ShouldMove = true;

        if (Hit.collider != null)
        {
            if (Hit.collider.GetComponent<ObjectStats>() != null)
            {
                ObjectStats HitStats = Hit.collider.GetComponent<ObjectStats>();
                switch (HitStats.Type)
                {
                    case "Wall":
                        StartCoroutine(MoveBump(Direction, Distance)); ShouldMove = false; break;
                    case "Enemy":
                        StartCoroutine(MoveBump(Direction, Distance)); ShouldMove = false; break;
                    case "Player":
                        StartCoroutine(MoveBump(Direction, Distance)); ShouldMove = false;
                        if (Stats.Type == "Enemy") { HitStats.TakeDamage(Stats.EnemyMeleeDamage); }
                        break;
                    case "Trap":
                        StartCoroutine(Movement(Direction, Distance)); ShouldMove = false;
                        if (Stats.Type == "Player") { Hit.collider.GetComponent<TrapManager>().TriggerTrap(true, Stats); }
                        else { Hit.collider.GetComponent<TrapManager>().TriggerTrap(false, Stats); }
                        break;

                }
            }
        }
        if (ShouldMove) { StartCoroutine(Movement(Direction, Distance)); }
    }

    private IEnumerator Movement(string Direction, int Distance)
    {
        if (!Moving)
        {
            Moving = true;
            Stats.Facing = Direction;
            //Debug.Log("Time to move" +  Direction + " by " + Distance);
            if (Stats.Type != "Player")
            {
                switch (Direction)
                {
                    case "UP":
                        transform.position = new Vector3(Stats.XPos, Stats.YPos + Distance * 0.1f, 0);
                        break;
                    case "DOWN":
                        transform.position = new Vector3(Stats.XPos, Stats.YPos - Distance * 0.1f, 0);
                        break;
                    case "LEFT":
                        transform.position = new Vector3(Stats.XPos - Distance * 0.1f, Stats.YPos, 0);
                        break;
                    case "RIGHT":
                        transform.position = new Vector3(Stats.XPos + Distance * 0.1f, Stats.YPos, 0);
                        break;
                }
                yield return new WaitForSeconds(0.05f);
            }
            switch (Direction)
            {
                case "UP":
                    transform.position = new Vector3(Stats.XPos, Stats.YPos + Distance * 0.8f, 0);
                    break;
                case "DOWN":
                    transform.position = new Vector3(Stats.XPos, Stats.YPos - Distance * 0.8f, 0);
                    break;
                case "LEFT":
                    transform.position = new Vector3(Stats.XPos - Distance * 0.8f, Stats.YPos, 0);
                    break;
                case "RIGHT":
                    transform.position = new Vector3(Stats.XPos + Distance * 0.8f, Stats.YPos, 0);
                    break;
            }
            yield return new WaitForSeconds(0.03f);
            switch (Direction)
            {
                case "UP":
                    transform.position = new Vector3(Stats.XPos, Stats.YPos + Distance * 0.92f, 0);
                    break;
                case "DOWN":
                    transform.position = new Vector3(Stats.XPos, Stats.YPos - Distance * 0.92f, 0);
                    break;
                case "LEFT":
                    transform.position = new Vector3(Stats.XPos - Distance * 0.92f, Stats.YPos, 0);
                    break;
                case "RIGHT":
                    transform.position = new Vector3(Stats.XPos + Distance * 0.92f, Stats.YPos, 0);
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
            if (Stats.Type == "Player") { Stats.Regen(); }
            Moving = false;
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator MoveBump(string Direction, int Distance) // The animation for bumping into a wall or an enemy attacking in melee.
    {
        if (!Moving)
        {
            Moving = true;
            Stats.Facing = Direction;
            switch (Direction)
            {
                case "UP":
                    transform.position = new Vector3(Stats.XPos, Stats.YPos + Distance * 0.2f, 0);
                    break;
                case "DOWN":
                    transform.position = new Vector3(Stats.XPos, Stats.YPos - Distance * 0.2f, 0);
                    break;
                case "LEFT":
                    transform.position = new Vector3(Stats.XPos - Distance * 0.2f, Stats.YPos, 0);
                    break;
                case "RIGHT":
                    transform.position = new Vector3(Stats.XPos + Distance * 0.2f, Stats.YPos, 0);
                    break;
            }
            yield return new WaitForSeconds(0.02f);
            switch (Direction)
            {
                case "UP":
                    transform.position = new Vector3(Stats.XPos, Stats.YPos + Distance * 0.1f, 0);
                    break;
                case "DOWN":
                    transform.position = new Vector3(Stats.XPos, Stats.YPos - Distance * 0.1f, 0);
                    break;
                case "LEFT":
                    transform.position = new Vector3(Stats.XPos - Distance * 0.1f, Stats.YPos, 0);
                    break;
                case "RIGHT":
                    transform.position = new Vector3(Stats.XPos + Distance * 0.1f, Stats.YPos, 0);
                    break;
            }
            yield return new WaitForSeconds(0.02f);
            transform.position = new Vector3(Stats.XPos, Stats.YPos, 0);

            Stats.XPos = (int)transform.position.x;
            Stats.YPos = (int)transform.position.y;
            Moving = false;
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void PlayerControlsMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (!MovementOnCooldown)
            {
                MovementOnCooldown = true;
                MoveObject("LEFT", 1);
                StartCoroutine(MovementCooldown(BaseMovementCooldown - ((Stats.Speed + Stats.SpeedModifier - 5) * SpeedIncrementOnMovementCooldown)));
            }
            if (!Moving) { transform.position = new Vector3(Stats.XPos - 0.1f, Stats.YPos, 0); }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!MovementOnCooldown)
            {
                MovementOnCooldown = true;
                MoveObject("RIGHT", 1);
                StartCoroutine(MovementCooldown(BaseMovementCooldown - ((Stats.Speed + Stats.SpeedModifier - 5) * SpeedIncrementOnMovementCooldown)));
            }
            if (!Moving) { transform.position = new Vector3(Stats.XPos + 0.1f, Stats.YPos, 0); }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (!MovementOnCooldown)
            {
                MovementOnCooldown = true;
                MoveObject("UP", 1);
                StartCoroutine(MovementCooldown(BaseMovementCooldown - ((Stats.Speed + Stats.SpeedModifier - 5) * SpeedIncrementOnMovementCooldown)));
            }
            if (!Moving) { transform.position = new Vector3(Stats.XPos, Stats.YPos + 0.1f, 0); }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (!MovementOnCooldown)
            {
                MovementOnCooldown = true;
                MoveObject("DOWN", 1);
                StartCoroutine(MovementCooldown(BaseMovementCooldown - ((Stats.Speed + Stats.SpeedModifier - 5) * SpeedIncrementOnMovementCooldown)));
            }
            if (!Moving) { transform.position = new Vector3(Stats.XPos, Stats.YPos - 0.1f, 0); }
        }
        else if (MovementOnCooldown && !Moving)
        {
            transform.position = new Vector3(Stats.XPos, Stats.YPos, 0);
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
        if (!MovementOnCooldown)
        {
            StartCoroutine(EnemyActions(EnemyPath[EnemyPathCount]));
            EnemyPathCount++;
            if (EnemyPathCount >= EnemyPath.Length) { EnemyPathCount = 0; }
        }
    }

    private IEnumerator EnemyActions(string Act)
    {
        switch (Act)
        {
            case "LEFT":
                MoveObject("LEFT", 1);
                break;
            case "RIGHT":
                MoveObject("RIGHT", 1);
                break;
            case "UP":
                MoveObject("UP", 1);
                break;
            case "DOWN":
                MoveObject("DOWN", 1);
                break;
            case "FORWARD":
                MoveObject(Stats.Facing, 1);
                break;
            case "BACK":
                MoveObject(Stats.Facing, -1);
                break;
            case "WAIT":
                break;
        }
        yield return new WaitForSeconds(BaseMovementCooldown - ((Stats.Speed + Stats.SpeedModifier - 5) * SpeedIncrementOnMovementCooldown));
        EnemyMovement();
    }
}
