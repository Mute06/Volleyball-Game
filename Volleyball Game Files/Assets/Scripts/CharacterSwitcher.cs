using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterSwitcher : MonoBehaviour
{

    [AssetsOnly]
    public GameObject spiritPrefab;
    private int currentCharacterIndex = 0;
    public GameObject[] characters = new GameObject[2];
    public float unusedCharactersDrag;
    public float usedCharactersDrag;
    public Transform currentCharactersGroundCheck { get; private set; }

    private GameObject currentCharacter;
    public GameObject CurrentCharacter
    {
        get
        {
            return currentCharacter;
        }
    }

    private Rigidbody2D currentRb;
    public Rigidbody2D CurrentRb
    {
        get
        {
            return currentRb;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SwitchCharacter(characters[currentCharacterIndex]);
    }


    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.SwitchButton)
        {
            SwitchToNextCharacter();
        }
    }

    public void SwitchCharacter(GameObject newCharacter)
    {
        

        // increasing drag for unused character
        if (currentRb != null)
        {
            currentRb.drag = unusedCharactersDrag;

            GameObject spirit = Instantiate(spiritPrefab, currentCharacter.transform.position, Quaternion.identity);
            spirit.GetComponent<FlyTowards>().target = newCharacter.transform;
        }
      
        currentRb = newCharacter.GetComponent<Rigidbody2D>();
        currentRb.drag = usedCharactersDrag; // making the drag normal
        currentCharacter = newCharacter;
        currentCharactersGroundCheck = newCharacter.GetComponent<Character>().groundCheckPoint;

        Debug.Log("Switched to " + newCharacter.name);
    }

    public void SwitchToNextCharacter()
    {
        if (currentCharacterIndex + 1 >= characters.Length)
        {
            SwitchCharacter(characters[0]);
            currentCharacterIndex = 0;
        }
        else
        {
            SwitchCharacter(characters[currentCharacterIndex + 1]);
            currentCharacterIndex += 1;
        }
    }

    public void SwitchToInitialCharacter()
    {
        SwitchCharacter(characters[0]);
        currentCharacterIndex = 0;
    }
}
