using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveFromStartToMenuScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GoGoGo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GoGoGo()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("MainMenu");
    }
}
