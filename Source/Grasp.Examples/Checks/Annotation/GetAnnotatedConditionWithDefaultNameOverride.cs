﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using FakeItEasy;
using Grasp.Checks.Conditions;
using Grasp.Checks.Methods;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Annotation
{
	public class GetAnnotatedConditionWithDefaultNameOverride : Behavior
	{
		Type _targetType;
		AnnotatedConditionSource _source;
		Condition _condition;

		protected override void Given()
		{
			_targetType = typeof(TestTarget);

			_source = new AnnotatedConditionSource(_targetType);
		}

		protected override void When()
		{
			_condition = _source.GetConditions().Single();
		}

		[Then]
		public void HasOriginalTargetType()
		{
			Assert.That(_condition.Key.TargetType, Is.EqualTo(_targetType));
		}

		[Then]
		public void HasOverriddenName()
		{
			Assert.That(_condition.Key.Name, Is.EqualTo(TestTarget.NameOverride));
		}

		[Then]
		public void HasMemberRule()
		{
			Assert.That(_condition.Rule.Type, Is.EqualTo(RuleType.Property));
		}

		[Then]
		public void MemberRuleAppliesToMember()
		{
			var memberRule = (MemberRule) _condition.Rule;

			Assert.That(memberRule.Member, Is.EqualTo(Reflect.Property<TestTarget>(x => x.Target)));
		}

		[Then]
		public void MemberRuleHasOriginalRule()
		{
			var memberRule = (MemberRule) _condition.Rule;

			Assert.That(memberRule.Rule, Is.EqualTo(TestCheckAttribute.Rule));
		}

		[DefaultConditionName(NameOverride)]
		private class TestTarget
		{
			internal const string NameOverride = "Test";

			[TestCheck]
			public int Target { get; set; }
		}

		private sealed class TestCheckAttribute : CheckAttribute
		{
			internal static readonly CheckRule Rule = Rules.Rule.Check(Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive));

			protected override ICheckMethod GetCheckMethod()
			{
				var method = A.Fake<ICheckMethod>();

				A.CallTo(() => method.GetRule(typeof(int))).Returns(Rule);

				return method;
			}
		}
	}
}