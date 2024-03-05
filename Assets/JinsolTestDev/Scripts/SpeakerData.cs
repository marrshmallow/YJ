using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Project 창에서 우클릭 했을 때 나오는 메뉴에서
// Create > Scriptable Object > Speaker Data라는 메뉴를 생성시켜줍니다.
// 메뉴를 실행하면 화자에 대한 데이터를 담을 수 있는 Scriptable Object가 생성됩니다.

[CreateAssetMenu(fileName = "Speaker Data", menuName = "Scriptable Object/Speaker Data", order = int.MaxValue)]
public class SpeakerData : ScriptableObject
{
    public string viewName; //보여질 이름
    
    // 일인칭 시점으로 연출할 거라서 (주인공 얼굴이 나오는 연출 할 수 있음)
    // 초상화 표시하는 코드는 없습니다.
    // 레퍼런스 : https://youtu.be/aOIXytJnc-c?si=jdit-1vwqlj3gyLU

#region 타임라인 연출을 위한 사전 준비
// 1. 컷씬의 포커스가 될 게임오브젝트 선택
// 2. 게임오브젝트 선택 후 Windows > Sequencing > Timeline
// 3. Create 버튼 클릭 후 필요한 파일 생성 (Timeline 폴더에 넣습니다)
// 4. 타임라인 창의 Settings에서 Time Unit을 Timecode로 변경 (편의)
// 5. 자동으로 Playable Director 컴포넌트가 게임오브젝트에 추가됨
#endregion

#region 타임라인 애니메이션 연출
// 1.
#endregion

}
