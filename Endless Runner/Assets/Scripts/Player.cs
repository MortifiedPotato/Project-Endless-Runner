using UnityEngine;

public class Player : MonoBehaviour, IControls
{
    Rigidbody rb;

    public float SlideSpeed = 5f;
    public int jumpForce = 5;

    Vector3 targetPos;

    [SerializeField] LayerMask groundLayer;
    bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, SlideSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer);
    }

    public void SlideRight()
    {
        if (targetPos.x < 1)
        {
            targetPos.x++;
        }
    }

    public void SlideLeft()
    {
        if (targetPos.x > -1)
        {
            targetPos.x--;
        }
    }

    public void Jump()
    {
        if (!grounded) return;
        rb.velocity = Vector3.up * jumpForce;
    }

    public void Duck()
    {
    }

    public void Pause()
    {
        UIManager.instance.TogglePause();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            UIManager.instance.GameOver();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Challenge"))
        {
            if (Random.Range(1, 3) == 1)
            {
                if (LevelManager.instance.obstaclePercentage < 40)
                {
                    LevelManager.instance.obstaclePercentage++;
                }
            }

            if (Random.Range(1, 3) == 1)
            {
                if (LevelManager.instance.tileSpeed < 20)
                {
                    LevelManager.instance.tileSpeed++;
                }
            }
            LevelManager.instance.playerScore++;
            UIManager.instance.UpdateUIValues();
        }
    }
}

public interface IControls
{
    void SlideRight();
    void SlideLeft();
    void Jump();
    void Duck();
    void Pause();
}
