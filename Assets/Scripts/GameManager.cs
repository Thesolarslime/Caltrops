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

        //give the player their stats back here if it is not 1st level scene or something
    }

    public void ChangeLevel(string Scene)
    {
        //store the player's stats here

        HasPlayer = false;
        SceneToGoTo = Scene;
        SceneManager.LoadScene("Loading"); //THIS SHOULD GO TO A LOADING SCENE THAT CHECKS THE GAMEMANAGERS SCENETOGOTO STRING AND THEN LOADS THE RIGHT ONE
    }
}
