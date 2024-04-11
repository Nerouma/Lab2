using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movement;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI moveText;
    public GameObject loseText;
    private Vector3 prevPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        loseText.SetActive(false);
    }

    private void Update() 
    {
        float distance = Vector3.Distance(prevPosition, transform.position);
        movement += distance;
        prevPosition = transform.position;
        SetmoveText();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) 
    {
    
   if (other.gameObject.CompareTag("PickUp")) 
       {
           other.gameObject.SetActive(false);
           SetCountText();
       }
       count = count + 1;

        if(other.gameObject.CompareTag("Anti-Pickup"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            loseText.gameObject.SetActive(true);
        }
    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
       if (count >= 12)
       {
           winTextObject.SetActive(true);
       }
   }
    void SetmoveText() 
    {
        moveText.text = "Distance: " + movement.ToString();
    }

}


