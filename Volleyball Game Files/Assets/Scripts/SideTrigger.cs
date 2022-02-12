using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrigger : MonoBehaviour
{
    public UiManager UI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            CharacterSwitcher.instance.SwitchToNextCharacter();
            UI.Score = UI.Score + 1;
        }
    }
}
