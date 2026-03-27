using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    private int cutsceneStep;
    private float counter;

    public SpriteRenderer image1;

    void Start()
    {
        cutsceneStep = 0;
        counter = 0;
    }

    void Update()
    {
        //duplicate for each image fading in and just change what image its affecting
        if (cutsceneStep == 0)
        {
            counter += 1*Time.deltaTime;
            if (counter > 1) { counter = 1; }
            image1.color = new Color(1.0f, 1.0f, 1.0f, counter);
            if (counter == 100) { cutsceneStep += 1; counter = 0; }

        }
    }
}
