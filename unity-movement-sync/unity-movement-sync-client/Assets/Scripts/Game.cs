using UnityEngine;
using System.Collections.Generic;

public struct NewClientMessage
{
    public string id;
    public string nickname;
    public float x;
    public float y;
}

public struct PositionMessage
{
    public string id;
    public float x;
    public float y;
}

public class Game : MonoBehaviour
{
    private Player currentPlayer;
    private Main main;
    private Dictionary<string, Character> characters;

    public Player playerTemplate;
    public Character characterTemplate;

    private void Start()
    {
        characters = new Dictionary<string, Character>();
        main = FindObjectOfType<Main>();
        currentPlayer = Instantiate(playerTemplate);
        
        main.client.Emitter.On("new-client", OnNewClient);
        main.client.Emitter.On("position", OnPosition);
        main.client.Emitter.On("disconnected", OnDisconnected);

        currentPlayer.character.Setup(
            PlayerPrefs.GetString("nickname"), 
            main.client.ID, 
            0, 
            0,
            false
        );

        Invoke("InGame", 0);
    }

    public void InGame()
    {
        main.client.Write("in-game", "");
    }

    public void OnNewClient(string data)
    {
        var newclientMessage = JsonUtility.FromJson<NewClientMessage>(data);
        var newClient = Instantiate(characterTemplate);
        
        newClient.Setup(
            newclientMessage.nickname, 
            newclientMessage.id, 
            newclientMessage.x, 
            newclientMessage.y,
            true
        );

        characters.Add(newclientMessage.id, newClient);
    }

    public void OnPosition(string data)
    {
        var positionMessage = JsonUtility.FromJson<PositionMessage>(data);
        
        if (characters.ContainsKey(positionMessage.id))
        {
            characters[positionMessage.id].targetPosition = new Vector2(
                positionMessage.x, 
                positionMessage.y
            );
        }
    }

    public void OnDisconnected(string data)
    {
        if (characters.ContainsKey(data))
        {
            Destroy(characters[data].gameObject);
            characters.Remove(data);
        }
    }
}
