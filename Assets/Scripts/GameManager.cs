using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Health;
    public int MaxHealth;
    public int Mana;
    public int MaxMana;
    public int XP;
    public int Level;
    public int SelectedCaltrop;
    public CaltropType[] CaltropCycle;
    public PassiveItem[] PassiveItems;

    public PlayerManager Player;
    private ObjectStats PlayerStats;
    private ObjectMovement PlayerMovement;

    public bool HasPlayer;
    public string SceneToGoTo;
    public int CurrentLevelID;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);

        HasPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPlayer(PlayerManager Input)
    {
        Player = Input;
        PlayerStats = Player.GetComponent<ObjectStats>();
        PlayerMovement = Player.GetComponent<ObjectMovement>();
        HasPlayer = true;

        if (CurrentLevelID > 1) { GiveStats(); }
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
        SelectedCaltrop = Player.SelectedCaltrop;
        CaltropCycle = Player.CaltropCycle;
    }

    public void GiveStats()
    {
        PlayerStats.Health = Health;
        PlayerStats.MaxHealth = MaxHealth;
        PlayerStats.Mana = Mana;
        PlayerStats.MaxMana = MaxMana;
        Player.SelectedCaltrop = SelectedCaltrop;
        Player.CaltropCycle = CaltropCycle;
    }
}
