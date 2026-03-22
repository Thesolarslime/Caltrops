using UnityEngine;

public class XPBar : MonoBehaviour
{
    private GameManager GameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.XP != 0)
        {
            float Percentage = GameManager.XP / GameManager.XPRequirements[GameManager.Level - 1];

            float XDis = (-14.375f + (14.375f * Percentage)) - transform.position.x;

            transform.position = new Vector3(transform.position.x + (XDis * 0.15f), -4.125f, 0);
        }
        else
        {
            float XDis = -14.375f - transform.position.x;

            transform.position = new Vector3(transform.position.x + (XDis * 0.15f), -4.125f, 0) + new Vector3(-7.25f, 0, 0);
        }
    }
}
