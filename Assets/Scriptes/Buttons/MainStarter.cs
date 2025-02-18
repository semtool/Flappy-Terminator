using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStarter : MonoBehaviour
{
    public void OpenGame()
    {
        StartCoroutine(Show());
    }

    public IEnumerator Show()
    {
        yield return null;

        SceneManager.LoadScene("Game");
    } 
}