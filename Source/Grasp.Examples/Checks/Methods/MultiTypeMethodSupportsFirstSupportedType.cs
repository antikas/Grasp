﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods
{
	public class MultiTypeMethodSupportsFirstSupportedType : Behavior
	{
		TestMultiTypeCheckMethod _method;
		bool _isSupported;

		protected override void Given()
		{
			_method = new TestMultiTypeCheckMethod();
		}

		protected override void When()
		{
			_isSupported = _method.SupportsFirstType();
		}

		[Then]
		public void TypeIsSupported()
		{
			Assert.That(_isSupported);
		}

		private sealed class TestMultiTypeCheckMethod : MultiTypeCheckMethod
		{
			internal bool SupportsFirstType()
			{
				return SupportsTargetType(typeof(int));
			}

			protected override IEnumerable<Type> TargetTypes
			{
				get
				{
					yield return typeof(int);
					yield return typeof(string);
				}
			}

			protected override MethodInfo GetMethod(Type targetType, Type checkType)
			{
				throw new NotImplementedException();
			}
		}
	}
}