using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    private int cutsceneStep;
    private float counter;

    public SpriteRenderer image1;
    public SpriteRenderer image2;
    public SpriteRenderer image3;
    public SpriteRenderer image4;
    public SpriteRenderer image5;
    public SpriteRenderer image6;
    public SpriteRenderer image7;
    public SpriteRenderer image8;
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;
    public TMP_Text text6;
    public TMP_Text text7;
    public TMP_Text text8;
    public TMP_Text text9;
    public TMP_Text text10;
    public TMP_Text text11;

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
        //skipping back to main menu early
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
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
        //WAIT
        if (cutsceneStep == 10)
        {
            counter += 0.2f * Time.deltaTime;
            if (counter > 1) { cutsceneStep += 1; counter = 1; }
        }
        //TEXT 4 HIDE
        if (cutsceneStep == 11)
        {
            FadeOut(text4);
            if (counter == 0) { cutsceneStep += 1;}
        }
        //TEXT 5 SHOW
        if (cutsceneStep == 12)
        {
            TextFadeIn(text5);
            if (counter == 1) { cutsceneStep += 1; counter = 0; }
        }
        //IMAGE 4 SHOW
        if (cutsceneStep == 13)
        {
            ImageFadeIn(image4);
            if (counter == 1) { cutsceneStep += 1;}
        }
        //TEXT 5 HIDE
        if (cutsceneStep == 14)
        {
            FadeOut(text5);
            if (counter == 0) { cutsceneStep += 1;}
        }
        //TEXT 6 SHOW
        if (cutsceneStep == 15)
        {
            TextFadeIn(text6);
            if (counter == 1) { cutsceneStep += 1; counter = 0; }
        }
        //IMAGE 5 SHOW
        if (cutsceneStep == 16)
        {
            ImageFadeIn(image5);
            if (counter == 1) { cutsceneStep += 1;}
        }
        //WAIT
        if (cutsceneStep == 17)
        {
            counter += 0.5f * Time.deltaTime;
            if (counter > 1) { cutsceneStep += 1; counter = 1; }
        }
        //TEXT 6 HIDE
        if (cutsceneStep == 18)
        {
            FadeOut(text6);
            if (counter == 0) { cutsceneStep += 1; }
        }
        //TEXT 7 SHOW
        if (cutsceneStep == 19)
        {
            TextFadeIn(text7);
            if (counter == 1) { cutsceneStep += 1; counter = 0; }
        }
        //IMAGE 6 SHOW
        if (cutsceneStep == 20)
        {
            ImageFadeIn(image6);
            if (counter == 1) { cutsceneStep += 1; }
        }
        //WAIT
        if (cutsceneStep == 21)
        {
            counter += 0.5f * Time.deltaTime;
            if (counter > 1) { cutsceneStep += 1; counter = 1; }
        }
        //TEXT 7 HIDE
        if (cutsceneStep == 22)
        {
            FadeOut(text7);
            if (counter == 0) { cutsceneStep += 1; }
        }
        //TEXT 8 SHOW
        if (cutsceneStep == 23)
        {
            TextFadeIn(text8);
            if (counter == 1) { cutsceneStep += 1; counter = 0; }
        }
        //WAIT
        if (cutsceneStep == 24)
        {
            counter += 0.4f * Time.deltaTime;
            if (counter > 1) { cutsceneStep += 1; counter = 1; }
        }
        //TEXT 8 HIDE
        if (cutsceneStep == 25)
        {
            FadeOut(text8);
            if (counter == 0) { cutsceneStep += 1; }
        }
        //TEXT 9 SHOW
        if (cutsceneStep == 26)
        {
            TextFadeIn(text9);
            if (counter == 1) { cutsceneStep += 1; counter = 0; }
        }
        //IMAGE 7 SHOW
        if (cutsceneStep == 27)
        {
            ImageFadeIn(image7);
            if (counter == 1) { cutsceneStep += 1; }
        }
        //WAIT
        if (cutsceneStep == 28)
        {
            counter += 0.5f * Time.deltaTime;
            if (counter > 1) { cutsceneStep += 1; counter = 1; }
        }
        //TEXT 9 HIDE
        if (cutsceneStep == 29)
        {
            FadeOut(text9);
            if (counter == 0) { cutsceneStep += 1; }
        }
        //TEXT 10 SHOW
        if (cutsceneStep == 30)
        {
            TextFadeIn(text10);
            if (counter == 1) { cutsceneStep += 1; counter = 0; }
        }
        //WAIT
        if (cutsceneStep == 31)
        {
            counter += 0.2f * Time.deltaTime;
            if (counter > 1) { cutsceneStep += 1; counter = 1; }
        }
        //TEXT 10 HIDE
        if (cutsceneStep == 32)
        {
            FadeOut(text10);
            if (counter == 0) { cutsceneStep += 1; }
        }
        //TEXT 11 SHOW
        if (cutsceneStep == 33)
        {
            TextFadeIn(text11);
            if (counter == 1) { cutsceneStep += 1;}
        }
        if (cutsceneStep == 34)
        {
            image1.color = new Color(1.0f, 1.0f, 1.0f, 0);
            image2.color = new Color(1.0f, 1.0f, 1.0f, 0);
            image3.color = new Color(1.0f, 1.0f, 1.0f, 0);
            image4.color = new Color(1.0f, 1.0f, 1.0f, 0);
            image5.color = new Color(1.0f, 1.0f, 1.0f, 0);
            image6.color = new Color(1.0f, 1.0f, 1.0f, 0);
            cutsceneStep += 1;
        }
        if (cutsceneStep == 35)
        {
            counter += -0.2f * Time.deltaTime;
            if (counter < 0) { counter = 0; }
            image7.color = new Color(1.0f, 1.0f, 1.0f, counter);
            text11.color = new Color(1.0f, 1.0f, 1.0f, counter);
            if (counter == 0) { cutsceneStep += 1; counter = 1; }
        }
        if (cutsceneStep == 36)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
