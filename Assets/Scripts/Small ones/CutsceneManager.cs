using TMPro;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    private int cutsceneStep;
    private float counter;

    public SpriteRenderer image1;
    public SpriteRenderer image2;
    public SpriteRenderer image3;
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;

    void Start()
    {
        cutsceneStep = 0;
        counter = 0;
    }

    void ImageFadeIn(SpriteRenderer image)
    {
        counter += 0.2f * Time.deltaTime;
        if (counter > 1) { counter = 1; }
        image.color = new Color(1.0f, 1.0f, 1.0f, counter);
    }

    void TextFadeIn(TMP_Text text)
    {
        counter += 0.6f * Time.deltaTime;
        if (counter > 1) { counter = 1; }
        text.color = new Color(1.0f, 1.0f, 1.0f, counter);
    }
    void FadeOut(TMP_Text text)
    {
        counter += -0.6f * Time.deltaTime;
        if (counter < 0) { counter = 0; }
        text.color = new Color(1.0f, 1.0f, 1.0f, counter);
    }


    void Update()
    {
        //change counter to either 0 or 1 depending on if the next step is fading in or out
        //TEXT 1 SHOW
        if (cutsceneStep == 0)
        {
            TextFadeIn(text1);
            if (counter == 1) { cutsceneStep += 1; counter = 0; }
        }
        //IMAGE 1 SHOW
        if (cutsceneStep == 1)
        {
            ImageFadeIn(image1);
            if (counter == 1) { cutsceneStep += 1; counter = 1; }
        }
        //TEXT 1 HIDE
        if (cutsceneStep == 2)
        {
            FadeOut(text1);
            if (counter == 0) { cutsceneStep += 1; counter = 0; }
        }
        //TEXT 2 SHOW
        if (cutsceneStep == 3)
        {
            TextFadeIn(text2);
            if (counter == 1) { cutsceneStep += 1; counter = 0; }
        }
        //IMAGE 2 SHOW
        if (cutsceneStep == 4)
        {
            ImageFadeIn(image2);
            if (counter == 1) { cutsceneStep += 1; counter = 1; }
        }
        //TEXT 2 HIDE
        if (cutsceneStep == 5)
        {
            FadeOut(text2);
            if (counter == 0) { cutsceneStep += 1; counter = 0; }
        }
        //TEXT 3 SHOW
        if (cutsceneStep == 6)
        {
            TextFadeIn(text3);
            if (counter == 1) { cutsceneStep += 1; counter = 0; }
        }
        //IMAGE 3 SHOW
        if (cutsceneStep == 7)
        {
            ImageFadeIn(image3);
            if (counter == 1) { cutsceneStep += 1; counter = 1; }
        }
        //TEXT 3 HIDE
        if (cutsceneStep == 8)
        {
            FadeOut(text3);
            if (counter == 0) { cutsceneStep += 1; counter = 0; }
        }
        //TEXT 4 SHOW
        if (cutsceneStep == 9)
        {
            TextFadeIn(text4);
            if (counter == 1) { cutsceneStep += 1; counter = 0; }
        }
    }
}
