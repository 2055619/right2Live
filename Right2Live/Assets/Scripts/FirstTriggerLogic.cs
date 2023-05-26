using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTriggerLogic : MonoBehaviour
{
    [SerializeField] GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            game.SendMessage("firstTrigger");
            Destroy(gameObject);
        }
    }
}