using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void LoadScene(int pBuildIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(pBuildIndex);
    }
}
