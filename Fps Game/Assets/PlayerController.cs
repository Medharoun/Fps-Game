using UnityEngine;

[RequireComponent (typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

	[SerializeField]
	private float speed = 5f;

	[SerializeField]
	private float lookSensitivity = 3f;

	private PlayerMotor motor;

	void Start ()
	{
		motor = GetComponent<PlayerMotor> ();
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
	}
}
