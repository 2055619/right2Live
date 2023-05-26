using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieAnimation : MonoBehaviour
{ 
    
    private enum ZombieState
    {
        Chase,
        Attack,
        Death
    }
    
    [SerializeField] private float _attackDistance = 3.000f;
    
    
    private NavMeshAgent _agent;
    private Animator _animator; // Composante pour l'animation du zombie.
    private GameObject _player;
    private ZombieState _currentState;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        
        _animator.Play("Z_Idle");
        _currentState = ZombieState.Chase;
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
        
        switch (_currentState)
        {
            case ZombieState.Death:
                Death();
                break;
            case ZombieState.Chase:
                Chase();
                break;
            case ZombieState.Attack:
                Attack();
                break;
        }
    }
    
    // Gestion de l'état du zombie selon la situation.
    private void ManageState()
    {
        if (_currentState == ZombieState.Death)
        {
            return;
        }

        float distance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance <= _attackDistance)
        {
            _currentState = ZombieState.Attack;
        }
        else
        {
            _currentState = ZombieState.Chase;
        }
    }

    // Lorsque le zombie chasse le joueur, il se dirige vers lui.
    private void Chase()
    {
        // Joue l'animation de course.
        _animator.Play("Z_Run_InPlace");
        _agent.SetDestination(_player.transform.position);
    }
    
    // Lorsque le zombie est assez proche, il attaque le joueur, il se dirige vers lui et le regarde.
    private void Attack()
    {
        var position = _player.transform.position;

        // Joue l'animation d'attaque.
        _animator.Play("Z_Attack");
        transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
        _agent.SetDestination(position);
}
    
    // Lorsque le zombie est mort, il tombe en arrière et se détruit.
    private void Death()
    {
        // Joue l'animation de mort.
        _animator.Play("Z_FallingBack");
        Destroy(gameObject, 1f);
    }

    public void DeathAnimationStart()
    {
        _currentState = ZombieState.Death;
        _agent.isStopped = true;
        Death();
    }
}


