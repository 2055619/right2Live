using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    [SerializeField]
    AudioClip audioClip;
    [SerializeField]
    GameObject muzzleFlashPosition;
    [SerializeField]
    GameObject muzzleFlash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playFiringEffects() {
        this.GetComponent<AudioSource>().PlayOneShot(audioClip);
        Destroy(Instantiate(muzzleFlash, muzzleFlashPosition.transform), 0.2f);
    }
}
