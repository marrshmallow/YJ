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

        private SphereCollider sphereCollider;

        private void Awake()
        {
            sphereCollider = (SphereCollider)GetComponent("SphereCollider");
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
