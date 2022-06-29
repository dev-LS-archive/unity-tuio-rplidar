using UnityEngine;
using UnityEngine.SceneManagement;
public class SimpleSceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
