using UnityEngine;

namespace Jinsol
{
    public class DisplayNextButton : MonoBehaviour
    {
        [SerializeField] private GameObject nextButton;

        private void OnEnable()
        {
            Typewriter.FullTextRevealed += ShowNextButton;
        }

        private void OnDisable()
        {
            Typewriter.FullTextRevealed -= ShowNextButton;
        }

        private void ShowNextButton()
        {
            nextButton.SetActive(true);
        }

        private void HideNextButton()
        {
            nextButton.SetActive(false);
        }
    }
}
