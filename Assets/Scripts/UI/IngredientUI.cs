using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientUI : MonoBehaviour
{
    public Transform follow;

    public Vector3 offset;

    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        var screenPos = mainCamera.WorldToScreenPoint(follow.position);
        transform.position = screenPos + offset;
    }

    void Update()
    {
        var screenPos = mainCamera.WorldToScreenPoint(follow.position);

        transform.position = screenPos + offset;
    }

}
