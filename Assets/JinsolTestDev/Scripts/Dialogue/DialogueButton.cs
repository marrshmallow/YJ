using UnityEngine;
using UnityEngine.Playables;

public class DialogueButton : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private PlayableAsset nextTimeline;
    public bool standby = false; // 다음 컷씬으로 넘어갈 준비가 되었으면 true (이 상태에서 대화창 버튼을 누르면 다음 씬 재생)

    void Update()
    {
        if (standby)
            {
                director.extrapolationMode = DirectorWrapMode.None;
                Debug.Log("Director stopped");
                director.Play(nextTimeline);
                Debug.Log("Playing nextTimeline...");
            }
    }
}