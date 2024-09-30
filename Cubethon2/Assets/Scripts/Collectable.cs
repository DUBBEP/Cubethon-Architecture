using UnityEngine;

public class Collectable : MonoBehaviour
{
    public delegate void Pickup();
    public event Pickup OnPickup;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null )
        {
            OnPickup?.Invoke();
            gameObject.SetActive(false);
        }
    }
}

