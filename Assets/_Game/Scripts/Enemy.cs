using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public LayerMask groundLayer;
    public NavMeshAgent navMeshAgent;
    public List<Brick> bricks = new List<Brick>();
    public GameObject brickSpawner;
    private EndLevel endLevel;
    public EnemyBaseState currentState;
    public BuildBridgeState BuildBridgeState = new BuildBridgeState();
    public SeekBrickState SeekBrickState = new SeekBrickState();
    private void Start()
    {
        currentState = SeekBrickState;
        endLevel = FindObjectOfType<EndLevel>();
        endLevel.OnEndLevelAction += EndGame;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (gameEnd) return;
        currentState.UpdateState(this);
    }
    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.OnStartState(this);
    }
}