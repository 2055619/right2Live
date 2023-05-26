using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ShootingLogic : MonoBehaviour
{
    [SerializeField] GameObject dustParticle;
    [SerializeField] float _damage = 20f;
    [SerializeField] float _initMag = 10f;
    [SerializeField] float _reloadTime = 3f;
    [SerializeField] private TextMeshProUGUI amoUIText;

    float _amoMagazine;
    private bool isPaused = false;
    private bool isReloading = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _amoMagazine = _initMag;
        amoUIText.SetText(_amoMagazine + "  /  " + _initMag);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused) {
            if (isReloading)
            {
                // print("In reloading");
            }
            else if (Input.GetKey(KeyCode.R))
            {
                StartCoroutine(reload());
            }
            else if (Input.GetButtonDown("Fire1")) {
                shootBullet();
            }
        }

        
        
    }

    void shootBullet() {
        if (_amoMagazine <= 0) {
            StartCoroutine(reload());
            return;
        }
        GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponLogic>().SendMessage("playFiringEffects");
        RaycastHit raycastHit = shootRaycast();
        if (raycastHit.collider.tag == "Ennemy") {
            raycastHit.collider.GetComponent<ZombieLogic>().SendMessage("DamageZombie", _damage);
            raycastHit.collider.GetComponent<ZombieLogic>().SendMessage("playBulletWound", raycastHit.transform.position);
        }
        if (raycastHit.collider.tag == "Ground") {
            Destroy(Instantiate(dustParticle, raycastHit.point, Quaternion.Euler(-90, 0, 0)), 5);
        }
        _amoMagazine--;
        amoUIText.SetText(_amoMagazine + "  /  " + _initMag);
    }

    RaycastHit shootRaycast() {
        RaycastHit raycastHit;
        Physics.Raycast(transform.position, transform.forward, out raycastHit);
        return raycastHit;
    }

    public void setIsPaused(bool boolean) {
        isPaused = boolean;
    }
    
    private IEnumerator reload()
    {
        isReloading = true;
        amoUIText.SetText("Reloading...");
        
        yield return new WaitForSecondsRealtime(_reloadTime);
        _amoMagazine = _initMag;
        
        amoUIText.SetText(_amoMagazine + "  /  " + _initMag);
        isReloading = false;
    }
}
