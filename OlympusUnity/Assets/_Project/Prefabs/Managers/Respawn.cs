using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject ares;

    public bool isDead;

    private void Update()
    {
        if (isDead)
        {
            DisableGod();
        }

        if (!isDead)
        {
            EnableGod();
        }
    }

    private void DisableGod()
    {
        ares.SetActive(false);
    }

    private void EnableGod()
    {
        ares.SetActive(true);
    }
}
