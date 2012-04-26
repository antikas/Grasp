﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Decimal"/>, <see cref="System.Double"/>, <see cref="System.Int32"/>, <see cref="System.Int64"/>, or <see cref="System.Single"/>
	/// is not negative (not less than 0)
	/// </summary>
	public sealed class CheckIsNotNegativeAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsNotNegativeMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsNotNegativeMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsNotNegativeMethod();
		}
	}
}