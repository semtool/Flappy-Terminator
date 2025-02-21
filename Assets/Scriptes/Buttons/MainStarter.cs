using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStarter : MonoBehaviour
{
    public void OpenGame()
    {
        SceneManager.LoadScene("Game");
    }
}