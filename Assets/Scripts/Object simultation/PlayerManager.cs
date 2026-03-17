using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private ObjectStats Stats;
    private ObjectMovement Movement;

    public GameObject TileSelect;
    public GameObject TileSelectMask;

    public int SelectedCaltrop;
    public CaltropType[] CaltropCycle;
    private bool CastAmountCanIncrease;
    public float CastTimeFulfilled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stats = GetComponent<ObjectStats>();
        Movement = GetComponent<ObjectMovement>();

        CastAmountCanIncrease = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckNeighbouringTiles();
        CaltropCasting();
        SetSummonTileState();
    }

    public void CaltropCasting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (CastAmountCanIncrease)
            {
                StartCoroutine(CaltropCastTime());
            }
        }
        else
        {
            CastTimeFulfilled = 0;
        }
    }

    public void SetSummonTileState()
    {
        switch (CaltropCycle[SelectedCaltrop].DirectionPlaced)
        {
            case "Up":
                TileSelect.transform.position = new Vector3(Stats.XPos, Stats.YPos + CaltropCycle[SelectedCaltrop].DistancePlaced, 0);
                break;
            case "Down":
                TileSelect.transform.position = new Vector3(Stats.XPos, Stats.YPos - CaltropCycle[SelectedCaltrop].DistancePlaced, 0);
                break;
            case "Left":
                TileSelect.transform.position = new Vector3(Stats.XPos - CaltropCycle[SelectedCaltrop].DistancePlaced, Stats.YPos, 0);
                break;
            case "Right":
                TileSelect.transform.position = new Vector3(Stats.XPos + CaltropCycle[SelectedCaltrop].DistancePlaced, Stats.YPos, 0);
                break;
            case "Forward":
                switch (Stats.Facing)
                {
                    case "UP":
                        TileSelect.transform.position = new Vector3(Stats.XPos, Stats.YPos + CaltropCycle[SelectedCaltrop].DistancePlaced, 0);
                        break;
                    case "DOWN":
                        TileSelect.transform.position = new Vector3(Stats.XPos, Stats.YPos - CaltropCycle[SelectedCaltrop].DistancePlaced, 0);
                        break;
                    case "LEFT":
                        TileSelect.transform.position = new Vector3(Stats.XPos - CaltropCycle[SelectedCaltrop].DistancePlaced, Stats.YPos, 0);
                        break;
                    case "RIGHT":
                        TileSelect.transform.position = new Vector3(Stats.XPos + CaltropCycle[SelectedCaltrop].DistancePlaced, Stats.YPos, 0);
                        break;
                }
                break;
        }
        if (CastTimeFulfilled <= 0)
        {
            TileSelectMask.transform.position = new Vector3(TileSelect.transform.position.x, TileSelect.transform.position.y, 0);
        }
        else
        {
            TileSelectMask.transform.position = new Vector3(TileSelect.transform.position.x, TileSelect.transform.position.y + (CastTimeFulfilled / CaltropCycle[SelectedCaltrop].CastTime) * 1.22f, 0);
        }
    }

    public IEnumerator CaltropCastTime()
    {
        CastAmountCanIncrease = false;
        CastTimeFulfilled += 0.01f;
        if (CastTimeFulfilled >= CaltropCycle[SelectedCaltrop].CastTime)
        {
            Instantiate(CaltropCycle[SelectedCaltrop].Prefab, new Vector3(TileSelect.transform.position.x, TileSelect.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
            CastTimeFulfilled = 0;
            if (SelectedCaltrop == 2)
            {
                SelectedCaltrop = 0;
            }
            else
            {
                SelectedCaltrop++;
            }
                yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
        }
        CastAmountCanIncrease = true;
    }

    public void CheckNeighbouringTiles() // checks the 4 tiles around and tells the appropriate enemies to attack
    {
        RaycastHit2D Hit = Physics2D.Raycast(new Vector2(Stats.XPos, Stats.YPos) + new Vector2(0, 1), Vector2.up, 0.45f);
        if (Hit.collider != null)
        {
            if (Hit.collider.GetComponent<ObjectStats>() != null && Hit.collider.GetComponent<ObjectMovement>() != null)
            {
                if (Hit.collider.GetComponent<ObjectStats>().EnemyAttacksInMelee && Hit.collider.GetComponent<ObjectMovement>().EnemyMeleeAttacking == false)
                {
                    Hit.collider.GetComponent<ObjectMovement>().InterruptToAttackPlayer("DOWN");
                }
            }
        }
        Hit = Physics2D.Raycast(new Vector2(Stats.XPos, Stats.YPos) + new Vector2(1, 0), Vector2.right, 0.45f);
        if (Hit.collider != null)
        {
            if (Hit.collider.GetComponent<ObjectStats>() != null && Hit.collider.GetComponent<ObjectMovement>() != null)
            {
                if (Hit.collider.GetComponent<ObjectStats>().EnemyAttacksInMelee && Hit.collider.GetComponent<ObjectMovement>().EnemyMeleeAttacking == false)
                {
                    Hit.collider.GetComponent<ObjectMovement>().InterruptToAttackPlayer("LEFT");
                }
            }
        }
        Hit = Physics2D.Raycast(new Vector2(Stats.XPos, Stats.YPos) + new Vector2(0, -1), Vector2.down, 0.45f);
        if (Hit.collider != null)
        {
            if (Hit.collider.GetComponent<ObjectStats>() != null && Hit.collider.GetComponent<ObjectMovement>() != null)
            {
                if (Hit.collider.GetComponent<ObjectStats>().EnemyAttacksInMelee && Hit.collider.GetComponent<ObjectMovement>().EnemyMeleeAttacking == false)
                {
                    Hit.collider.GetComponent<ObjectMovement>().InterruptToAttackPlayer("UP");
                }
            }
        }
        Hit = Physics2D.Raycast(new Vector2(Stats.XPos, Stats.YPos) + new Vector2(-1, 0), Vector2.left, 0.45f);
        if (Hit.collider != null)
        {
            if (Hit.collider.GetComponent<ObjectStats>() != null && Hit.collider.GetComponent<ObjectMovement>() != null)
            {
                if (Hit.collider.GetComponent<ObjectStats>().EnemyAttacksInMelee && Hit.collider.GetComponent<ObjectMovement>().EnemyMeleeAttacking == false)
                {
                    Hit.collider.GetComponent<ObjectMovement>().InterruptToAttackPlayer("RIGHT");
                }
            }
        }
    }
}
