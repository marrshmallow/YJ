using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public SpeakerData speaker; // 화자 데이터 불러오기
    public int emotionCode = 0; // 화자 감정표현 분류 코드 불러오기
    [TextArea(3, 10)]
    public string dialogue; // 표시될 대화 내용을 문자열로 불러오기
}

// Project > Create > Scriptable Object > Dialogue Data 메뉴 생성
[CreateAssetMenu(fileName = "Dialogue Data", menuName = "Scriptable Object/Dialogue Data", order = int.MaxValue)]
public class DialogueData : ScriptableObject
{
   public Dialogue[] dialogueList; // 대화용 스크립터블 오브젝트를 만들어 발화 내용을 배열로 저장
}
