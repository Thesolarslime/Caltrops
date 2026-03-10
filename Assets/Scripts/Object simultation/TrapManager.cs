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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stats = GetComponent<ObjectStats>();
        Movement = GetComponent<ObjectMovement>();
        TrapAnimator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerTrap(bool PlayerTriggered, ObjectStats Triggerer)
    {
        TrapAnimator.SetTrigger("TrapTrigger");

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
                if (Triggerer.XPos == Stats.XPos && Triggerer.YPos == Stats.YPos)
                {
                    Triggerer.TakeDamage(TrapDamage);
                }
                yield return new WaitForSeconds(0.1f);
                Sprite.sprite = TrapSprites[2];
                yield return new WaitForSeconds(0.05f);
                Sprite.sprite = TrapSprites[0];
                break;
        }
    }
}
