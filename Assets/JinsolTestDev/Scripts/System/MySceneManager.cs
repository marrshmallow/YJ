using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

/// <summary>
/// 
/// 1. 다음 씬(Unity의 Scene 파일)을 불러오는 기능
/// 2. 플레이어블 에셋 재생 방식을 토글하는 기능
///     DirectorWrapMode.None: 재생이 끝나면 재생하기 전의 상태로 원상복귀하는 방식
///     DirectorWrapMode.Hold: 재생이 끝나도 마지막 상태를 유지하는 방식
///     
/// - 정진솔
/// </summary>

namespace Jinsol
{
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
}
