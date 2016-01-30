using UnityEngine;
using System.Collections;

public class StatePatternMenhir : MonoBehaviour 
{
    
    public MeshRenderer meshRendererFlag;
    public GameObject playerTgt;
	public float sightRange = 20f;


    [HideInInspector] public IMenhirState currentState;
    [HideInInspector] public SingState singState;
    [HideInInspector] public IdleState idleState;


    private void Awake()
    {
        singState = new SingState (this);
        idleState = new IdleState (this);
        playerTgt = GameObject.FindWithTag("Player");

    }

    // Use this for initialization
    void Start () 
    {
        currentState = idleState;
    }
    
    // Update is called once per frame
    void Update () 
    {
        currentState.UpdateState ();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter (other);
    }
}

public interface IMenhirState
{

    void UpdateState();

    void OnTriggerEnter (Collider other);

    void ToIdleState();

    void ToSingState();
}

public class IdleState : IMenhirState 
    
{
    private readonly StatePatternMenhir menhir;
    
    public IdleState (StatePatternMenhir StatePatternMenhir)
    {
        menhir = StatePatternMenhir;
    }

    public void UpdateState()
    {
        Look ();
    }
    
    public void OnTriggerEnter (Collider other)
    {
        
    }
    
    public void ToIdleState()
    {
        Debug.Log ("Can't transition to same state");
    }
    
    public void ToSingState()
    {
        menhir.currentState = menhir.singState;
    }

    private void Look()
    {
        RaycastHit hit;
		if (Physics.Raycast (menhir.playerTgt.transform.position, menhir.playerTgt.transform.forward, out hit, menhir.sightRange) &&  hit.collider.CompareTag(menhir.gameObject.tag)) {
            ToSingState();
        }
    }

}

public class SingState : IMenhirState 
    
{

    private readonly StatePatternMenhir menhir;

    
    public SingState (StatePatternMenhir StatePatternMenhir)
    {
        menhir = StatePatternMenhir;
    }
    
    public void UpdateState()
    {
        Look ();
    }
    
    public void OnTriggerEnter (Collider other)
    {
        
    }
    
    public void ToIdleState()
    {
        menhir.currentState = menhir.idleState;
    }
    
    public void ToSingState()
    {
        
    }

    private void Look()
    {
        RaycastHit hit;
		if (Physics.Raycast (menhir.playerTgt.transform.position, menhir.playerTgt.transform.forward, out hit, menhir.sightRange) &&  hit.collider.CompareTag(menhir.gameObject.tag)) {
            
        }else{
            ToIdleState();
        }
    }
   
}