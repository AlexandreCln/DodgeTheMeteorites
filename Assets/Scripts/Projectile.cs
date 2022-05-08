using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]private float _fallingSpeedMin = 7f;
    [SerializeField]private float _fallingSpeedMax = 15f;

    private float _verticalPosToDestroy;

    private void Start()
    {
        float _bottomScreen = -Camera.main.orthographicSize;
        float _projectileHeight = transform.localScale.y;
        _verticalPosToDestroy = _bottomScreen - (_projectileHeight/4);
    }

    void Update()
    {
        float fallingSpeed = Mathf.Lerp(_fallingSpeedMin, _fallingSpeedMax, Difficulty.GetDifficultyPercent());
        transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime);

        if (transform.position.y < _verticalPosToDestroy)
            Destroy(gameObject);
    }
}
