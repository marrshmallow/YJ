using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MySceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    public void LoadNextScene(int sceneAssetNumber)
    {
        SceneManager.LoadScene(sceneAssetNumber);
    }

    public void ToggleHoldDirector()
    {
        if (director.extrapolationMode == DirectorWrapMode.Hold)
            director.extrapolationMode = DirectorWrapMode.None;
        else if (director.extrapolationMode == DirectorWrapMode.None)
            director.extrapolationMode = DirectorWrapMode.Hold;
    }
}
