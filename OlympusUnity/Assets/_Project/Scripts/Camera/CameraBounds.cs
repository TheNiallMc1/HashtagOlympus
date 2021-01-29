using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Camera cam;
    public Bounds terrainBounds;
    
    
    // Start is called before the first frame update
    void Update()
    {
        //CalculateLimits(cam, terrainBounds);
    }

    public static void CalculateLimits(Camera aCam, Bounds aArea, out Rect aLimits, out float aMaxHeight)
    {
        var angle = aCam.fieldOfView * Mathf.Deg2Rad * 0.5f;
        Vector2 tan = Vector2.one * Mathf.Tan(angle);
        tan.x *= aCam.aspect;
        Vector3 dim = aArea.extents;
        Vector3 center = aArea.center - new Vector3(0, aArea.extents.y, 0);
        float maxDist = Mathf.Min(dim.x / tan.x, dim.z / tan.y);
        float dist = aCam.transform.position.y - center.y;
        float f = 1f - dist / maxDist;
        dim *= f;
        aMaxHeight = center.y + maxDist;
        aLimits = new Rect(center.x-dim.x, center.z - dim.z, dim.x*2, dim.z*2);
    }
}
