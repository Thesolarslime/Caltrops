using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitches : MonoBehaviour
{
    private GameManager GameManager;
    public void StartGame()
    {
        GameManager = FindAnyObjectByType<GameManager>();
        GameManager.ChangeLevel("Level1");
    }
}
