using UnityEngine;

/// <summary>
/// 
/// NPC 머리 위 퀘스트 아이콘의 색상을 상태에 따라 변경해주는 기능
/// 
/// - 정진솔
/// </summary>

namespace Jinsol
{
    public class QuestIconChanger : MonoBehaviour
    {
        private Material material;

        private void Awake()
        {
            material = GetComponent<MeshRenderer>().material;
        }

        // Default color: RGBA (255, 152, 83, 1)

        public void QuestIncomplete()
        {
            material.color = new Color(204f / 255f, 0f, 28f / 255f, 1f); // 퀘스트 진행중일 때 붉게 표시
        }

        public void CanCompleteQuest()
        {
            material.color = new Color(0f, 168f / 255f, 102f / 255f, 1f); // 퀘스트 완료 가능할 때 녹색으로 표시
        }
    }
}
