using UnityEngine;

public class Menu : MonoBehaviour
{
    public Game game;
    public SubmitInput submitInput;

    private void Start()
    {
        submitInput.onSubmit.AddListener(SetName);
    }

    public void SetName(string name)
    {
        game.client.Write("nickname", name);
        game.ChangeScene(Scene.Chat);
    }
}
