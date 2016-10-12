using UnityEngine;

public class PreviewManager : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0f, 1f, 0f), 5 * Time.deltaTime);
    }

}
