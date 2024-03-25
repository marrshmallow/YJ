using UnityEngine;

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
        material.color = new Color(204f / 255f, 0f, 28f / 255f, 1f); // ����Ʈ �������� �� �Ӱ� ǥ��
    }

    public void CanCompleteQuest()
    {
        material.color = new Color(0f, 168f / 255f, 102f / 255f, 1f); // ����Ʈ �Ϸ� ������ �� ������� ǥ��
    }
}
