using UnityEngine;
using UnityEngine.SceneManagement;

public class Exiter : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    } 
}