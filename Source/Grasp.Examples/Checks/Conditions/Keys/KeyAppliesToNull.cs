﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class KeyAppliesToNull : Behavior
	{
		ConditionKey _key;
		bool _applies;

		protected override void Given()
		{
			_key = new ConditionKey(typeof(object));
		}

		protected override void When()
		{
			_applies = _key.AppliesTo(null as string);
		}

		[Then]
		public void Applies()
		{
			Assert.That(_applies);
		}
	}
}