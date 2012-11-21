﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cloak;

namespace Grasp.Hypermedia.Linq
{
	public abstract class MContent : Notion
	{
		public static readonly Field<MClass> ClassField = Field.On<MContent>.For(x => x.Class);

		protected MContent(MClass @class = null)
		{
			Class = @class ?? MClass.Empty;
		}

		public MClass Class { get { return GetValue(ClassField); } private set { SetValue(ClassField, value); } }

		internal virtual object GetHtml()
		{
			return Class.ItemsFromHere().Any(item => item != MClass.Empty)
				? GetHtmlWithClass(Class.ToString())
				: GetHtmlWithoutClass();
		}

		protected abstract object GetHtmlWithoutClass();

		protected abstract object GetHtmlWithClass(string classStack);

		public override string ToString()
		{
			return GetHtml().ToString();
		}
	}
}