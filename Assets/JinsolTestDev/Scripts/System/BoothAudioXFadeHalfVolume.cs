using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

/// <summary>
/// 
/// 유튜브 소스 파일의 소리가 너무 커서 반틈 줄이는 코드.
/// 
/// - 정진솔
/// </summary>

namespace Jinsol
{

    public class BoothAudioXFadeHalfVolume : BoothAudioXFade
    {
        [SerializeField] private float targetVolume = 0.5f;

        private void Awake()
        {
            soundArea = (BoxCollider)GetComponent("BoxCollider");
            videoSound = (AudioSource)GetComponent("AudioSource");
        }

        private void Update()
        {
            if (director.state == PlayState.Playing)
                StartCoroutine(FadeOut(videoSound, cutsceneFadeTime));
            else if (isNear)
                StartCoroutine(FadeIn(videoSound, fadeTime));
            else
                StartCoroutine(FadeOut(videoSound, fadeTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                isNear = true;
                StartCoroutine(FadeIn(videoSound, fadeTime));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                isNear = false;
                StartCoroutine(FadeOut(videoSound, fadeTime));
            }
        }

        private IEnumerator FadeIn(AudioSource videoSound, float fadeTime)
        {
            while (videoSound.volume < 0.5f)
            {
                videoSound.volume += Time.deltaTime / fadeTime * 0.1f;
                yield return null;
            }
            videoSound.Play();

            if (videoSound.volume >= 0.5f)
                videoSound.volume = 0.5f;
        }
    }
}
