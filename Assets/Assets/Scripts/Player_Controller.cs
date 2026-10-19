using Photon.Pun;
using UnityEngine;

public class Player_Controller : MonoBehaviourPun
{
    //publib vers
    public float move_force = 95;
    public float sprint_force = 125;
    public float jump_force = 125;
    public float mouse_sen = 95;
    public Camera player_camera;
    public Transform body;
    public Rigidbody player_body;
    public float ground_dis;
    public LayerMask ground_layer;
    public bool on_ground;

    //main vers 
    private float force;
    private float cam_rotX;
    void Start()
    {
        if (!photonView.IsMine) player_camera.enabled = false;
    }

    //player main movements
    public void Movement()
    {
        if (Input.GetKey(KeyCode.W)){ player_body.AddForce(body.forward*force*Time.deltaTime); }
        if (Input.GetKey(KeyCode.S)){ player_body.AddForce(-body.forward*force*Time.deltaTime); }
        if (Input.GetKey(KeyCode.A)){ player_body.AddForce(body.right*force*Time.deltaTime); }
        if (Input.GetKey(KeyCode.D)){ player_body.AddForce(-body.right*force *Time.deltaTime); }
        if (Input.GetKey(KeyCode.Space)&&on_ground){ player_body.AddForce(transform.up*jump_force*Time.deltaTime); }
    }
    public void Camera_Look()
    {
        float cam_x = Input.GetAxis("Mouse X") * mouse_sen*Time.deltaTime;
        float cam_y = Input.GetAxis("Mouse Y") * mouse_sen*Time.deltaTime;

        cam_rotX -= cam_y;
        cam_rotX = Mathf.Clamp(cam_rotX,-90,90);

        player_camera.transform.localRotation = Quaternion.Euler(cam_rotX,0,0);
        body.Rotate(Vector3.up*cam_x);
    }

    public void Ground_Ditection()
    {
        on_ground = Physics.Raycast(body.position,Vector3.down,ground_dis,ground_layer);
    }
    void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        Movement();
    }
    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        Ground_Ditection();
        Camera_Look();

        if (Input.GetKey(KeyCode.LeftShift)){ force = sprint_force; }
        else { force = move_force; }
    }
}
