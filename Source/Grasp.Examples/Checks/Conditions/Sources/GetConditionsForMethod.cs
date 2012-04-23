﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Sources
{
	public class GetConditionsForMethod : Behavior
	{
		TestConditionSource _source;
		Condition _condition;

		protected override void Given()
		{
			_source = new TestConditionSource();
		}

		protected override void When()
		{
			_condition = _source.GetConditions().Single();
		}

		[Then]
		public void KeyHasTargetType()
		{
			Assert.That(_condition.Key.TargetType, Is.EqualTo(_source.TargetType));
		}

		[Then]
		public void KeyHasOriginalName()
		{
			Assert.That(_condition.Key.Name, Is.EqualTo(ConditionKey.DefaultName));
		}

		[Then]
		public void RuleHasMethodType()
		{
			Assert.That(_condition.Rule.Type, Is.EqualTo(RuleType.Method));
		}

		[Then]
		public void RuleAppliesToMethod()
		{
			var methodRule = (MemberRule) _condition.Rule;

			Assert.That(methodRule.Member, Is.EqualTo(typeof(TargetType).GetMethod("GetTarget")));
		}

		[Then]
		public void MethodRuleAppliesRule()
		{
			var methodRule = (MemberRule) _condition.Rule;
			var passesRule = (ConstantRule) methodRule.Rule;

			Assert.That(passesRule.Passes);
		}

		private sealed class TargetType
		{
			public int GetTarget()
			{
				return 0;
			}
		}

		private sealed class TestConditionSource : MemberConditionSource
		{
			public override Type TargetType
			{
				get { return typeof(TargetType); }
			}

			protected override IEnumerable<IConditionDeclaration> GetDeclarations(MemberInfo member)
			{
				if(member == typeof(TargetType).GetMethod("GetTarget"))
				{
					var declaration = A.Fake<IConditionDeclaration>();

					A.CallTo(() => declaration.GetConditions(typeof(int))).Returns(new[] { new Condition<int>(Rule.Constant(true)) });

					yield return declaration;
				}
			}
		}
	}
}