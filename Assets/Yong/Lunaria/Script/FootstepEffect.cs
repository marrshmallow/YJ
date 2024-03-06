using UnityEngine;

public class FootstepEffect : MonoBehaviour
{
    CameraController ss;

    public ParticleSystem footstepParticles;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Ground"))
        {
            
            // ∂•ø° ¥Í¿ª ∂ß ¿Ã∆Â∆Æ∏¶ »∞º∫»≠
            footstepParticles.Play();

          
        }
    }
}