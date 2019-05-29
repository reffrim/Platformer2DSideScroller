using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //Singleton pattern
    private static PlayerState _instance;
    public static PlayerState Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("PlayerState").AddComponent<PlayerState>();
            return _instance;
        }
    }

    public Attack Attack;
    public Vertical Vertical;
    public Horizontal Horizontal;
    public LifeCondition LifeCondition;
    public DirectionFacing DirectionFacing;
}

public enum Horizontal
{
    Idle = 0,
    MovingLeft = -1,
    MovingRight = 1
}

public enum Vertical
{
    Grounded,
    Airborn
}

public enum DirectionFacing
{
    Left = -1,
    Right = 1
}

public enum Attack
{
    Passive,
    Punch,
    Projectile
}

public enum LifeCondition
{
    Alive,
    Dead
}
