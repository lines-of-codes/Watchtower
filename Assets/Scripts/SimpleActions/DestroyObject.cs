using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject target;

    public void DoDestroy()
    {
        Destroy(target);
    }
}
