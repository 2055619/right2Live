using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLogic : MonoBehaviour
{
    
    [SerializeField] float _health = 100f;
    [SerializeField] float _dealthDamage = 10f;
    [SerializeField] float _attackTime = 1f;
    
    [SerializeField] AudioClip damageAudioClip;
    [SerializeField] AudioClip deathAudioClip;
    
    [SerializeField] GameObject bloodEffect;

    private AudioSource audioSource;
    private float _lastAttackTime = -1f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_lastAttackTime >= 0)
        {
            _lastAttackTime += Time.deltaTime;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _lastAttackTime >= _attackTime)
        {
            other.gameObject.SendMessage("DamagePlayer", _dealthDamage);
            _lastAttackTime = -1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _lastAttackTime = _attackTime;
        }
    }
    
    public void DamageZombie(float damage)
    {
        _health -= damage;
        
        if (_health <= 0) {
            GetComponent<ZombieAnimation>().DeathAnimationStart();
            this.audioSource.PlayOneShot(deathAudioClip);
        }
        else {
            this.audioSource.PlayOneShot(damageAudioClip);
        }
    }
    
    public void playBulletWound(Vector3 position) {
        Destroy(
            Instantiate(bloodEffect, 
                position, 
                Quaternion.Euler(Random.Range(-45, 45), 
                    Random.Range(-180, 180), 
                    Random.Range(-180, 180)
                    )
                ), 
            2);
    }

}
