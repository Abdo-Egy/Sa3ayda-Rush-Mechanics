using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    Animator Animator;
    int _Randomindex;
    private void Start()
    {
        Animator = GetComponent<Animator>();

        _Randomindex = Random.Range(0, 9);
        Animator.SetInteger("index", _Randomindex);

    }
}
