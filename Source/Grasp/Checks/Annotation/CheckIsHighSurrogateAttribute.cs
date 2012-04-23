﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is a high surrogate
	/// </summary>
	public sealed class CheckIsHighSurrogateAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsHighSurrogate"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsHighSurrogate"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsHighSurrogateMethod();
		}
	}
}