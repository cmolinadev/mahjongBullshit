using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UdaetaView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public enum UdaetaState
    {
        Wow,
        Idle,
        Smoke,
        No,
        Clap,
    }

    public void PlayEmote(UdaetaState state)
    {
        switch (state)
        {
            case UdaetaState.Wow:
                _animator.SetTrigger("Wow");
                break;
            case UdaetaState.Idle:
                _animator.SetTrigger("Idle");
                break;
            case UdaetaState.Smoke:
                _animator.SetTrigger("Smoke");
                break;
            case UdaetaState.Clap:
                _animator.SetTrigger("Clap");
                break;
            case UdaetaState.No:
                _animator.SetTrigger("No");
                break;
        }
    }
}
