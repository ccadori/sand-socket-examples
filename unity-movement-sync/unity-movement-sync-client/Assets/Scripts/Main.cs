using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum Scene
{
    Menu = 0,
    Game = 1,
}

public class Main : MonoBehaviour
{
    public Sand.Client client;
    public UnityEvent onConnected;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        client.Connect();
        client.Emitter.On("connected", OnClientConnected);
    }

    private void OnClientConnected(string data)
    {
        onConnected?.Invoke();
    }

    public void ChangeScene(Scene scene)
    {
        SceneManager.LoadScene((int)scene);
    }
}
