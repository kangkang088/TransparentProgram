using UnityEngine;

public class Move : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 20);
    }
}