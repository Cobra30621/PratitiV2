using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraControl : MonoBehaviour
{
    [SerializeField]
    GameObject player_1;
    [SerializeField]
    GameObject player_2;
    [SerializeField]
    Camera this_camera;
    [SerializeField]
    float level;
    [SerializeField]
    float rotation_arg;
    [SerializeField]
    int times;
    [SerializeField]
    float duration;
    [SerializeField]
    bool startShake;
    float player_distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //player_distance = Mathf.Abs(player_1.transform.position.x - player_2.transform.position.x);
        player_distance = Vector3.Distance(player_1.transform.position, player_2.transform.position);
        
        //this.transform.position = new Vector3((player_1.transform.position.x + player_2.transform.position.x) / 2, -2/Mathf.Sqrt(player_distance)+1, -10);
    }
    void FixedUpdate()
    {
        //this.transform.localRotation = Quaternion.Euler(0, 0, level);
        //CameraScale();
        if(startShake)
            StartCoroutine(CameraShake(level, times, duration));
    }

    void CameraScale()
    {
        if (this_camera.orthographicSize < 1)
        {
            this_camera.orthographicSize = 1;
        }
        else if (this_camera.orthographicSize > 6)
        {
            this_camera.orthographicSize = 6;
        }
        else
        {
            this_camera.orthographicSize = 1.25f * Mathf.Sqrt(player_distance);
        }
    }
    public IEnumerator CameraShake(float level,int times, float duration)
    {
        startShake = false;
        float x, y, rotation;
        while (times > 0)
        {
            x = Random.Range(-level, level);
            y = Random.Range(-level, level);
            rotation = Random.Range(-level * rotation_arg, level * rotation_arg);
            this.transform.position = new Vector3(x, y,this_camera.transform.position.z);
            this.transform.localRotation = Quaternion.Euler(0, 0, rotation);
            yield return new WaitForSeconds(duration / times);
            times--;
        }
        this.transform.position = new Vector3(0, 0, this_camera.transform.position.z);
        this.transform.localRotation = Quaternion.Euler(0, 0, 0);

    }
}
