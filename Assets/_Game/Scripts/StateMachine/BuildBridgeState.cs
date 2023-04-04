using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridgeState : EnemyBaseState
{
    public override void OnStartState(Enemy enemy)
    {
        MoveToNextLevel(enemy);
    }

    public override void UpdateState(Enemy enemy)
    {
        if (enemy.collectedBrick.Count == 0)
        {
            enemy.SwitchState(enemy.SeekBrickState);
        }
    }
    private void MoveToNextLevel(Enemy enemy)
    {
        int nextLevel = enemy.currentLevel + 1;
        RaycastHit nextGroundLevel;
        Vector3 targetPosition = enemy.transform.position;
        for (int i = 0; i < 100; i++)
        {
            if (Physics.Raycast(enemy.transform.position + Vector3.forward * i + new Vector3(0, 1000f, 0), Vector3.down, out nextGroundLevel, Mathf.Infinity, enemy.groundLayer))
            {
                if (nextGroundLevel.transform.name == "Ground " + nextLevel.ToString())
                {
                    targetPosition = nextGroundLevel.point;
                    break;
                }
            }
        }
        enemy.navMeshAgent.SetDestination(targetPosition);
    }
}
