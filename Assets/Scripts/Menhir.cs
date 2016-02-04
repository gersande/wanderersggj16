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
		if (!menhir.audioSource.isPlaying) {
			menhir.audioSource.minDistance = 1;
			menhir.audioSource.PlayOneShot (menhir.call);
		}
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
		if ( menhir.audioSource.clip != menhir.call) {
				menhir.audioSource.clip = menhir.call;
				menhir.audioSource.minDistance = 100;
				menhir.audioSource.PlayOneShot (menhir.call);
				Debug.Log (menhir.tag + " Playing: " + menhir.audioSource.clip.name);
		}else{
			if(!menhir.audioSource.isPlaying){
				Debug.Log(menhir.gameObject.tag + ": I go idle now");
				if(menhir.correct) {
					menhir.completed = true;
					Debug.Log ("Found!");
				}
				menhir.audioSource.clip = menhir.song;
				menhir.audioSource.PlayOneShot (menhir.song);
				Debug.Log (menhir.tag + " Playing: " + menhir.audioSource.clip.name);
				ToIdleState();
			}
		}


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
	public AudioSource audioSource;
	public AudioClip call;
	public AudioClip song;

	
	
    [HideInInspector] public IMenhirState currentState;
    [HideInInspector] public SingState singState;
    [HideInInspector] public IdleState idleState;


    private void Awake()
    {
        singState = new SingState (this);
        idleState = new IdleState (this);
		audioSource = gameObject.GetComponent<AudioSource> ();
		audioSource.clip = song;
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
