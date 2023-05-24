using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Camera _camera;
    public Animator fadeAnimator;

    private void Awake()
    {
        // get player rigidbody and camera
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfInput();
    }

    private void SetPlayerVelocity()
    {
        _rigidbody.velocity = _movementInput * speed;

        // prevent player from going off screen (we dont want the user to loose the player)
        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < 0 && _rigidbody.velocity.x < 0) || (screenPosition.x > _camera.pixelWidth && _rigidbody.velocity.x > 0))
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }

        if ((screenPosition.y < 0 && _rigidbody.velocity.y < 0) || (screenPosition.y > _camera.pixelHeight && _rigidbody.velocity.y > 0))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        }
    }

    private void RotateInDirectionOfInput()
    {
        // if we are pressing any button
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _movementInput);

            _rigidbody.MoveRotation(targetRotation);
        }
    }

    // called when the player presses a button
    private void OnMove(InputValue inputValue)
    {
       _movementInput = inputValue.Get<Vector2>();
    }

    public void PlayerDeath()
    {
        fadeAnimator.Play("ANIM_FadeOut");
        Invoke("DelayedSceneSwitch", 3f);
    }

    private void DelayedSceneSwitch()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
