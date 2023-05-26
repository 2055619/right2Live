using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMainMenuAnimation : MonoBehaviour
{

    [SerializeField] private GameObject waypoints;
    
    private NavMeshAgent _meshAgent;
    private Animator _animator;

    private int _currentWp;
    private List<Transform> _wpList = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
            
        if (waypoints && _meshAgent)
        {
            foreach (Transform t in waypoints.GetComponentsInChildren<Transform>())
            {
                _wpList.Add(t);
            }

            _currentWp = 0;
            _meshAgent.SetDestination(_wpList[_currentWp].position);
            _animator.Play("Z_Run_InPlace");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints && _meshAgent && !_meshAgent.hasPath)
        {
            if (_currentWp == _wpList.Count -1)
            {
                _currentWp = 0;
            }
            
            _meshAgent.SetDestination(_wpList[++_currentWp].position);
            
            if (_currentWp == _wpList.Count -1)
            {
                _animator.Play("Z_Attack");
            }
            else
            {
                _animator.Play("Z_Run_InPlace");
            }

            
        }
    }
}
