using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip die;

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        Enemy.OnEnemyDied += DieSFX;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDied -= DieSFX;
    }

    private void DieSFX()
    {
        audioSource.clip = die;
        audioSource.Play();
    }

}
