﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsNullOrWhiteSpace"/> method
	/// </summary>
	public sealed class IsNullOrWhiteSpaceMethod : SingleTypeCheckMethod
	{
		/// <summary>
		/// Gets <see cref="System.String"/>
		/// </summary>
		protected override Type TargetType
		{
			get { return typeof(string); }
		}

		/// <summary>
		/// Gets the <see cref="Checkable.IsNullOrWhiteSpace"/> method
		/// </summary>
		/// <param name="checkType">The type of check</param>
		/// <returns>The <see cref="Checkable.IsNullOrWhiteSpace"/> method</returns>
		protected override MethodInfo GetMethod(Type checkType)
		{
			return Reflect.Func<ICheckable<string>, Check<string>>(Checkable.IsNullOrWhiteSpace);
		}
	}
}