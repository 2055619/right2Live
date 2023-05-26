using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyLogic : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] AudioClip damageAudioClip;
    [SerializeField] AudioClip deathAudioClip;
    [SerializeField] float deathAnimationDuration = 0.5f;
    
    float currentDeathAnimationTime = 0;
    AudioSource audioSource;
    float deathAnimationYPositionStart;
    bool deathAnimationStarted = false;
    
    [SerializeField] float animationSpeedMultiplier = 2;
    [SerializeField] GameObject bloodEffect;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathAnimationStarted) {
            playDeathAnimation();
        } 
    }
    public void takeDamage(int healthChange)
    {
        _health = _health - healthChange;
        if (_health <= 0) {
            killEnnemy();
            this.audioSource.PlayOneShot(deathAudioClip);
        }
        else {
            this.audioSource.PlayOneShot(damageAudioClip);
        }
    }
    public void playBulletWound(Vector3 position) {
        Destroy(Instantiate(bloodEffect, position, Quaternion.Euler(Random.Range(-45, 45), Random.Range(-180, 180), Random.Range(-180, 180))), 2);
    }
    private void killEnnemy()
    {
        prepareDeathAnimation();
        playDeathAnimation();
        Destroy(this.gameObject, deathAnimationDuration);
    }

    private void prepareDeathAnimation() {
        deathAnimationYPositionStart = this.transform.position.y; //Je met ce code ici car c'est l'équivalent du "Start" de DeathAnimation
    }
    private void playDeathAnimation()
    {
        deathAnimationStarted = true;
        if (currentDeathAnimationTime * animationSpeedMultiplier < deathAnimationDuration) { //currentDeathAnimationTime * 2 permet de faire que le zombie reste sur le sol pour la moitié du temps
            currentDeathAnimationTime = currentDeathAnimationTime + Time.deltaTime;
        }
        this.transform.rotation = Quaternion.Euler((currentDeathAnimationTime * animationSpeedMultiplier / deathAnimationDuration) * 90, this.transform.rotation.y, this.transform.rotation.z);
        this.transform.position = new Vector3(this.transform.position.x, deathAnimationYPositionStart - ((currentDeathAnimationTime * animationSpeedMultiplier / deathAnimationDuration) * 0.5f), this.transform.position.z);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!deathAnimationStarted) {
            other.SendMessage("DamagePlayer", 20);
        }
    }
}
