using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // EventInstance footsteps;
    // EventDescription footstepsDescription;
    // PARAMETER_DESCRIPTION pd;
    // PARAMETER_ID pID;

    // //[SerializeField] EventReference footstepEvent;
    // [SerializeField] float rate; // 걸음의 간격
    // [SerializeField] Player player;

    // private float time;
    // //public int m_Grass; // FMOD에서 설정해준 parameter 값에 대응하는 값
    // //public int m_Patio; // FMOD에서 설정해준 parameter 값에 대응하는 값
    // //public int m_Concrete; // FMOD에서 설정해준 parameter 값에 대응하는 값

    // private void Start()
    // {
    //     footsteps = RuntimeManager.CreateInstance("event:/Player/Footsteps");
    //     footsteps.start();
    //     footstepsDescription = RuntimeManager.GetEventDescription("event:/Player/Footsteps");
    //     footstepsDescription.getParameterDescriptionByName("Concrete", out pd);
    //     pID = pd.id;
    // }

    // void Update()
    // {
    //     time += Time.deltaTime;
    //     if (time >= rate)
    //     {
    //         footsteps.setParameterByID(pID, 1f);
    //         time = 0;
    //     }
    // }

    // public void PlayFootstep()
    // {
    //     // Default Setting
    //     // m_Grass = 0;
    //     // m_Patio = 0;
    //     // m_Concrete = 1;

    //     // EventInstance walk = RuntimeManager.CreateInstance(footstepEvent);
    //     // walk.setParameterByID(walkParameterID,)
    //     // RuntimeManager.PlayOneShotAttached(footstepEvent, player);
    // }
}
