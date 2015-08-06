using UnityEngine;

namespace UnityTest
{
	public interface IPlayerActionController 
	{
		void StartFiring();
		void StopFiring();
		void Fire();
		void Move ();
		float GetMoveH ();
		float GetMoveV ();
		float GetFireAimH ();
		float GetFireAimV ();
	}
}