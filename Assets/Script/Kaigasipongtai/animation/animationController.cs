using UnityEngine;

public class animationController : MonoBehaviour
{
    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            _animator.SetBool("MouseNotHold", false);
        }
        else
        {
            _animator.SetBool("MouseNotHold", true);
        }
    }
}
