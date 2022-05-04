using UnityEngine;
using System;

public class Globals : MonoBehaviour
{
    public static Globals instance; 
    public event Action OnPlayerHit;
    public event Action OnGameOver;
    public int playerLifes;
    
    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }

    public void HitPlayer()
    {
        playerLifes--;

        if (OnPlayerHit != null)
            OnPlayerHit();

        if (OnGameOver != null && PlayerIsDead)
            OnGameOver();
    }

    public bool PlayerIsDead { get => playerLifes == 0;}
}
