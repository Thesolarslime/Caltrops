using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public bool MenuActive;
    public Animator MenuAnimator;

    public LevelUpChoiceBox[] ChoiceBoxes;
    public SpriteRenderer[] ChoiceSprites;
    public int SelectedBox;

    public int LevelUpPhase; // 0 = intro anim, 1 = bonus choice, 2 = caltrop choice, 3 = replace choice
    private bool SelectionMade; // becomes true to signal that a choice has been made and it's time to move on with the levelup sequence.

    public CaltropType[] PossibleCaltrops;
    public LevelBonus[] PossibleBonuses;

    public GameManager GameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MenuAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GoToPlayer();
    }

    public void GoToPlayer()
    {
        if (GameManager.HasPlayer)
        {
            transform.position = GameManager.Player.gameObject.transform.position;
        }
    }

    public void PickOptions(int phase)
    {
        if (phase == 1)
        {
            List<int> Bonuses = new List<int>();
            Bonuses.Add(0); Bonuses.Add(1); Bonuses.Add(2); Bonuses.Add(3); Bonuses.Add(4); Bonuses.Add(5);

            // a really unoptimised but simple way of getting 3 random bonuses
            int ChosenBonus = Random.Range(0, Bonuses.Count);
            ChoiceBoxes[0].Bonus = PossibleBonuses[ChosenBonus];
            Bonuses.RemoveAt(ChosenBonus);
            ChosenBonus = Random.Range(0, Bonuses.Count);
            ChoiceBoxes[1].Bonus = PossibleBonuses[ChosenBonus];
            Bonuses.RemoveAt(ChosenBonus);
            ChosenBonus = Random.Range(0, Bonuses.Count);
            ChoiceBoxes[2].Bonus = PossibleBonuses[ChosenBonus];
            Bonuses.RemoveAt(ChosenBonus);
        }
        
    }

    public IEnumerator LevelUpSequence()
    {
        LevelUpPhase = 0;
        MenuActive = true;
        MenuAnimator.SetBool("MenuUp", true);
        this.gameObject.SetActive(true);
        SelectedBox = 3;
        yield return new WaitForSeconds(1);
        PickOptions(1);
        SelectedBox = 1;
        LevelUpPhase = 1;
        Selection();
    }

    public void Selection()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (SelectedBox == 0) { SelectedBox = 2; }
            else { SelectedBox--; }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (SelectedBox == 2) { SelectedBox = 0; }
            else { SelectedBox++; }
        }

        if (MenuActive)
        {
            Selection();
        }
    }
}
