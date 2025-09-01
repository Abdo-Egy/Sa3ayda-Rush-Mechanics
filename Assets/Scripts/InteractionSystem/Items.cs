using UnityEngine;

public class Items : MonoBehaviour, Iinteractble
{
    public void Interact()
    {
        Destroy(this.gameObject);
    }

}
