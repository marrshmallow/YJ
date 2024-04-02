using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

/// <summary>
/// 
/// �÷��̾ �����Կ� ���� ��ũ������ ����Ǵ� ������ �Ҹ� ����
/// �ƾ� ��� �� ���� �Ҹ��� ���ִ� ���
/// ������ ��׶��忡�� �׽� ����˴ϴ�.
/// 
/// - ������
/// </summary>

namespace Jinsol
{
    public class BoothAudioXFade : MonoBehaviour
    {
        protected BoxCollider soundArea; // �ö��̴��� ������ �ν� ���� �ڵ� �浹 ����
        protected AudioSource videoSound;
        [SerializeField] protected float fadeTime = 1.5f; // ũ�ν����̵� �ð�
        protected bool isNear; // �ö��̴��� ��Ҵ���
        [SerializeField] protected PlayableDirector director;
        protected float cutsceneFadeTime = 0.5f; // �ƾ� ����� ���� �� �� ���� ��ȯ�ǰ�

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




