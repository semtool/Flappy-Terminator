using UnityEngine;

public class Pauser : MonoBehaviour
{
    public void MakeStop()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
