using UnityEngine;
using System.Collections;


public interface IMenhirState
{
	
	void UpdateState();
	
	void OnTriggerEnter (Collider other);
	
	void ToIdleState();
	
	void ToSingState();
}

public class IdleState : IMenhirState 
	
{
	private readonly Menhir menhir;
	
	public IdleState (Menhir stateMenhir)
	{
		menhir = stateMenhir;
	}
	
	public void UpdateState()
	{
		//Play mySong
		//Debug.Log(menhir.gameObject.tag + ": I idle now");

	}
	
	public void OnTriggerEnter (Collider other)
	{
		
	}
	
	public void ToIdleState()
	{
		Debug.Log ("Can't transition to same state: " + menhir.currentState);
	}
	
	public void ToSingState()
	{
		menhir.currentState = menhir.singState;
	}
	
	
}

public class SingState : IMenhirState 
	
{
	
	private readonly Menhir menhir;
	
	
	public SingState (Menhir stateMenhir)
	{
		menhir = stateMenhir;

	}
	
	public void UpdateState()
	{
		//Debug.Log(menhir.gameObject.tag + ": I singing now");
		//Play 2 songs sequentially and If I am correct light up. Then go to idle state
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
		Debug.Log ("Can't transition to same state: " + menhir.currentState);
		
	}
	
	
}

public class Menhir : MonoBehaviour 
{
	
	public MeshRenderer meshRendererFlag;
	public bool correct;
	public bool completed;
	AudioSource audio;

	
	
    [HideInInspector] public IMenhirState currentState;
    [HideInInspector] public SingState singState;
    [HideInInspector] public IdleState idleState;


    private void Awake()
    {
        singState = new SingState (this);
        idleState = new IdleState (this);
		audio = GetComponent<AudioSource>();
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

	public void Sing(){
		currentState.ToSingState ();
	}

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter (other);
    }
}
