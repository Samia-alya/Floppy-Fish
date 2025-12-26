using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private Vector3 direction;
    public float floatForce = 8.0f;     
    public float diveStrength = 3.0f;   
    public float drag = 1.0f;           
    public float maxUpSpeed = 10f;     
    public float maxDownSpeed = -6f; 
    


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            direction.y = -diveStrength;
        }

        if(Input.touchCount > 0){
            Touch touch  = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began){
                direction.y = -diveStrength;
            }
        }

        direction.y += floatForce * Time.deltaTime;

        direction.y = Mathf.Clamp(direction.y, maxDownSpeed, maxUpSpeed);

        direction.y *= 1f - drag * Time.deltaTime;

        transform.position += direction * Time.deltaTime;

    }

     private void  AnimateSprite()
    {
        spriteIndex++;
        if(spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = sprites[spriteIndex];
        
    }

    private void  OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if(other.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("HIT OBSTACLE");
            FindObjectOfType<GameManager>().GameOver();
        } else if(other.gameObject.CompareTag("Scoring"))
        {
            Debug.Log("score");
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
