﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Variables.Namespaces.SinglePart
{
	public class DigitAtEndIsNamespace : Behavior
	{
		bool _isNamespace;

		protected override void Given()
		{}

		protected override void When()
		{
			_isNamespace = Variable.IsNamespace("Test0");
		}

		[Then]
		public void IsTrue()
		{
			Assert.That(_isNamespace, Is.True);
		}
	}
}