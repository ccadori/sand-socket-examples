using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Main game;
    public SubmitInput input;

    private void Start()
    {
        input.onSubmit.AddListener(SetNickname);
    }

    public void SetNickname(string nickname)
    {
        PlayerPrefs.SetString("nickname", nickname);

        game.client.Write("nickname", nickname);
        game.ChangeScene(Scene.Game);
    }
}
