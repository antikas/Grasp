﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Variables.Namespaces.MultipleParts
{
	public class UnderscoreInMiddleIsNamespace : Behavior
	{
		bool _isNamespace;

		protected override void Given()
		{}

		protected override void When()
		{
			_isNamespace = Variable.IsNamespace("TestA.Te_stB");
		}

		[Then]
		public void IsTrue()
		{
			Assert.That(_isNamespace, Is.True);
		}
	}
}