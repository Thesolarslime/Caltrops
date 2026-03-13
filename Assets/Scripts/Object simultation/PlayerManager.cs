using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private ObjectStats Stats;
    private ObjectMovement Movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stats = GetComponent<ObjectStats>();
        Movement = GetComponent<ObjectMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckNeighbouringTiles();
    }

    public void CheckNeighbouringTiles()
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
