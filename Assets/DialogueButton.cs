using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class DialogueButton : MonoBehaviour
{
    public PlayableDirector director;
    public SignalAsset cueSign;
    public SignalReceiver receiver;
    public bool standby = false; // 다음 컷씬으로 넘어갈 준비가 되었으면 true (이 상태에서 대화창 버튼을 누르면 다음 씬 재생)

    void Update()
    {
        if (standby)
            EndDialogue(cueSign, receiver);
    }

    public void EndDialogue(SignalAsset signal, SignalReceiver receiver)
    {
        Debug.Log("End of conversation.");
        //UnityEvent nextTimeline = receiver.GetReaction(cueSign); //.Timeline 관련 시그널은 EventSystem 담당
        //nextTimeline?.Invoke();
    }
}
