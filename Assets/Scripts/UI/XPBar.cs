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
            float Percentage = (float)GameManager.XP / (float)GameManager.XPRequirements[GameManager.Level - 1];

            float XDis = (-7.125f + (14.375f * Percentage)) - transform.localPosition.x;

            transform.localPosition = new Vector3(transform.localPosition.x + (XDis * 0.15f), 0, 0);
        }
        else
        {
            float XDis = -14.375f - transform.localPosition.x;

            transform.localPosition = new Vector3(transform.localPosition.x + (XDis * 0.15f), -4.125f, 0) + new Vector3(-7.25f, 0, 0);
        }
    }
}
