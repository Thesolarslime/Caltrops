using UnityEngine;

public class LevelUpChoiceBox : MonoBehaviour
{
    public LevelBonus Bonus;
    public CaltropType Caltrop;

    public Animator BoxAnimator;
    public Animator SpriteAnimator;

    public LevelUpManager LevelUpManager;
    public int ID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelUpManager.MenuActive)
        {
            if (LevelUpManager.SelectedBox == ID)
            {
                BoxAnimator.SetBool("Selected", true);
                SpriteAnimator.SetBool("Selected", true);
            }
            else
            {
                BoxAnimator.SetBool("Selected", false);
                SpriteAnimator.SetBool("Selected", false);
            }
        }
    }
}
