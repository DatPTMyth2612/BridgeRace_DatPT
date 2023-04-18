using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBrickState : EnemyBaseState
{
    public override void OnStartState(Enemy enemy)
    {
        CreateBrickList(enemy);
        MoveToBrick(enemy);
    }

    public override void UpdateState(Enemy enemy)
    {
        if (enemy.navMeshAgent.remainingDistance < 0.1f)
        {
            if (enemy.collectedBrick.Count >= 7)
            {
                enemy.SwitchState(enemy.BuildBridgeState);
            }
            else
            {
                MoveToBrick(enemy);
            }
        }
    }
    private Vector3 GetClosestBrickPosition(Enemy enemy)
    {
        Vector3 closestBrickPosition = enemy.transform.position;
        float distanceToClosestBrick = 100000000f;
        for (int i = 0; i < enemy.bricks.Count; i++)
        {
            if (enemy.bricks[i].gameObject.active && enemy.bricks[i].brickColor == enemy.characterColor)
            {
                float distanceToBrick = Vector3.Distance(enemy.transform.position, enemy.bricks[i].transform.position);
                if (distanceToBrick < distanceToClosestBrick)
                {
                    distanceToClosestBrick = distanceToBrick;
                    closestBrickPosition = enemy.bricks[i].transform.position;
                }
            }
        }
        return closestBrickPosition;
    }
    private void MoveToBrick(Enemy enemy)
    {
        CreateBrickList(enemy);
        enemy.navMeshAgent.SetDestination(GetClosestBrickPosition(enemy));
        enemy.animator.SetBool("isRunning", true);
    }
    private void CreateBrickList(Enemy enemy)
    {
        enemy.bricks.Clear();
        enemy.brickSpawner = GameObject.Find("BrickSpawnerGround" + enemy.currentGround.ToString());
        if (enemy.brickSpawner != null)
        {
            for (int i = 0; i < enemy.brickSpawner.transform.childCount - 1; i++)
            {
                Brick brick = enemy.brickSpawner.transform.GetChild(i).GetComponent<Brick>();
                enemy.bricks.Add(brick);
            }
        }
    }
}
