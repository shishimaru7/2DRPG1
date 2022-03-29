using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneChange.instance.SceneChangeType(SCENE_TYPE.Town);
        }
    }
}