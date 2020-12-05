using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor;
using System.Collections;

public class DrawEditViewRange : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D boxCollider2D;
    public Color color;
    void Start()
    {
        
    }

    void OnDrawGizmos()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        Vector3 size = new Vector3 ( boxCollider2D.size.x, boxCollider2D.size.y, 1);
        Vector3 offset = new Vector3 ( boxCollider2D.offset.x, boxCollider2D.offset.y, 1);

        Gizmos.color = color;
        Gizmos.DrawWireCube(this.transform.position + offset, size);
 
        Gizmos.color = new Color (color.r, color.g, color.b, 0.4f);
        Gizmos.DrawCube(this.transform.position + offset, size);
    }
}
