using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;
using TMPro;

public class Typewriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;

    #region 기본 기능
    private int currentVisibleCharIndex;
    private Coroutine typewriterCoroutine;
    private bool standbyNewText = true;
    private WaitForSeconds _delay;
    private WaitForSeconds _punctDelay; // 문장부호 때 조금 늦추기
    #endregion

    #region 스킵 기능
    public bool isSkipping {get; private set;} // 지금 건너뛰는 중인지
    private WaitForSeconds _skipDelay;
    private bool quickSkip; // 텍스트 한번에 전부 표시하는 연출
    [SerializeField] [Min(1)] private int skipSpeedup = 5;
    #endregion

    #region 텍스트 전부 표시 완료 이벤트
    private WaitForSeconds textboxFullEventDelay;
    [SerializeField] [Range(0.1f, 0.5f)] private float sendFinishedDelay = 0.25f;
    public static event Action FullTextRevealed;
    #endregion

    [Header("텍스트 표시 설정")]
    [SerializeField] private float charPerSecond = 20f;
    [SerializeField] private float punctDelay = 0.5f;
    
    private void Awake()
    {
        _delay = new WaitForSeconds(1/charPerSecond);
        _punctDelay = new WaitForSeconds(punctDelay);

        _skipDelay = new WaitForSeconds(1/(charPerSecond*skipSpeedup));

        textboxFullEventDelay = new WaitForSeconds(sendFinishedDelay);
    }

    #region 텍스트 출력
    private void OnEnable()
    {
        // 텍스트 박스 안의 내용에 변화가 있을 때마다 신호가 발생하는 걸 구독
        // 텍스트를 표시하는 함수도 이에 해당되게 한다
        // 단, 여기에 적용하는 함수의 매개변수는 Object여야 한다
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(PrepareText); // TMPro_EventManager는 무려 기본 기능
    }

    private void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(PrepareText);
    }

    public void PrepareText(Object obj)
    {
        if (obj != textBox || !standbyNewText || textBox.maxVisibleCharacters >= textBox.textInfo.characterCount) // 기존에 텍스트가 표시되는 중에는 실행되지 않게 함
            return;

        isSkipping = false;
        standbyNewText = false;
        
        // 한 번에 오로지 하나의 코루틴만 실행되게
        if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);

        textBox.maxVisibleCharacters = 0; // 최적화용
        currentVisibleCharIndex = 0;
        Debug.Log("???");
        typewriterCoroutine = StartCoroutine(TypewriterStart()); // 언제든지 멈출 수 있게 하기 위함
    }

    private IEnumerator TypewriterStart()
    {
        TMP_TextInfo textInfo = textBox.textInfo;

        while (currentVisibleCharIndex < textInfo.characterCount + 1)
        {
            int lastCharIndex = textInfo.characterCount - 1;
            
            if (currentVisibleCharIndex == lastCharIndex)
            {
                textBox.maxVisibleCharacters++;
                yield return textboxFullEventDelay;
                FullTextRevealed?.Invoke();
                standbyNewText = true;
                yield break;
            }

            char character = textInfo.characterInfo[currentVisibleCharIndex].character;
            textBox.maxVisibleCharacters++;

            if (!isSkipping && (character == '?' || character == '!' || character == ',' || character == '.'))
            {
                yield return _punctDelay;
            }
            else
            {
                yield return isSkipping ? _skipDelay : _delay; // isSkipping이 참이면 _skipDelay를 불러오고, 아니면 _delay를 불러온다
                // 같은 코드: if (isSkipping) yield return _skipDelay else yield return _delay;
            }

            currentVisibleCharIndex++;
        }
    }

    private void Skip()
    {
        if (isSkipping)
            return;
        
        isSkipping = true;

        if (!quickSkip)
        {
            StartCoroutine(SkipSpeedupReset());
            return;
        }
        
        StopCoroutine(typewriterCoroutine);
        textBox.maxVisibleCharacters = textBox.textInfo.characterCount;
        standbyNewText = true;
        FullTextRevealed?.Invoke();
    }

    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => textBox.maxVisibleCharacters == textBox.textInfo.characterCount -1);
        isSkipping = false;
    }
    #endregion

    // Q) I'm puzzled. You add this script to a Text (TMP) component. Where do you put the complete text?
    // When I put my text in Text Input my text is just standing there. What am I missing?
    //
    // A) It's not explained super clearly. We attach the 'typewriterEffect' to the TextMeshProText-element and it detects any changes we make to the text.
    // So we can change the text in other scripts without a reference to the typewriter.
    // The typewriter will simply detect that the text is different and type it out.
    // Also remember to set the _textBox.maxVisibleCharacters = 0 every time you write out new text.
}
