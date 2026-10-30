using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyBody;
    public bool enemyDead = false;
    public float speed = -0.01f;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        player = GameObject.Find("Player");
        bc.GetComponent<BoxCollider2D>();
        rb.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(enemyDead == false)
        {
            gameObject.transform.position += new Vector3(speed, 0, 0);
            if (rb.linearVelocity.magnitude > speed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * speed;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            speed *= -1;
        }
        if(collision.gameObject == player && player.transform.position.y > gameObject.transform.position.y)
        {
            bc.enabled = false;
            enemyDead = true;
            enemyBody.SetActive(false);
            player.GetComponent<Movement>().rb.AddForce(player.GetComponent<Movement>().jumpForce, (ForceMode2D)ForceMode.Impulse);
            player.GetComponent<Movement>().jumpedEnemy = true;
        }
    }
}


