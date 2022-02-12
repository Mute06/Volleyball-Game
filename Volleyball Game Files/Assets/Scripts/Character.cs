using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform groundCheckPoint;
    public float hitForce = 50f;
    private bool isCurrentlyControlled
    {
        get
        {
            return CharacterSwitcher.instance.CurrentCharacter == gameObject;
        }
    }



    
}
