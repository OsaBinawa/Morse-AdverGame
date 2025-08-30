using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip die, damaged;

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        Enemy.OnEnemyDied += DieSFX;
        PlayerController.OnPlayerDamaged += DieSFX;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDied -= DieSFX;
        PlayerController.OnPlayerDamaged -= DieSFX;
    }

    private void DieSFX()
    {
        audioSource.clip = die;
        audioSource.Play();
    }
}
