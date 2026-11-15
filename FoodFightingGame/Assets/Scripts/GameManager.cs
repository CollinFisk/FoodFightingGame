using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player1Controller player1;
    private Player2Controller player2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player1 = FindAnyObjectByType<Player1Controller>();
        player2 = FindAnyObjectByType<Player2Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
