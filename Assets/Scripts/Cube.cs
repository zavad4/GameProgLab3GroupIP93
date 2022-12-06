using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] float jumpForce = 1.0f;
    Rigidbody player;
    Vector3 movement;
    bool isOnTheGround;
    bool isRunning;
    private PointsManager pointsManager;
    private StateMachine stateMachine;

    private IdleState idleState;
    private JumpingState jumpingState;
    private RunningState runningState;
    // Start is called before the first frame update
    void Start()
    {
        isOnTheGround = true;
        player = GetComponent<Rigidbody>();
        pointsManager = FindObjectOfType<PointsManager>();
        stateMachine = gameObject.AddComponent<StateMachine>();

        idleState = new IdleState(this.player);
        jumpingState = new JumpingState(this.player);
        runningState = new RunningState(this.player);

        if (stateMachine != null) Debug.Log("machine");
        if (idleState != null) Debug.Log("idle");
        if (jumpingState != null) Debug.Log("jump");
        if (runningState != null) Debug.Log("run");
        stateMachine.Initialize(idleState);
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.Update();
        if (Input.GetAxis("Horizontal") != 0)
        {
            isRunning = true;
            if (stateMachine.CurrentState != runningState) stateMachine.ChangeState(runningState);
            movement.x = moveSpeed * Input.GetAxis("Horizontal");
            player.velocity = movement;
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            isRunning = true;
            if(stateMachine.CurrentState != runningState) stateMachine.ChangeState(runningState);
            movement.z = moveSpeed * Input.GetAxis("Vertical");
            player.velocity = movement;
        }

        if (Input.GetKey(KeyCode.Space) && isOnTheGround)
        {
            if (stateMachine.CurrentState != idleState) stateMachine.ChangeState(jumpingState);
            // player.AddRelativeTorque(new Vector3(0, 100f, 0), );
            player.AddForce(new Vector3(0, jumpForce, 0));
        }

        if (Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (stateMachine.CurrentState != idleState && isOnTheGround && !isRunning) stateMachine.ChangeState(idleState);

        isRunning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            pointsManager.AddPoint();
        }
        if (this.CompareTag("Player") && other.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            stateMachine.ChangeState(idleState);
            isOnTheGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            stateMachine.ChangeState(jumpingState);
            isOnTheGround = false;
        }
    }
}
