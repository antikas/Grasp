﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Grasp.Checks.Methods;
using NUnit.Framework;

namespace Grasp.Checks.Annotation.Inherent
{
	public class GetIsEqualToMethod : Behavior
	{
		CheckIsEqualToAttribute _attribute;
		ICheckMethod _method;

		protected override void Given()
		{
			_attribute = new CheckIsEqualToAttribute(1);
		}

		protected override void When()
		{
			_method = _attribute.GetCheckMethod();
		}

		[Then]
		public void IsIsEqualToMethod()
		{
			Assert.That(_method, Is.InstanceOf<IsEqualToMethod>());
		}
	}
}