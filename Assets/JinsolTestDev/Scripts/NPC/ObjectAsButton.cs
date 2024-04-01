using UnityEngine;

namespace Jinsol
{
    public class ObjectAsButton : MonoBehaviour
    {
        [Header("Events")]
        public GameEvent onQuestAccepted;

        private void OnMouseUpAsButton()
        {
            onQuestAccepted.Raise();
        }
    }
}
