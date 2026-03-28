using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    private GameManager GameManager;
    private AudioPlayer Sound;

    public TextMeshProUGUI Text;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager = FindAnyObjectByType<GameManager>();
        Sound = GetComponent<AudioPlayer>();
        ChangeText();
        StartCoroutine(Loading());
    }

    public void ChangeText()
    {
        switch(GameManager.SceneToGoTo)
        {
            case "Level1":
                Text.text = "LEVEL 1\n\nTHE BOX";
                GameManager.CurrentLevelID = 1;
                Sound.PlaySound(0, false, 1);
                break;
            case "Level2":
                Text.text = "LEVEL 2\n\nINNER RING";
                GameManager.CurrentLevelID = 2;
                Sound.PlaySound(1, false, 1);
                break;
            case "Level3":
                Text.text = "LEVEL 3\n\nMIDDLE RING";
                GameManager.CurrentLevelID = 4;
                break;
            case "Level4":
                Text.text = "LEVEL 4\n\nOUTER RING";
                GameManager.CurrentLevelID = 6;
                break;
            case "MainMenu":
                Text.text = "ESCAPE FAILED";
                GameManager.CurrentLevelID = 0;
                break;
            case "Boss1":
                Text.text = "THE ROGUE";
                GameManager.CurrentLevelID = 3;
                break;
            case "Boss2":
                Text.text = "THE ARCHMAGE";
                GameManager.CurrentLevelID = 5;
                break;
            case "Boss3":
                Text.text = "THE HERO";
                GameManager.CurrentLevelID = 7;
                break;
            case "IntroCutscene":
                Text.text = "STORY\n\nSPACE TO SKIP";
                GameManager.CurrentLevelID = 0;
                break;
        }
    }

    public IEnumerator Loading()
    {
        yield return new WaitForSecondsRealtime(5.5f);
        SceneManager.LoadScene(GameManager.SceneToGoTo);
    }
}
