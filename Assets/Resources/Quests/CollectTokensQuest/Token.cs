using UnityEngine;

/// <summary>
/// �ν��� NPC�� �̾߱��� ������ ȹ���ϴ� ��ǥ�� ����
/// ���������δ� �ν� NPC ���� �ߴ� �����ܿ� ����
/// ��ȭ�� �����ϰ� �Ǹ� ��ȭ�� ���� ������ Ÿ�Ӷ��� ����� ����ǹǷ�
/// ���� �� ��ū �ϳ� ȹ���� �ɷ� ó��������
/// ǥ���� ��ȭ�� ������ �ϴ� ���
/// 
/// - ������
/// </summary>

namespace Jinsol
{
    [RequireComponent(typeof(SphereCollider))]
    public class Token : MonoBehaviour
    {
        [Header("����")]
        [SerializeField] private int tokensCollected = 1;

        // ���� ����Ʈ�� ���� ��Ȳ�� ���� ������ ������ ������ �ʰ� �ϱ� ���� �κ�
        private QuestIcon questIcon;

        private SphereCollider sphereCollider;

        private void Awake()
        {
            questIcon = (QuestIcon)GetComponent("QuestIcon");
            sphereCollider = (SphereCollider)GetComponent("SphereCollider");
            //questIcon.isSubQuest = true;
        }

        public void CollectToken()
        {
            sphereCollider.enabled = false;
            GameEventsManager.instance.tokenEvents.TokenCollected(tokensCollected);
            GameEventsManager.instance.miscEvents.TokenCollected();
            GameEventsManager.print(tokensCollected);
        }
    }
}
