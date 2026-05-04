using UnityEngine;

public class MapMove : MonoBehaviour
{
    public float speed = 9f; 

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }


}
// ObstacleDestroy.cs

public class ObstacleDestroy : MonoBehaviour
{
    void Update()
    {
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}
