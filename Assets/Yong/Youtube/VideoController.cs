using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace YoutubePlayer
{
    public class VideoController : MonoBehaviour
    {
        // ���� ��ü�� ���� ��Ʃ�� �÷��̾�
        public YoutubePlayer youtubePlayer;
        // ���� ��ü�� ���� �÷��̾�
        public VideoPlayer videoPlayer;
        // ��Ʃ�� �������� �÷��� �������� Ȯ���ϱ� ���� ��ư
        // ��ġ�� ȭ�� ������ �������� �������ʰ� ����.
        public Button playBtn;
        // ���� ������ ����� ���� �ؽ�ó
        public RenderTexture renderTexture;
        // ���� ������� �ƹ��͵� ������ ���� �ӽ� ���� �ؽ�ó
        public RenderTexture nonRenderTexture;
        // ���� �ؽ�ó�� �����ϴ� RawImage ��ü
        public RawImage rawImage;
        // Start is called before the first frame update

        // Start�� Update�� ������� �ʴ´�.
        private void Awake()
        {
            //rawImage.texture = nonRenderTexture;
            playBtn.interactable = false;

            SetYoutubeVideo("https://youtu.be/Jn_Wxx24Yak");
        }

        public void SetYoutubeVideo(string url)
        {
            try
            {
                videoPlayer.prepareCompleted -= VideoReadyAndCompleted;
            }
            catch (System.Exception ex)
            {
                // TODO
            }
            try
            {
                videoPlayer.prepareCompleted += VideoReadyAndCompleted;
            }
            catch (System.Exception ex)
            {
                // TODO
            }
            playBtn.interactable = false;
            youtubePlayer.youtubeUrl = url;
            Prepare(youtubePlayer.youtubeUrl);
        }

        void VideoReadyAndCompleted(VideoPlayer source)
        {
            playBtn.interactable = source.isPrepared;
        }

        public async void Prepare(string url = null)
        {
            try
            {
                await youtubePlayer.PrepareVideoAsync();
                StartCoroutine(Play());
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }

        public IEnumerator Play()
        {
            while (true)
            {
                yield return null;
                if (playBtn.interactable == true)
                {
                    rawImage.texture = renderTexture;
                    videoPlayer.Play();
                    break;
                }
            }
        }

        public void OnDestroy()
        {
            videoPlayer.Stop();
            rawImage.texture = nonRenderTexture;
            videoPlayer.prepareCompleted -= VideoReadyAndCompleted;
        }
    }
}

