using UnityEngine;

namespace cakeslice
{
    public class Rotate : MonoBehaviour
    {
        private float timer;
        private const float time = 1;

        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 20);

            timer -= Time.deltaTime;
            if(timer < 0)
            {
                timer = time;
                //GetComponent<Outline>().enabled = !GetComponent<Outline>().enabled;
            }
        }
    }
}