using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // Awake is called when the script instance is loaded
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Multiple instances of GameManager detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

}
