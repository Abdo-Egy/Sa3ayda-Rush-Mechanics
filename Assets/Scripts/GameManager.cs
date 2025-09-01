using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;

    public static GameManager instance;
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
}
