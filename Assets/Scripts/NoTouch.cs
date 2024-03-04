#if UNITY_EDITOR
using UnityEngine;

// 코드를 수정하면 인스펙터 창에 적어놓은 메모 전부 지워짐 주의
// 빌드 파일에는 반영되지 않음
// 플레이 시 컴포넌트에서 사라짐
// 소소한 주의: 한글 치고 마지막에 엔터든 뭐든 안 눌러주면 저장하면서 마지막 한 글자 잘립니다


/* **************************************************************************
* 2023 Alan Mattanó, Soaring Stars lab }}
*
* Overall, this script allows you to add notes
* or comments to GameObjects in the Unity Editor,
* making it easier to communicate essential information
* about GameObjects or their components to other developers
*
*           WARNING: DO NOT MODIFY
*           you will lose all data
*
* By wrapping the script inside the #if UNITY_EDITOR directive,
* it will only be compiled and executed in the Unity Editor.
* When you build your project, the script will be excluded from the build,
* and it won't impact the final game. It may need to add using UnityEditor;
* at the beginning of the script if you encounter any issues with the
* #if UNITY_EDITOR directive.
* ************************************************************************/

[AddComponentMenu("Miscellaneous/README Info Note")]
public class NoTouch : MonoBehaviour
{
    [TextArea(17, 1000)]
    public string comment = "Information Here.";

    void Awake()
    {
        comment = null;

        // Assuming you want to destroy this script component
        Destroy(this);
    }
}
#endif