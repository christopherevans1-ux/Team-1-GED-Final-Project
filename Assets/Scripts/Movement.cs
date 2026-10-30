using Chapter.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    public GameObject player;
    public GameObject playerBody;
    public GameObject goal;
    public bool isGrounded;
    public Rigidbody2D rb;
    public float speed = 10f;
    public float walkSpeed = 5f;
    public Vector2 jumpForce;
    public bool isDead = false;
    public bool hasWon = false;
    public GameManager gameManager;
    public bool jumpedEnemy = false;
    private Dictionary<GameAction, ICommand> commands;
    public bool canUndo = true;
    public Vector3 pastPlayerPos;
    public float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        jumpForce = transform.up * speed;
        commands = new Dictionary<GameAction, ICommand>
        {
            { GameAction.MoveRight, new ActionCommands(MoveRight) },
            { GameAction.MoveLeft, new ActionCommands(MoveLeft) },
            { GameAction.Jump, new ActionCommands(Jump) }
        };
    }

    // Update is called once per frame
    void Update()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        if(!isDead && !hasWon && !gameManager.isPaused && !KeyRebineMenu.isOpen)
        {
            if(Input.GetKey(KeyBindings.MoveRight))
            {
                commands[GameAction.MoveRight].Execute();
            }
            if (Input.GetKey(KeyBindings.MoveLeft))
            {
                commands[GameAction.MoveLeft].Execute();
            }
            if (Input.GetKeyDown(KeyBindings.Jump) && isGrounded)
            {
                commands[GameAction.Jump].Execute();
            }
        }
        player.transform.position = Vector3.ClampMagnitude(player.transform.position, 1000);

        if (isDead)
        {
            playerBody.SetActive(false);
        }

        BoolReset();

        timer += Time.deltaTime;
        if (timer >= 2)
        {
            pastPlayerPos = player.transform.position;
            timer = 0;
        }

        URCommand trackMovement = new URMoveCommand(player.transform, pastPlayerPos);
        URCommandManager.Instance.AddAndExecuteCommand(trackMovement);
        if (Input.GetKeyDown(KeyBindings.Undo) && canUndo)
        {
            URCommandManager.Instance.Undo();
            canUndo = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && (player.transform.position.y - collision.gameObject.transform.position.y <= 0.2f) && collision.gameObject.GetComponent<Enemy>().enemyDead == false)
        {
            Debug.Log("You Died to an enemy");
            isDead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Goal")
        {
            hasWon = true;
        }
        if (collision.gameObject.name == "Void")
        {
            Debug.Log("You died to the void");
            isDead = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall" && (player.transform.position.y - collision.gameObject.transform.position.y >=0.2f))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void BoolReset()
    {
        if(jumpedEnemy == true){
            jumpedEnemy = false;
        }
    }

    public void MoveRight()
    {
        rb.linearVelocity = new Vector2(walkSpeed, rb.linearVelocity.y);
    }
    public void MoveLeft()
    {
        rb.linearVelocity = new Vector2(-walkSpeed, rb.linearVelocity.y);
    }
    public void Jump()
    {
        rb.AddForce(jumpForce, (ForceMode2D)ForceMode.Impulse);
    }
}
