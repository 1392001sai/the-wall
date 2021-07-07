using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTransition : MonoBehaviour
{
    public void CallTransition()
    {
        GameObject.FindWithTag("LevelPanel").GetComponent<Animator>().SetTrigger("ShiftRight");
    }
}
