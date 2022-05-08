using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    private Globals _globals;
    [SerializeField] private int _secondsDelayGameOver;
    [SerializeField] private AudioSource _gameOverAudio;

    void Start()
    {
        _globals = FindObjectOfType<Globals>();
        _globals.OnGameOver += GameOver;
    }
    
    void GameOver()
    {
        Debug.Log("GAME OVER");
        StartCoroutine(PlayGameOverAfterTime());
    }

    IEnumerator PlayGameOverAfterTime()
    {
        yield return new WaitForSeconds(_secondsDelayGameOver);
        _gameOverAudio.Play();
        yield return new WaitForSeconds(_gameOverAudio.clip.length);
        Debug.Log("GAME OVER SCREEN");
    }
}
