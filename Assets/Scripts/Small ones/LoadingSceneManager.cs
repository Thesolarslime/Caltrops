using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    private GameManager GameManager;

    public TextMeshProUGUI Text;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager = FindAnyObjectByType<GameManager>();
        ChangeText();
        StartCoroutine(Loading());
    }

    public void ChangeText()
    {
        switch(GameManager.SceneToGoTo)
        {
            case "Level1":
                Text.text = "LEVEL 1\n\nTHE BOX";
                break;
            case "Level2":
                Text.text = "LEVEL 2\n\nINNER RING";
                break;
            case "Level3":
                Text.text = "LEVEL 3\n\nMIDDLE RING";
                break;
            case "Level4":
                Text.text = "LEVEL 4\n\nOUTER RING";
                break;
            case "MainMenu":
                Text.text = "ESCAPE FAILED";
                break;
            case "Boss1":
                Text.text = "THE ROGUE";
                break;
            case "Boss2":
                Text.text = "THE ARCHMAGE";
                break;
            case "Boss3":
                Text.text = "THE HERO";
                break;
        }
    }

    public IEnumerator Loading()
    {
        yield return new WaitForSecondsRealtime(5.5f);
        SceneManager.LoadScene(GameManager.SceneToGoTo);
    }
}
