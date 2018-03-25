using UnityEngine;

[RequireComponent (typeof(ConfigurableJoint))]
[RequireComponent (typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

	[SerializeField]
	private float speed = 5f;

	[SerializeField]
	private float lookSensitivity = 3f;

	[SerializeField]
	private float thrusterForce = 1000f;

	[Header("Spring Settings")]
	[SerializeField]
	private JointDriveMode mode = JointDriveMode.Position;
	[SerializeField]
	private float jointSpring = 20f;
	[SerializeField]
	private float jointMaxForce = 40f;


	private PlayerMotor motor;
	private ConfigurableJoint joint;

	void Start ()
	{
		motor = GetComponent<PlayerMotor> ();
		joint = GetComponent<ConfigurableJoint> (); 
		SetJointSettings (jointSpring);
	}

	void Update ()
	{
		//calcul movemement velocity vector3
		float xMov = Input.GetAxisRaw ("Horizontal");
		float zMov = Input.GetAxisRaw ("Vertical");

		Vector3 movHorizontal = transform.right * xMov;
		Vector3 movVertical = transform.forward * zMov;

		Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

		motor.Move (velocity);

		//calcul rotation  vector3 (turning around)
		float yRot = Input.GetAxisRaw ("Mouse X");

		Vector3 rotation = new Vector3 (0, yRot, 0) * lookSensitivity;

		motor.Rotation (rotation);


		float xRot = Input.GetAxisRaw ("Mouse Y");

		Vector3 cameraRotation = new Vector3 (xRot, 0, 0) * lookSensitivity;

		motor.rotateCamera (cameraRotation);

		Vector3 _thrusterForce = Vector3.zero;

		if (Input.GetButton ("Jump")) {
			_thrusterForce = Vector3.up * thrusterForce;
			SetJointSettings (0f);	
		} else {
			SetJointSettings (jointSpring); 
		}

		//Apply the thruster force
		motor.ApplyThruster(_thrusterForce);
	}
	private void SetJointSettings(float _jointSpring)
	{
		joint.yDrive = new JointDrive{
			positionSpring=jointSpring , 
			maximumForce = jointMaxForce};
	}
}
