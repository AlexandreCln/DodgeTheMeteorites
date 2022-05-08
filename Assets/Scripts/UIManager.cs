using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform _heartsContainer;
    [SerializeField] private GameObject _heartPrefab;
    private List<GameObject> _hearts;
    private Globals _globals;

    void Start()
    {
        _globals = FindObjectOfType<Globals>();
        _globals.OnPlayerHit += PlayerHit;
        
        _hearts = new List<GameObject>(Globals.instance.playerLifes);
        for (int i = 0; i < Globals.instance.playerLifes; i++)
            _hearts.Add(Instantiate(_heartPrefab, _heartsContainer));
    }

    void PlayerHit()
    {
        GameObject heart = _hearts[_hearts.Count - 1];
        _hearts.Remove(heart);
        Destroy(heart);
    }
}
