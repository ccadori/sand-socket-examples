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
    public float velocity = 10f;
    [HideInInspector]
    public Vector2 targetPosition;

    public void Setup(string nickname, string id, float x, float y, bool sync)
    {
        this.id = id;
        this.sync = sync;
        this.nickname = nickname;
        transform.position = new Vector2(x, y);
        nicknameComponent.text = nickname;
        targetPosition = new Vector2(x, y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position, 
            targetPosition, 
            velocity * Time.deltaTime
        );
    }
}
