﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Runtime.Compilation
{
	/// <summary>
	/// Indicates a calculation references a variable that is not defined in a schema's variables or as the output variable of any of a schema's calculations
	/// </summary>
	public class InvalidCalculationVariableException : CompilationException
	{
		/// <summary>
		/// Initializes an exception with the specified variable, calculation, and schema
		/// </summary>
		/// <param name="schema">The schema which defines the valid variables</param>
		/// <param name="variable">The invalid variable</param>
		/// <param name="calculation">The calculation which references the invalid variable</param>
		public InvalidCalculationVariableException(Schema schema, Variable variable, Calculation calculation) : base(schema)
		{
			Contract.Requires(variable != null);
			Contract.Requires(calculation != null);

			Variable = variable;
			Calculation = calculation;
		}

		/// <summary>
		/// Initializes an exception with the specified variable, calculation, schema, and message
		/// </summary>
		/// <param name="schema">The schema which defines the valid variables</param>
		/// <param name="variable">The invalid variable</param>
		/// <param name="calculation">The calculation which references the invalid variable</param>
		/// <param name="message">The message that describes the error</param>
		public InvalidCalculationVariableException(Schema schema, Variable variable, Calculation calculation, string message) : base(schema, message)
		{
			Contract.Requires(variable != null);
			Contract.Requires(calculation != null);

			Variable = variable;
			Calculation = calculation;
		}

		/// <summary>
		/// Initializes an exception with the specified variable, calculation, schema, message, and inner exception
		/// </summary>
		/// <param name="schema">The schema which defines the valid variables</param>
		/// <param name="variable">The invalid variable</param>
		/// <param name="calculation">The calculation which references the invalid variable</param>
		/// <param name="message">The message that describes the error</param>
		/// <param name="inner">The exception that is the cause of this exception</param>
		public InvalidCalculationVariableException(Schema schema, Variable variable, Calculation calculation, string message, Exception inner) : base(schema, message, inner)
		{
			Contract.Requires(variable != null);
			Contract.Requires(calculation != null);

			Variable = variable;
			Calculation = calculation;
		}

		/// <summary>
		/// Gets the invalid variable
		/// </summary>
		public Variable Variable { get; private set; }

		/// <summary>
		/// Gets the calculation which references the invalid variable
		/// </summary>
		public Calculation Calculation { get; private set; }
	}
}