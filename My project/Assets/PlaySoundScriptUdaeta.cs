using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundScriptUdaeta : MonoBehaviour
{
    public void PlayClap()
    {
        FindFirstObjectByType<sfxManager>().Play("Aplauso");
    }
}
