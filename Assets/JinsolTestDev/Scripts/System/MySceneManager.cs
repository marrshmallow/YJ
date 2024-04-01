using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

/// <summary>
/// 
/// 1. ���� ��(Unity�� Scene ����)�� �ҷ����� ���
/// 2. �÷��̾�� ���� ��� ����� ����ϴ� ���
///     DirectorWrapMode.None: ����� ������ ����ϱ� ���� ���·� ���󺹱��ϴ� ���
///     DirectorWrapMode.Hold: ����� ������ ������ ���¸� �����ϴ� ���
///     
/// - ������
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
