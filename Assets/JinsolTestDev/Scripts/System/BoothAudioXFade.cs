using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

/// <summary>
/// 
/// 플레이어가 접근함에 따라 스크린에서 재생되는 비디오의 소리 조절
/// 컷씬 재생 시 비디오 소리를 꺼주는 기능
/// 비디오는 백그라운드에서 항시 재생됩니다.
/// 
/// - 정진솔
/// </summary>

namespace Jinsol
{
    public class BoothAudioXFade : MonoBehaviour
    {
        protected BoxCollider soundArea; // 컬라이더가 인접한 부스 사이 코드 충돌 방지
        protected AudioSource videoSound;
        [SerializeField] protected float fadeTime = 1.5f; // 크로스페이드 시간
        protected bool isNear; // 컬라이더에 닿았는지
        [SerializeField] protected PlayableDirector director;
        protected float cutsceneFadeTime = 0.5f; // 컷씬 재생할 때는 좀 더 빨리 전환되게

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

        protected IEnumerator FadeOut(AudioSource videoSound, float fadeTime)
        {
            float startVolume = videoSound.volume;
            while (videoSound.volume > 0)
            {
                videoSound.volume -= startVolume * Time.deltaTime / fadeTime;
                yield return null;
            }
            videoSound.Stop();
        }

        private IEnumerator FadeIn(AudioSource videoSound, float fadeTime)
        {
            while (videoSound.volume < 1)
            {
                videoSound.volume += Time.deltaTime / fadeTime * 0.1f;
                yield return null;
            }
            videoSound.Play();
        }
    }
}




