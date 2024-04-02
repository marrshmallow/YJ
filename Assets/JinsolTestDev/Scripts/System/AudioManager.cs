using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private EventReference uiClickEvent;
    [SerializeField] private EventReference uiOpenEvent;
    [SerializeField] private EventReference uiKnobClickEvent;

    private void Awake()
    {
        instance = this;
    }

    public void PlayUIClickEvent()
    {
        RuntimeManager.PlayOneShot(uiClickEvent);
    }
    
    public void PlayUIOpenEvent()
    {
        RuntimeManager.PlayOneShot(uiOpenEvent);
    }
    
    public void PlayUIKnobClickEvent()
    {
        RuntimeManager.PlayOneShot(uiKnobClickEvent);
    }
}
