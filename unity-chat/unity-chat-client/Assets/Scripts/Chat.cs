using UnityEngine;
using UnityEngine.UI;

public struct ChatMessage
{
    public string nickname;
    public string message;

    public override string ToString()
    {
        return string.Format("{0}: {1}\n", nickname, message);
    }
}

public class Chat : MonoBehaviour
{
    public SubmitInput submitInput;
    public Text textComponent;
    
    private Game game;

    private void Start()
    {
        game = FindObjectOfType<Game>();
        game.client.Emitter.On("message", OnReceiveMessage);
        submitInput.onSubmit.AddListener(SendChatMessage);
        textComponent.text = "";
    }

    private void SendChatMessage(string message)
    {
        game.client.Write("message", message);
    }

    private void OnReceiveMessage(string data)
    {
        var message = JsonUtility.FromJson<ChatMessage>(data);
        textComponent.text += message;
    }
}
