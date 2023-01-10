using UnityEngine;

namespace CodeBase
{
    public class PlayerController : MonoBehaviour
    {
        float Horizontal;
        float Vertical;
        float turnSmoothVelocity;
        [SerializeField] float smoothTurnTime = 0.1f;
        Vector3 direction;
        [SerializeField] float MovementSpeed = 10;
        Rigidbody rb;

       
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
           
        }

        // Update is called once per frame
        void Update()
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            direction = new Vector3(Horizontal, 0, Vertical);
            if (direction.magnitude > 0.01f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurnTime);
                transform.rotation = Quaternion.Euler(0, Angle, 0);

                rb.MovePosition(transform.position + (direction * MovementSpeed * Time.fixedDeltaTime));
            }


         
        }

      
    }
}