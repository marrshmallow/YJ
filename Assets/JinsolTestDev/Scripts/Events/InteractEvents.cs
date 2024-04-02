//using System;
using UnityEngine;

// 아직 사용하지 않음

namespace Jinsol
{
    public class InteractEvents
    {
        /*     public delegate void InteractHandler();
            public event InteractHandler hasInteracted;
            public void CallInteractEvent() => hasInteracted?.Invoke();
            public event Action<string> onInteract; // 상호작용 이벤트 */
    }

    public class Interact : MonoBehaviour
    {
        /*     private InteractEvents interact = new InteractEvents();
            Player player;

            public InteractEvents GetInteractEvents
            {
                get
                {
                    if (interact == null) interact = new InteractEvents();
                    return interact;
                }
            }

            public Player GetPlayer
            {
                get
                {
                    return player;
                }
            }
            public void CallInteract(Player interactedPlayer)
            {
                player = interactedPlayer;
                interact.CallInteractEvent();
            } */
    }
}