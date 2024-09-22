using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float speed;
    public Rigidbody rb;

    public GameObject bulletPrefab;

    public float fireRate;

    public float newFireRate;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 0f;
        rb = GetComponent<Rigidbody>();
        speed = 10.0f;
        newFireRate = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        KeyBinds();
    }
    void KeyBinds()
    {
        fireRate -= Time.deltaTime;
        Debug.Log(fireRate);    
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        if(Input.GetKeyDown(KeyCode.Mouse0) && fireRate <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = transform.up * 10.0f;
            Destroy(bullet, 2.0f);
            fireRate = newFireRate;
        }
    }
}
