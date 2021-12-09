using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public Transform cameraTransform;
    public Material material;

    public float speed = 0.1f;
    
    // Update is called once per frame
    void Update()
    {
        float offset = cameraTransform.position.x * speed;
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
