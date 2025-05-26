using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float time;

    public void SelfDestructGameObject()
    {
        Destroy(gameObject, time);
    }
}
