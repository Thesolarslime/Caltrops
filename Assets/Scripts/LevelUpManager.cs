using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public bool MenuActive;
    public Animator MenuAnimator;

    public LevelUpChoiceBox[] ChoiceBoxes;

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

    public void PickCaltropOptions()
    {

    }

    public void LevelUpSequence()
    {
        MenuAnimator.SetBool("MenuUp", true);
        this.gameObject.SetActive(true);
    }
}
