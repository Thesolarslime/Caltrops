using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    private ObjectStats Stats;
    private ObjectMovement Movement;
    private SpriteRenderer Sprite;
    public CaltropType Caltrop;

    public Sprite[] TrapSprites;
    private Animator TrapAnimator;

    public float TrapDelay; // if a trap waits for a bit before doing something, this is how long in seconds
    public int TrapDamage; // how much damage a trap does when it hits
    public int TrapDurability; // how many times this trap can activate before it dies
    public bool OnlyPlayerCanTrigger;
    public bool PlayerCantTrigger; // if true the trap won't do anything if it was triggered by the player
    public bool TrapHasAnimation; // if true this script will trigger an animation when trap is triggered

    public ObjectStats MostRecentTriggerer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stats = GetComponent<ObjectStats>();
        Movement = GetComponent<ObjectMovement>();
        Sprite = GetComponent<SpriteRenderer>();

        if (TrapHasAnimation) { TrapAnimator = GetComponent<Animator>(); }

        if (Stats.Name == "Caltrop")
        {
            TrapDelay = 0.1f;
            TrapDamage = Caltrop.Damage;
            TrapDurability = Caltrop.Durability;
            PlayerCantTrigger = Caltrop.PlayerImmune;
        }
        else
        {
            TrapDurability = 100000;
            PlayerCantTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerTrap(bool PlayerTriggered, ObjectStats Triggerer)
    {
        if (TrapHasAnimation) { TrapAnimator.SetTrigger("TrapTrigger"); }

        if (PlayerTriggered || (!OnlyPlayerCanTrigger))
        {
            if (!PlayerCantTrigger || !PlayerTriggered)
            {
                StartCoroutine(TrapAction(Triggerer));
            }  
        }
    }

    private IEnumerator TrapAction(ObjectStats Triggerer)
    {
        yield return new WaitForSeconds(TrapDelay);

        switch (Stats.Name)
        {
            case "Caltrop":
                switch (Caltrop.Name)
                {
                    case "Mundane":
                        Triggerer.TakeDamage(TrapDamage);
                        break;
                }
                TrapDurability -= 1;
                if (TrapDurability <= 0) { Stats.StartCoroutine(Stats.Die()); }
                break;
            case "Spike trap":
                Sprite.sprite = TrapSprites[1];
                if (MostRecentTriggerer.XPos == Stats.XPos && MostRecentTriggerer.YPos == Stats.YPos)
                {
                    MostRecentTriggerer.TakeDamage(TrapDamage);
                }
                yield return new WaitForSeconds(0.1f);
                Sprite.sprite = TrapSprites[2];
                yield return new WaitForSeconds(0.05f);
                Sprite.sprite = TrapSprites[0];
                break;
        }
    }
}
