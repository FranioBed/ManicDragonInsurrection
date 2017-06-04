using UnityEngine;
using System.Collections;

public enum AnimationState
{
    IDLE,
    WALK,
    FIGHT,
    DIE,
}

public class AnimationController : MonoBehaviour {

    private Animator _animator;
    public AnimationState state;

	// Use this for initialization
	void Start () {
        _animator = this.GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        SwitchAnimation();
    }

    //Play animation depending on current state
    void SwitchAnimation() {
        switch (state)
        {
            case AnimationState.IDLE:
                _animator.SetInteger(_animator.GetParameter(0).name, 0);                                  
                break;
            case AnimationState.WALK:
                _animator.SetInteger(_animator.GetParameter(0).name, 1);
                break;
            case AnimationState.FIGHT:
                _animator.SetInteger(_animator.GetParameter(0).name, 2);
                break;
            case AnimationState.DIE:
                _animator.SetInteger(_animator.GetParameter(0).name, 3);
                break;
        }
    }

    public void SetAnimationState(AnimationState _state)
    {
        state = _state;
    }
    
}
