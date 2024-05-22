using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private string poolTag;

    public void InitializeEffect(Vector2 position)
    {
        transform.position = position;
        transform.localScale = Vector2.one;
    }
    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;

        transform.localScale -= new Vector3(deltaTime, deltaTime, 0f);

        if (0f >= transform.localScale.x)
            gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        GameManager.Instance.GetComponent<ObjectPool>().RetrieveObject(poolTag, this.gameObject);
    }

    public void SetTag(string tag)
    {
        poolTag = tag;
    }
}
