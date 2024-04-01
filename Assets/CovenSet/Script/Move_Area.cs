using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace YoutubePlayer
{
    public class Move_Area : MonoBehaviour
    {
        public VideoPlayer videoPlayer;

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            { 
                videoPlayer.Play();
             
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                videoPlayer.Pause();
            }
        }
    }
}




