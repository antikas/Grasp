﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class KeysEqual : Behavior
	{
		ConditionKey _key1;
		ConditionKey _key2;
		bool _equal;

		protected override void Given()
		{
			_key1 = new ConditionKey(typeof(int));
			_key2 = new ConditionKey(typeof(int));
		}

		protected override void When()
		{
			_equal = _key1.Equals(_key2);
		}

		[Then]
		public void KeysAreEqual()
		{
			Assert.That(_equal);
		}
	}
}