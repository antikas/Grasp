﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Knowledge.Runtime.Compilation
{
	internal sealed class SchemaCompiler
	{
		private readonly Schema _schema;
		private readonly ISet<Variable> _variables;
		private readonly IList<CalculationSchema> _calculations;

		internal SchemaCompiler(Schema schema)
		{
			_schema = schema;

			_calculations = schema.Calculations.Select(calculation => new CalculationSchema(calculation)).ToList();

			_variables = new HashSet<Variable>(schema.Variables.Concat(_calculations.Select(calculation => calculation.OutputVariable)));
		}

		internal Executable Compile()
		{
			ValidateCalculations();

			return new Executable(_schema, GetCalculator());
		}

		private void ValidateCalculations()
		{
			foreach(var calculation in _calculations)
			{
				EnsureVariablesExistInSchema(calculation);

				EnsureAssignableToOutputVariable(calculation);
			}
		}

		private void EnsureVariablesExistInSchema(CalculationSchema calculation)
		{
			foreach(var variable in calculation.Variables)
			{
				if(!_variables.Contains(variable))
				{
					throw new InvalidCalculationVariableException(
						_schema,
						variable,
						calculation.Source,
						Resources.InvalidCalculationVariable.FormatInvariant(variable, calculation));
				}
			}
		}

		private void EnsureAssignableToOutputVariable(CalculationSchema calculation)
		{
			if(!calculation.OutputVariable.Type.IsAssignableFrom(calculation.Expression.Type))
			{
				throw new InvalidCalculationResultTypeException(
					_schema,
					calculation.Source,
					Resources.InvalidCalculationResultType.FormatInvariant(
						calculation.Expression.Type,
						calculation,
						calculation.OutputVariable.Type,
						calculation.OutputVariable));
			}
		}

		private ICalculator GetCalculator()
		{
			return _calculations.Count == 1 ? GetCalculator(_calculations.Single()) : GetCalculators();
		}

		private static ICalculator GetCalculator(CalculationSchema schema)
		{
			return new CalculationCompiler().CompileCalculation(schema);
		}

		private ICalculator GetCalculators()
		{
			return new CompositeCalculator(OrderCalculatorsByDependency());
		}

		private IEnumerable<ICalculator> OrderCalculatorsByDependency()
		{
			return _calculations.OrderByDependency(_schema).Select(GetCalculator);
		}
	}
}