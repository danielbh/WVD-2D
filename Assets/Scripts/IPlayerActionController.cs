using UnityEngine;

namespace UnityTest
{
	public interface IPlayerActionController 
	{
		void Fire();
		void Move ();
		float GetMoveH ();
		float GetMoveV ();
		float GetFireAimH ();
		float GetFireAimV ();
	}
}