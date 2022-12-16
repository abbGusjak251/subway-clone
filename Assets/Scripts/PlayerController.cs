using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum HitX {Left,Mid,Right,None}
public enum HitY {UP,Mid,Down,None}
public enum HitZ {Forward,Mid,Backward,None}
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpForce = 5f;
    private float y;   
    private Vector3 positionOrigin;
    private Vector3[] positions = new Vector3[3];
    private int positionIndex = 0;
    public float railDistance = 2f; 
    [HideInInspector]
    private bool swipeLeft, swipeRight, swipeDown, swipeUp;
    private bool grounded;
    private string powerUp;
    private Animator m_Animator;
    private float HalfHeight;
    public float DodgeSpeed = .1f;
    public enum State {
        Idle,
        Running,
        Dead
    };
    public bool InJump;
    public bool InRoll;
    public State state = State.Running;
    private BoxCollider col;
    public HitX hitX = HitX.None;
    public HitY hitY = HitY.None;   
    public HitZ hitZ = HitZ.None;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>(); 
        col = GetComponent<BoxCollider>();

        positionOrigin = transform.position;

        // Add possible horizontal positions

        positions[0] = positionOrigin - Vector3.left*railDistance;
        positions[1] = positionOrigin;
        positions[2] = positionOrigin + Vector3.left*railDistance;
    }
    void Update()
    {

        swipeLeft = Input.GetKeyDown("left") || Input.GetKeyDown(KeyCode.A);
        swipeRight = Input.GetKeyDown("right") || Input.GetKeyDown(KeyCode.D);
        swipeDown = Input.GetKeyDown("down") || Input.GetKeyDown(KeyCode.S);
        swipeUp = Input.GetKeyDown("up") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("space");
        // Toggle states (Debug)

        if(Input.GetKeyDown(KeyCode.T)) {

            if(state == State.Running) {
                state = State.Dead;
            } else {
                state = State.Running;
            }
        }

        if(state == State.Dead) return;
        if(state == State.Running) {
            // Jump
            if(swipeUp) {
                Jump();                
            }
            // Horizontal Movement
            
            if(swipeLeft) {
                if(positionIndex < positions.Length-1) {
                    positionIndex++;
                    if(!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("rolling")&& grounded)  {
                    
                        m_Animator.Play("dodgeLeft");
                    }    
                }
            }
            if(swipeRight) {
                if(positionIndex > 0) {
                    positionIndex--;
                     if(!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("rolling") && grounded){
                    
                        m_Animator.Play("dodgeRight");
                    }
                }
                
            }
            if(swipeDown){
                Roll();
            }

            if(!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("rolling")){
                col.size = new Vector3(.4f,1.8f,.3f);
                col.center = new Vector3(0f,.9f,0f);
            }
            if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("rolling")){
                col.size = new Vector3(.4f,.9f,.3f);
                col.center = new Vector3(0f,.45f,0f);
            }
           
            
            Vector3 toPos = new Vector3(positions[positionIndex].x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, toPos, DodgeSpeed);
        }
    } 





    void Jump() {
        if(grounded){
            y = jumpForce;
            m_Animator.Play("jump");
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }
    }



    void OnCollisionEnter(Collision other) {

        if(other.collider.tag == "Ground") {
            m_Animator.ResetTrigger("dodgeLeft");
            m_Animator.ResetTrigger("dodgeRight");
            m_Animator.ResetTrigger("jump");
            if(!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("rolling")){
                m_Animator.Play("landing");
            }
            
            grounded = true;
            
        }   
    }



    void OnCollisionExit(Collision other) {
        if(other.collider.tag == "Ground") {
            grounded = false;
        }
    }
    
    void Roll(){
       
        if (!grounded){
            rb.AddForce(-jumpForce * Vector3.up, ForceMode.Impulse);
            m_Animator.Play("rolling");
           
        }
        else if(grounded){
            m_Animator.Play("rolling");
            
          
        }
    }

public void OnCharacterColliderHit(Collider col)
{
    hitX = GetHitX(col);
}

public HitX GetHitX(Collider col)
{
    Bounds col_bounds = col.bounds;
    float min_x = Mathf.Max(col_bounds.min.x);
    float max_x = Mathf.Min(col_bounds.min.x);
    float avrage = (min_x + max_x) / 2f - col.bounds.min.x;
    HitX hit;
    if (avrage>col_bounds.size.x - 0.33f)
        hit = HitX.Right;
    else if(avrage<0.33f)
        hit = HitX.Left;
    else 
        hit = HitX.Mid;
    return hit;
}
}

