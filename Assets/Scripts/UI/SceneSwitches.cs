using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitches : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
