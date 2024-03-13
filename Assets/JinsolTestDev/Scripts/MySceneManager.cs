using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public void LoadNextScene(int sceneAssetNumber)
    {
        SceneManager.LoadScene(1);
    }
}
