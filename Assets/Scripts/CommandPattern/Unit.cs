using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool CanExecuteCommand { get; private set; }

    [SerializeField] private float movementLenght = 1, jumpMovementLenght = 2, jumpHeight = 1.5f;
    [SerializeField] private float movementTime = .5f;

    private void Start()
    {
        CanExecuteCommand = true;
    }

    public void StartMove(Vector3 movement)
    {
        StartCoroutine(Move(movement));
    }

    public void StartJump(Vector3 movement)
    {
        StartCoroutine(Jump(movement));
    }

    private IEnumerator Move(Vector3 movement)
    {
        CanExecuteCommand = false;
        var start = transform.position;
        var target = start + movement * movementLenght;
        var t = 0f;

        while (t <= movementTime)
        {
            transform.position = Vector3.Lerp(start, target, t / movementTime);
            t += Time.deltaTime;
            yield return null;
        }

        transform.position = Vector3.Lerp(start, target, 1);
        CanExecuteCommand = true;
    }

    private IEnumerator Jump(Vector3 movement)
    {
        CanExecuteCommand = false;
        var start = transform.position;
        var target = start + movement * jumpMovementLenght;
        var t = 0f;
        Vector3 pos;
        while (t <= .5f)
        {
            pos = Vector3.Lerp(start, target, t / movementTime);
            transform.position = pos + Vector3.up * jumpHeight *
                Mathf.Sin(Mathf.Lerp(0, 180, t / movementTime) * Mathf.Deg2Rad);
            t += Time.deltaTime;
            yield return null;
        }

        pos = Vector3.Lerp(start, target, 1);
        transform.position = pos;
        CanExecuteCommand = true;
    }
}