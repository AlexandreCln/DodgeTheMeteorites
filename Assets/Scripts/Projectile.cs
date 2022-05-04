using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]private float _fallingSpeed = 5f;

    private float _verticalPosToDestroy;

    private void Start()
    {
        float _bottomScreen = -Camera.main.orthographicSize;
        float _projectileHeight = transform.localScale.y;
        _verticalPosToDestroy = _bottomScreen - (_projectileHeight/4);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _fallingSpeed * Time.deltaTime);

        if (transform.position.y < _verticalPosToDestroy)
            Destroy(gameObject);
    }
}
