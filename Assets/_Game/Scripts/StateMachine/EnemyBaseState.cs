using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState 
{
    public abstract void OnStartState(Enemy enemy);
    public abstract void UpdateState(Enemy enemy);
}
