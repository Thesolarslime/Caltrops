using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TileSelectCheck : MonoBehaviour
{
    public PlayerManager Player;
    public Animator Animator;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D Hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 0.1f);
        if (Hit.collider != null)
        {
            Player.CanSummonCaltrop = false;
            Animator.SetInteger("SummonState", 0);
        }
        else
        {
            if (Player.Stats.Mana == 0)
            {
                Player.CanSummonCaltrop = false;
                Animator.SetInteger("SummonState", 1);
            }
            else
            {
                Player.CanSummonCaltrop = true;
                Animator.SetInteger("SummonState", 2);
            }
        }
    }
}
