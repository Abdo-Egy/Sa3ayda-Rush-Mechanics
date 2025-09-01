using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCsIinteractble : MonoBehaviour,Iinteractble
{
    public void Interact()
    {
        gameObject.layer = 0;
        GetComponent<Animator>().SetTrigger("Death");
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<NavMeshAgent>().enabled = false;
    }

}
