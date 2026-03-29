using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public int Health;
    public int MaxHealth;
    public int Mana;
    public int MaxMana;
    public float CastTimeModifier;
    public int Speed;
    public int RegenBase;
    public int XP;
    private int XPCarryOver;
    public int Level;
    public int[] XPRequirements;
    public int SelectedCaltrop;
    public CaltropType[] CaltropCycle;
    public PassiveItem[] PassiveItems;
    public string[] PassiveItemNames; // WHEN PLAYER GAINS AN ITEM IT ADDS IT'S NAME TO THIS

    public PlayerManager Player;
    public ObjectStats PlayerStats;
    private ObjectMovement PlayerMovement;

    public bool HasPlayer;
    public string SceneToGoTo;
    public int CurrentLevelID;

    public bool Paused;
    public LevelUpManager LevelUpManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);

        HasPlayer = false;
        Paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (HasPlayer)
        {
            transform.position = Player.gameObject.transform.position;
        }
    }

    public void GetPlayer(PlayerManager Input)
    {
        Player = Input;
        PlayerStats = Player.GetComponent<ObjectStats>();
        PlayerMovement = Player.GetComponent<ObjectMovement>();
        HasPlayer = true;

        if (CurrentLevelID > 1) { GiveStats(); }
    }

    public void GainXP(int Amount)
    {
        XP += Amount; //MAKE THIS BETTER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        if (XP > XPRequirements[Level - 1])
        {
            XPCarryOver = XP - XPRequirements[Level - 1];
            XP = XPRequirements[Level - 1];
        }
        if (XP == XPRequirements[Level - 1])
        {
            StartCoroutine(LevelUp());
        }
    }

    public IEnumerator LevelUp()
    {
        yield return new WaitForSeconds(1);
        Level++;
        Paused = true;
        LevelUpManager.gameObject.SetActive(true);
        LevelUpManager.StartCoroutine(LevelUpManager.LevelUpSequence());
        XP = 0;
        if (PassiveItemNames.Contains("REFILLING POTION")) { PlayerStats.Health += 3; } // ITEM
        // The level up ui stuff
        yield return new WaitForSeconds(0.5f);
        GainXP(XPCarryOver);
        XPCarryOver = 0;
    }

    public void ChangeLevel(string Scene)
    {
        if (HasPlayer) { StoreStats(); }

        HasPlayer = false;
        SceneToGoTo = Scene;
        SceneManager.LoadScene("Loading"); //THIS SHOULD GO TO A LOADING SCENE THAT CHECKS THE GAMEMANAGERS SCENETOGOTO STRING AND THEN LOADS THE RIGHT ONE
    }

    public void StoreStats()
    {
        Health = PlayerStats.Health;
        MaxHealth = PlayerStats.MaxHealth;
        Mana = PlayerStats.Mana;
        MaxMana = PlayerStats.MaxMana;
        CastTimeModifier = Player.CastTimeModifier;
        Speed = PlayerStats.Speed;
        RegenBase = PlayerStats.RegenBase;
        SelectedCaltrop = Player.SelectedCaltrop;
        CaltropCycle = Player.CaltropCycle;
    }

    public void GiveStats()
    {
        PlayerStats.Health = Health;
        PlayerStats.MaxHealth = MaxHealth;
        PlayerStats.Mana = Mana;
        PlayerStats.MaxMana = MaxMana;
        Player.CastTimeModifier = CastTimeModifier;
        PlayerStats.Speed = Speed;
        PlayerStats.RegenBase = RegenBase;
        Player.SelectedCaltrop = SelectedCaltrop;
        Player.CaltropCycle = CaltropCycle;
    }
}
