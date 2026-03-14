using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    private ObjectStats Stats;
    private ObjectMovement Movement;
    private SpriteRenderer Sprite;

    public Sprite[] TrapSprites;
    private Animator TrapAnimator;

    public float TrapDelay; // if a trap waits for a bit before doing something, this is how long in seconds
    public int TrapDamage; // how much damage a trap does when it hits
    public bool OnlyPlayerCanTrigger;
    public bool TrapHasAnimation; // if true this script will trigger an animation when trap is triggered

    public ObjectStats MostRecentTriggerer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stats = GetComponent<ObjectStats>();
        Movement = GetComponent<ObjectMovement>();
        Sprite = GetComponent<SpriteRenderer>();

        if (TrapHasAnimation) { TrapAnimator = GetComponent<Animator>(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerTrap(bool PlayerTriggered, ObjectStats Triggerer)
    {
        if (TrapHasAnimation) { TrapAnimator.SetTrigger("TrapTrigger"); }

        if (PlayerTriggered || (!OnlyPlayerCanTrigger) )
        {
            StartCoroutine(TrapAction(Triggerer));
        }
    }

    private IEnumerator TrapAction(ObjectStats Triggerer)
    {
        yield return new WaitForSeconds(TrapDelay);

        switch (Stats.Name)
        {
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
