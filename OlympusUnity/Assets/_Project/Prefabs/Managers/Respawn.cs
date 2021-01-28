using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject ares;
    [SerializeField] Transform respawnPoint;

    public bool isDead;

    // Update is called once per frame
    void Update()
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



    void DisableGod()
    {
        ares.SetActive(false);
    }

    void EnableGod()
    {
        ares.SetActive(true);
    }

    public void RespawnProcedure()
    {
        DisableGod();
        ares.transform.position = respawnPoint.position;
        EnableGod();
    }
}
