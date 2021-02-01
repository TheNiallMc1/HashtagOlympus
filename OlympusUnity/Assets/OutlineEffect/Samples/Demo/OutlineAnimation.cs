using UnityEngine;

namespace cakeslice
{
    public class OutlineAnimation : MonoBehaviour
    {
        private bool pingPong = false;

        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            Color c = GetComponent<OutlineEffect>().lineColor0;

            if(pingPong)
            {
                c.a += Time.deltaTime;

                if(c.a >= 1)
                    pingPong = false;
            }
            else
            {
                c.a -= Time.deltaTime;

                if(c.a <= 0)
                    pingPong = true;
            }

            c.a = Mathf.Clamp01(c.a);
            GetComponent<OutlineEffect>().lineColor0 = c;
            GetComponent<OutlineEffect>().UpdateMaterialsPublicProperties();
        }
    }
}