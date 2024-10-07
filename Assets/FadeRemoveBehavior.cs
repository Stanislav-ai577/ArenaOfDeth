using UnityEngine;

public class FadeRemoveBehavior : StateMachineBehaviour
{
    [SerializeField] private float _fadeTime = 0.5f;
    private SpriteRenderer _spriteRenderer;
    private GameObject _objectToRemove;
    private Color _startColor;
    private float _timeElapsed = 0f;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _spriteRenderer = animator.GetComponent<SpriteRenderer>();
        _objectToRemove = animator.GetComponent<GameObject>();
        _startColor = _spriteRenderer.color;
        _timeElapsed = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed += Time.deltaTime;
        float newAlpha = _startColor.a * (1 - (_timeElapsed / _fadeTime));
        _spriteRenderer.color = new Color(_startColor.r, _startColor.g, _startColor.b, newAlpha);
        if (_timeElapsed > _fadeTime)
        {
            Destroy(_objectToRemove);   
        }
    }
}
