using UnityEngine;

namespace UnityTest
{
	public interface IPlayerActionController 
	{
		void Fire();
		void Move ();
		bool FireButtonBeingTouched ();
		bool DirectionButtonBeingTouched ();
	}
}