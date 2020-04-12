using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private bool sync;

    [HideInInspector]
    public string nickname;
    [HideInInspector]
    public string id;
    public Text nicknameComponent;
    
    public Vector2 SyncPosition { get; set; }

    public void Setup(string nickname, string id, float x, float y, bool sync)
    {
        this.id = id;
        this.sync = sync;
        this.nickname = nickname;
        transform.position = new Vector2(x, y);
        nicknameComponent.text = nickname;
    }

    private void Update()
    {
        if (sync)
            transform.position = Vector2.Lerp(transform.position, SyncPosition, 0.1f);
    }
}
