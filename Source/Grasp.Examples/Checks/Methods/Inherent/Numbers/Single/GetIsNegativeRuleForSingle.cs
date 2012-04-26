﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Numbers.Single
{
	public class GetIsNegativeRuleForSingle : Behavior
	{
		IsNegativeMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsNegativeMethod();

			_expectedMethod = Reflect.Func<ICheckable<float>, Check<float>>(Checkable.IsNegative);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(float));
		}

		[Then]
		public void HasExpectedMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}
	}
}