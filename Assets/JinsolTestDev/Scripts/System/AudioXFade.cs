using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 
/// 플레이어가 내부로 들어갈 때, 다시 로비로 나올 때
/// 로비 스크린에서 재생되는 비디오의 소리와 메인 BGM 소리 사이의 균형 조절
/// 
/// - 정진솔
/// </summary>

namespace Jinsol
{
    public class AudioXFade : MonoBehaviour
    {
        [SerializeField] private AudioSource videoSound;
        [SerializeField] private float fadeTime = 1f;
        private float cutsceneFadeTime = 0.5f;
        [SerializeField] private PlayableDirector director;
        private bool inLobby = true;

        private void Update()
        {
            if (inLobby)
            {
                if (director.state == PlayState.Playing)
                    StartCoroutine(FadeOut(videoSound, cutsceneFadeTime));
                else
                    StartCoroutine(FadeIn(videoSound, fadeTime));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                inLobby = false;
                StartCoroutine(FadeOut(videoSound, fadeTime));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                inLobby = true;
                StartCoroutine(FadeIn(videoSound, fadeTime));
            }
        }

        public IEnumerator FadeOut(AudioSource videoSound, float fadeTime)
        {
            float startVolume = videoSound.volume;
            while (videoSound.volume > 0)
            {
                videoSound.volume -= startVolume * Time.deltaTime / fadeTime;
                yield return null;
            }
            videoSound.Stop();
        }

        public IEnumerator FadeIn(AudioSource videoSound, float fadeTime)
        {
            float startVolume = videoSound.volume;
            while (videoSound.volume < 1)
            {
                videoSound.volume += Time.deltaTime / fadeTime * 0.1f;
                yield return null;
            }
            videoSound.Play();
        }
    }
}
