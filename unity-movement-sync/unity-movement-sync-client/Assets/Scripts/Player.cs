using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Main game;

    public Character character;

    private void Start()
    {
        game = FindObjectOfType<Main>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            character.targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            game.client.Write("position", JsonUtility.ToJson((Vector2)character.targetPosition));
        }
    }
}
