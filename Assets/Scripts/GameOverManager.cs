using UnityEngine;
using System.Threading.Tasks;

public class GameOverManager : MonoBehaviour
{
    private Globals _globals;
    [SerializeField] private int _secondsBeforeGameOver;


    void Start()
    {
        _globals = FindObjectOfType<Globals>();
        _globals.OnGameOver += GameOver;
    }
    
    void GameOver()
    {
        Debug.Log("GAME OVER");

        Task.Delay(1000 * _secondsBeforeGameOver).ContinueWith(t => Debug.Log("GAME OVER SCREEN"));
    }
}
