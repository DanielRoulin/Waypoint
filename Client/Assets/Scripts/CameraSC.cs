using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSC : MonoBehaviour
{
    [SerializeField] Transform mainPlayer;
    
    [SerializeField] Transform currentTarget;
    [SerializeField] Vector2 mapSize = new Vector2(16, 10);
    [SerializeField] float speed = 1;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector3 Spawnpos;
    Camera cam;
    public static float width;

    [SerializeField] Transform[] tests;

    void Start()
    {
        width = Camera.main.orthographicSize * Camera.main.aspect * 2;
        cam = this.GetComponent<Camera>();
    }

    public void SetMainPlayer(Transform t)
    {
        mainPlayer = t;
    }

    public void setTarget(Transform target)
    {
        currentTarget = target;
    }

    void Update()
    {
        if(mainPlayer != null && currentTarget != null)
        {
            float height_ = 2f * cam.orthographicSize;
            //float width_ = height_ * cam.aspect;

            Vector3 UpperLeft = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
            Vector3 UpperRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 LowerLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 LowerRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

            tests[0].position = UpperLeft;
            tests[1].position = UpperRight;
            tests[2].position = LowerLeft;
            tests[3].position = LowerRight;

            Vector3 targetPos = new Vector3(mainPlayer.position.x, mainPlayer.position.y, -10);

            float halfcamx = Mathf.Abs(UpperRight.x - transform.position.x);
            float halfcamy = Mathf.Abs(UpperRight.y - transform.position.y);

            if((mainPlayer.position.x + halfcamx) > (currentTarget.position.x + mapSize.x))
            {
                targetPos = new Vector3((currentTarget.position.x + mapSize.x) - halfcamx, targetPos.y, -10);
            }else if((mainPlayer.position.x - halfcamx) < (currentTarget.position.x - mapSize.x))
            {
                targetPos = new Vector3((currentTarget.position.x - mapSize.x) + halfcamx, targetPos.y, -10);
            }

            if((mainPlayer.position.y + halfcamy) > (currentTarget.position.y + mapSize.y))
            {  
                targetPos = new Vector3(targetPos.x, (currentTarget.position.y + mapSize.y) - halfcamy, -10);
            }else if((mainPlayer.position.y - halfcamy) < (currentTarget.position.y - mapSize.y))
            {
                targetPos = new Vector3(targetPos.x, (currentTarget.position.y - mapSize.y) + halfcamy, -10);
            }
            /*transform.position = new Vector3(mainPlayer.position.x, mainPlayer.position.y, -10);

            if((mainPlayer.position.x + Mathf.Abs(UpperRight.x - transform.position.x)) > (currentTarget.position.x + mapSize.x))
            {
                Debug.Log("out (0,1)");
                transform.position = new Vector3((currentTarget.position.x + mapSize.x) - Mathf.Abs(UpperRight.x - transform.position.x), mainPlayer.position.y, -10);
            }

            if((mainPlayer.position.x - Mathf.Abs(UpperRight.x - transform.position.x)) < (currentTarget.position.x - mapSize.x))
            {
                Debug.Log("out (0,-1)");
                transform.position = new Vector3((currentTarget.position.x - mapSize.x) + Mathf.Abs(UpperRight.x - transform.position.x), mainPlayer.position.y, -10);

            }

            if((mainPlayer.position.y + Mathf.Abs(UpperRight.y - transform.position.y)) > (currentTarget.position.y + mapSize.y))
            {
                Debug.Log("out (1,0)");
                transform.position = new Vector3(mainPlayer.position.x, (currentTarget.position.y + mapSize.y) - Mathf.Abs(UpperRight.y - transform.position.y), -10);
            }

            if((mainPlayer.position.y - Mathf.Abs(UpperRight.y - transform.position.y)) < (currentTarget.position.y - mapSize.y))
            {
                Debug.Log("out (-1,0)");
                transform.position = new Vector3(mainPlayer.position.x, (currentTarget.position.y - mapSize.y) + Mathf.Abs(UpperRight.y - transform.position.y), -10);
            }*/

            Vector2 dist = targetPos - transform.position;
            rb.AddForce(dist * speed);
            
        }else
        {
            transform.position = Spawnpos;
        }
    }
}
