using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private BoxCollider2D _boxCollider;
    
    #region Variables: Movements 
    private Vector3 _inputMovement;
    [SerializeField]private float _speed = 12f;
    private float playerOffsetWorldUnit;
    #endregion
   
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        // In otrhographic mode (currently used) the 'size' attribute represents the screen half height in world unit
        float screenHalfWidthInWorldUnits = Camera.main.orthographicSize * Camera.main.aspect;
        float halfPlayerWidth = transform.localScale.x / 2;
        playerOffsetWorldUnit = screenHalfWidthInWorldUnits + halfPlayerWidth;
    }

    void Update()
    {
        if (Globals.instance.PlayerIsDead)
        {
            _boxCollider.enabled = false;
            return;
        }

        HandleMovement();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        _inputMovement = value.ReadValue<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            Globals.instance.HitPlayer();
            StartCoroutine(HitFlashInvulnerability());
            
            if (Globals.instance.PlayerIsDead)
                _animator.SetBool("IsDead", true);
        }
    }

    private void HandleMovement()
    {
        float velocityX = _inputMovement.x * _speed;

        if (velocityX > 0)
            transform.eulerAngles = Vector3.zero;
        else if (velocityX < 0)
            transform.eulerAngles = Vector2.up * 180;

        _animator.SetFloat("MovementSpeed", Mathf.Abs(velocityX));
        transform.Translate(Vector2.right * velocityX * Time.deltaTime, Space.World);

        // assume the camera is centered
        if (transform.position.x < -playerOffsetWorldUnit)
            transform.position = new Vector2(playerOffsetWorldUnit, transform.position.y);
        else if (transform.position.x > playerOffsetWorldUnit)
            transform.position = new Vector2(-playerOffsetWorldUnit, transform.position.y);
    }

    private IEnumerator HitFlashInvulnerability()
    {
        int temp = 0;
        _animator.enabled = false;
        _boxCollider.enabled = false;
        while (temp < 5)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(.08f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.08f);
            temp++;
        }
        _animator.enabled = true;
        _boxCollider.enabled = true;
    }
}
