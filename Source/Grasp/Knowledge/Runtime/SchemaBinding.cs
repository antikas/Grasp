﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Knowledge.Runtime
{
	/// <summary>
	/// A context in which a schema is bound to a calculation runtime and a set of values
	/// </summary>
	public class SchemaBinding : Notion
	{
		public static readonly Field<Schema> SchemaField = Field.On<SchemaBinding>.For(x => x.Schema);
		public static readonly Field<ICalculator> _calculatorField = Field.On<SchemaBinding>.For(x => x._calculator);
		public static readonly Field<ManyKeyed<Variable, VariableBinding>> _bindingsByVariableField = Field.On<SchemaBinding>.For(x => x._bindingsByVariable);

		private ICalculator _calculator { get { return GetValue(_calculatorField); } set { SetValue(_calculatorField, value); } }
		private ManyKeyed<Variable, VariableBinding> _bindingsByVariable { get { return GetValue(_bindingsByVariableField); } set { SetValue(_bindingsByVariableField, value); } }

		/// <summary>
		/// Initializes a binding with the specified schema, calculator, and variable bindings
		/// </summary>
		/// <param name="schema">The schema which defines the effective variables and calculations</param>
		/// <param name="calculator">The calculator which applies the specified schema's calculations</param>
		/// <param name="bindings">The initial variable bindings</param>
		public SchemaBinding(Schema schema, ICalculator calculator, IEnumerable<VariableBinding> bindings)
		{
			Contract.Requires(schema != null);
			Contract.Requires(calculator != null);
			Contract.Requires(bindings != null);

			// TODO: Ensure that all bound variables exist in schema

			Schema = schema;
			_calculator = calculator;

			_bindingsByVariable = bindings.ToDictionary(binding => binding.Variable).ToManyKeyed();
		}

		/// <summary>
		/// Initializes a binding with the specified schema, calculator, and bindings
		/// </summary>
		/// <param name="schema">The schema which defines the effective variables and calculations</param>
		/// <param name="calculator">The calculator which applies the specified schema's calculations</param>
		/// <param name="bindings">The initial variable bindings</param>
		public SchemaBinding(Schema schema, ICalculator calculator, params VariableBinding[] bindings) : this(schema, calculator, bindings as IEnumerable<VariableBinding>)
		{}

		/// <summary>
		/// Gets the schema which defines the effective variables and calculations
		/// </summary>
		public Schema Schema { get { return GetValue(SchemaField); } private set { SetValue(SchemaField, value); } }

		/// <summary>
		/// Applies the calculations defined by <see cref="Schema"/>
		/// </summary>
		public void ApplyCalculations()
		{
			_calculator.ApplyCalculation(this);
		}

		/// <summary>
		/// Gets the value of the specified variable
		/// </summary>
		/// <param name="variable">The variable for which to get the value</param>
		/// <returns>The value of the specified variable</returns>
		/// <exception cref="UnboundVariableException">Thrown if the specified variable is not bound</exception>
		public object GetVariableValue(Variable variable)
		{
			Contract.Requires(variable != null);

			var binding = TryGetBinding(variable);

			if(binding == null)
			{
				throw new UnboundVariableException(variable, Resources.VariableNotBound.FormatInvariant(variable));
			}

			return binding.Value;
		}

		/// <summary>
		/// Sets the value of the specified variable
		/// </summary>
		/// <param name="variable">The variable for which to set the specified value</param>
		/// <param name="value">The new value of the specified variable</param>
		public void SetVariableValue(Variable variable, object value)
		{
			Contract.Requires(variable != null);

			var binding = TryGetBinding(variable);

			if(binding != null)
			{
				binding.Value = value;
			}
			else
			{
				binding = new VariableBinding(variable, value);

				_bindingsByVariable.AsWriteable()[variable] = binding;
			}
		}

		private VariableBinding TryGetBinding(Variable variable)
		{
			VariableBinding binding;

			_bindingsByVariable.TryGetValue(variable, out binding);

			return binding;
		}
	}
}