﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FluentAssertions;
using Grasp.Knowledge;
using Grasp.Knowledge.Forms;
using Grasp.Knowledge.Runtime;
using Xunit;

namespace Grasp.Checks
{


	public class XXX
	{
		[Fact]
		public void YYY()
		{
			var text = new TextInput(required: true, minimumLength: new Count(5), pattern: new RegexPattern(".*"));

			var parsedInput = new ParsedInput(text, typeof(int));

			var schema = parsedInput.GetQuestion().GetSchema(new Namespace("SomeInt"));

			foreach(var variable in schema.Variables.OrderByDescending(variable => variable.Name.Count()).ThenBy(variable => variable.Name.Identifier))
			{
				Console.WriteLine(variable);
			}

			Console.WriteLine();

			foreach(var calculation in schema.Calculations.OrderByDescending(calculation => calculation.OutputVariable.Name.Count()).ThenBy(calculation => calculation.OutputVariable.Name.Identifier))
			{
				Console.WriteLine(calculation);
			}
		}






		[Fact]
		public void ZZZ()
		{
			var text = new TextInput(required: true, minimumLength: new Count(5), pattern: new RegexPattern(".*"));

			var parsedInput = new ParsedInput(text, typeof(int));

			var schema = parsedInput.GetQuestion().GetSchema(new Namespace("SomeInt"));

			var executable = schema.Compile();

			var binding = executable.Bind(new FakeSnapshot());

			binding.ApplyCalculations();

			Console.Write(binding);
		}


		private sealed class FakeSnapshot : ISnapshot
		{
			public object GetValue(FullName name)
			{
				return "1";
			}
		}



	}





	public class ThatChecks
	{
		[Fact] public void Create()
		{
			var target = 1;

			var thatCheck = Check.That(target);

			thatCheck.Should().NotBeNull();
			thatCheck.Target.Should().Be(target);
			thatCheck.TargetType.Should().Be(target.GetType());
		}

		[Fact] public void Apply()
		{
			var thatCheck = Check.That(1);

			var result = thatCheck.Apply();

			result.Should().BeTrue();
		}

		[Fact] public void ApplyWithDefault()
		{
			var defaultResult = false;
			var thatCheck = Check.That(1, defaultResult);

			var result = thatCheck.Apply();

			result.Should().Be(defaultResult);
		}

		[Fact] public void ImplicitlyConvertToBoolean()
		{
			var check = new TestCheck();

			bool result = check;

			result.Should().BeFalse();
		}

		private sealed class TestCheck : Check<int>
		{
			internal TestCheck() : base(0)
			{}

			public override bool Apply()
			{
				return false;
			}
		}
	}
}