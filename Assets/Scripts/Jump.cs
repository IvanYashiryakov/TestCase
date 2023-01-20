using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Animator _visualModelAnimator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _visualModelAnimator.SetTrigger("Jump");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _visualModelAnimator.ResetTrigger("Jump");
        }
    }
}
