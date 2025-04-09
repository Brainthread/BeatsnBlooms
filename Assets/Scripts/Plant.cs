using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private ProjectileEmitter emitter;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioSource shootAudio;
    public void Activate()
    {
        print("Hell yeah, I am activated!!!");
        shootAudio.clip = shootSound;
        shootAudio.Play();
        emitter.FireProjectile();
    }
}
