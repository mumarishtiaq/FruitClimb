using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    //Variables...
    float speed = 10f;   //speed of horizontal movement
    float HorizontalInput;
    float ForwardInput;
    public GameObject ground;
    float minX, maxX, minZ, maxZ;

    /*Rotation Smoothning*/
    float turnSmoothTime=0.2f, turnSmoothVelocity;

    #endregion


    #region UnityEvents
    private void Start()
    {
        Collider col = ground.GetComponent<Collider>();
        float stoppingOffset = 0.7f;

        minX = col.bounds.min.x + stoppingOffset;
        maxX = col.bounds.max.x - stoppingOffset;

        minZ = col.bounds.min.z + stoppingOffset;
        maxZ = col.bounds.max.z - stoppingOffset;

        /*Debug.Log("X : " + minX + "   " + maxX);
        Debug.Log("Z : " + minZ + "   " + maxZ);*/
        
    }

    // Update is called once per frame
    void Update()
    {

        #region keyboard Input on xAxis and zAxis and movement of Player
        ////keyboard Input on xAxis and zAxis and movement of Player...
        HorizontalInput = Input.GetAxis("Horizontal");
        ForwardInput = Input.GetAxis("Vertical");
        float horizonOffset = HorizontalInput * speed * Time.deltaTime;
        float forwardOffset = ForwardInput * speed * Time.deltaTime;

       
        #endregion

        #region Clamping Player Within bounds of Ground
        //-------------Clamping Player Within bounds of Ground-------------------
        float rawHorizonPos = transform.position.x + horizonOffset;
        float rawForwardPos = transform.position.z + forwardOffset;
        float ClampedHorizontal = Mathf.Clamp(rawHorizonPos, minX, maxX);
        float ClampedVertical = Mathf.Clamp(rawForwardPos, minZ, maxZ);
        transform.position = new Vector3(rawHorizonPos, transform.position.y, ClampedVertical);// setting position
                                                                                               //--------------Clamping Done--------------------------
        #endregion


        #region setting and smoothning rotation of player a/c to player moving in direction
        // --------setting rotation of player a/c to player moving in direction--------
        Vector3 direction = new Vector3(HorizontalInput, 0f, ForwardInput).normalized;

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = PlayerSmoothRotation(transform.eulerAngles.y, targetAngle); //rotation smoothning
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        //---------Setting Rotation Done-------------
        #endregion


        //Camera.main.transform.LookAt(transform.position);

    }
    #endregion


    #region CustomEvents
    /*smoothning player rotation according to movement direction  */
    float PlayerSmoothRotation(float rotY,float targetAngle)
    {
        float angle = Mathf.SmoothDampAngle(rotY, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        return angle;
    }
    #endregion

}
