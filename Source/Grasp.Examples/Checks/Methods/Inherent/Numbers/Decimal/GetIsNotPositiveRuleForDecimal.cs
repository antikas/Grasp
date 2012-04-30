﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Numbers.Decimal
{
	public class GetIsNotPositiveRuleForDecimal : Behavior
	{
		IsNotPositiveMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsNotPositiveMethod();

			_expectedMethod = Reflect.Func<ICheckable<decimal>, Check<decimal>>(Checkable.IsNotPositive);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(decimal));
		}

		[Then]
		public void HasExpectedMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}
	}
}