using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public Vector3 headTargetPosition = new Vector3(0, 0, 0);
	public Vector3 leftArmTargetPosition = new Vector3(0, 0, 0);
	public Vector3 rightArmTargetPosition = new Vector3(0, 0, 0);
	public Vector3 leftLegTargetPosition = new Vector3(0, 0, 0);
	public Vector3 rightLegTargetPosition = new Vector3(0, 0, 0);

	public void Reposition(BodyPart bodyPart) {
		switch (bodyPart) {
			case BodyPart.head:
				transform.localPosition = headTargetPosition;
				break;
			case BodyPart.leftArm:
				transform.localPosition = leftArmTargetPosition;
				break;
			case BodyPart.rightArm:
				transform.localPosition = rightArmTargetPosition;
				break;
			case BodyPart.leftLeg:
				transform.localPosition = leftLegTargetPosition;
				break;
			case BodyPart.rightLeg:
				transform.localPosition = rightLegTargetPosition;
				break;
		}
	}
}
