using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField][Range(1, 15)] private float speed = 1.0f;
    [SerializeField] private Vector3 direction = Vector3.back;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
