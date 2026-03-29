using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public bool MenuActive;
    public Animator MenuAnimator;
    public AudioPlayer Audio;

    public LevelUpChoiceBox[] ChoiceBoxes;
    public SpriteRenderer[] ChoiceSprites;
    public int SelectedBox;

    public TextMeshProUGUI Description;
    public TextMeshProUGUI Title;

    public int LevelUpPhase; // 0 = intro anim, 1 = bonus choice, 2 = caltrop choice, 3 = replace choice
    private bool SelectionMade; // becomes true to signal that a choice has been made and it's time to move on with the levelup sequence.
    private bool TimeToSelect;

    public CaltropType[] PossibleCaltrops;
    public LevelBonus[] PossibleBonuses;

    public GameManager GameManager;
    public Camera MainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MenuAnimator = GetComponent<Animator>();
        Audio = GetComponent<AudioPlayer>();
        TimeToSelect = false;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuActive && TimeToSelect) { Selection(); }
    }

    public void PickOptions(int phase)
    {
        if (phase == 1)
        {
            List<int> Bonuses = new List<int>();
            Bonuses.Add(0); Bonuses.Add(1); Bonuses.Add(2); Bonuses.Add(3); Bonuses.Add(4); Bonuses.Add(5);

            // a really unoptimised but simple way of getting 3 random bonuses
            int ChosenBonus = Random.Range(0, Bonuses.Count);
            Debug.Log(ChosenBonus + PossibleBonuses[ChosenBonus].Name);
            ChoiceBoxes[0].Bonus = PossibleBonuses[ChosenBonus];
            ChoiceSprites[0].sprite = PossibleBonuses[ChosenBonus].Icon;
            Bonuses.RemoveAt(ChosenBonus);
            ChosenBonus = Random.Range(0, Bonuses.Count);
            Debug.Log(ChosenBonus + PossibleBonuses[ChosenBonus].Name);
            ChoiceBoxes[1].Bonus = PossibleBonuses[ChosenBonus];
            ChoiceSprites[1].sprite = PossibleBonuses[ChosenBonus].Icon;
            Bonuses.RemoveAt(ChosenBonus);
            ChosenBonus = Random.Range(0, Bonuses.Count);
            Debug.Log(ChosenBonus + PossibleBonuses[ChosenBonus].Name);
            ChoiceBoxes[2].Bonus = PossibleBonuses[ChosenBonus];
            ChoiceSprites[2].sprite = PossibleBonuses[ChosenBonus].Icon;
            Bonuses.RemoveAt(ChosenBonus);
        }
        if (phase == 2)
        {
            List<CaltropType> TempCaltrops;
            TempCaltrops = PossibleCaltrops.ToList();

            // a really unoptimised but simple way of getting 3 random bonuses
            int ChosenBonus = Random.Range(0, TempCaltrops.Count);
            ChoiceBoxes[0].Caltrop = TempCaltrops[ChosenBonus];
            ChoiceSprites[0].sprite = TempCaltrops[ChosenBonus].Icon;
            TempCaltrops.RemoveAt(ChosenBonus);
            ChosenBonus = Random.Range(0, TempCaltrops.Count);
            ChoiceBoxes[1].Caltrop = TempCaltrops[ChosenBonus];
            ChoiceSprites[1].sprite = TempCaltrops[ChosenBonus].Icon;
            TempCaltrops.RemoveAt(ChosenBonus);
            ChosenBonus = Random.Range(0, TempCaltrops.Count);
            ChoiceBoxes[2].Caltrop = TempCaltrops[ChosenBonus];
            ChoiceSprites[2].sprite = TempCaltrops[ChosenBonus].Icon;
            TempCaltrops.RemoveAt(ChosenBonus);
        }
        if (phase == 3)
        {

        }
    }

    public IEnumerator LevelUpSequence()
    {
        MainCamera = FindAnyObjectByType<Camera>();
        LevelUpPhase = 0;
        MenuActive = true;
        MenuAnimator.SetBool("MenuUp", true);
        this.gameObject.SetActive(true);
        SelectedBox = 3;
        Title.text = "LEVEL UP!\nCHOOSE A STAT BOOST";
        yield return new WaitForSeconds(1);
        PickOptions(1);
        SelectedBox = 1;
        LevelUpPhase = 1;
        TimeToSelect = true;
    }

    public IEnumerator LevelUpSequence2()
    {
        ApplyBonus();
        Title.text = "LEVEL UP!";
        SelectionMade = false;
        LevelUpPhase = 2;
        SelectedBox = 3;
        yield return new WaitForSeconds(1);
        Title.text = "LEVEL UP!\nCHOOSE A CALTROP ENCHANTMENT";
        PickOptions(2);
        SelectedBox = 1;
        TimeToSelect = true;
    }
    public IEnumerator LevelUpSequence3()
    {
        ApplyCaltrop();
        SelectedBox = 3;
        yield return new WaitForSeconds(1);
        MenuAnimator.SetBool("MenuUp", false);
        GameManager.Paused = false;
        MenuActive = false;
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);

        /*SelectionMade = false;
        LevelUpPhase = 3;
        yield return new WaitForSeconds(1);
        PickOptions(3);
        SelectedBox = 1;
        TimeToSelect = true;*/
    }

    public void ApplyBonus()
    {
        switch (ChoiceBoxes[SelectedBox].Bonus.Name)
        {
            case "HEALTH":
                GameManager.PlayerStats.MaxHealth++;
                GameManager.MaxHealth++; break;
            case "MANA":
                GameManager.PlayerStats.MaxMana++;
                GameManager.MaxMana++; break;
            case "SPEED":
                GameManager.PlayerStats.Speed++;
                GameManager.Speed++; break;
            case "CAST TIME":
                GameManager.Player.CastTimeModifier -= 0.1f;
                GameManager.CastTimeModifier -= 0.1f; break;
            case "REGEN":
                GameManager.PlayerStats.RegenBase--;
                GameManager.RegenBase--; break;
            case "XP":
                GameManager.GainXP(GameManager.XPRequirements[GameManager.Level-1] / 3); break;
        }
    }

    public void ApplyCaltrop()
    {
        GameManager.Player.CaltropCycle[2] = ChoiceBoxes[SelectedBox].Caltrop;
    }

    private void UpdateDescription()
    {
        if (LevelUpPhase == 1)
        {
            Description.text = ChoiceBoxes[SelectedBox].Bonus.Description;
        }
        if (LevelUpPhase == 2)
        {
            Description.text = ChoiceBoxes[SelectedBox].Caltrop.Description;
        }
    }

    public void Selection()
    {
        if (!SelectionMade)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Audio.PlaySound(Random.Range(0, 2), true, 0.5f);
                if (SelectedBox == 0) { SelectedBox = 2; }
                else { SelectedBox--; }
                UpdateDescription();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Audio.PlaySound(Random.Range(0, 2), true, 0.5f);
                if (SelectedBox == 2) { SelectedBox = 0; }
                else { SelectedBox++; }
                UpdateDescription();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Audio.PlaySound(2, true, 0.5f);
                SelectionMade = true;
                if (LevelUpPhase == 1)
                {
                    StartCoroutine(LevelUpSequence2());
                }
                else if (LevelUpPhase == 2)
                {
                    StartCoroutine(LevelUpSequence3());
                }

            }
        }
        
    }
}
