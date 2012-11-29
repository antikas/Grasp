﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Work.Items;

namespace Grasp.Knowledge.Definition
{
	public class RangeQuestion : Question
	{
		public static readonly Field<ValueQuestion> MinimumField = Field.On<RangeQuestion>.For(x => x.Minimum);
		public static readonly Field<ValueQuestion> MaximumField = Field.On<RangeQuestion>.For(x => x.Maximum);
		public static readonly Field<Identifier> LowerLessThanUpperVariableNameField = Field.On<RangeQuestion>.For(x => x.LowerLessThanUpperVariableName);

		public RangeQuestion(FullName name, ValueQuestion minimum, ValueQuestion maximum, Identifier lowerLessThanUpperVariableName) : base(name)
		{
			Contract.Requires(minimum != null);
			Contract.Requires(maximum != null);
			Contract.Requires(lowerLessThanUpperVariableName != null);

			Minimum = minimum;
			Maximum = maximum;
			LowerLessThanUpperVariableName = lowerLessThanUpperVariableName;
		}

		public ValueQuestion Minimum { get { return GetValue(MinimumField); } private set { SetValue(MinimumField, value); } }
		public ValueQuestion Maximum { get { return GetValue(MaximumField); } private set { SetValue(MaximumField, value); } }
		public Identifier LowerLessThanUpperVariableName { get { return GetValue(LowerLessThanUpperVariableNameField); } private set { SetValue(LowerLessThanUpperVariableNameField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var minimum = GetMinimum(rootNamespace);
			var maximum = GetMaximum(rootNamespace);

			var lowerLessThanUpper = GetLowerLessThanUpper(rootNamespace, minimum, maximum);

			return new Schema(Params.Of(minimum, maximum), Params.Of(lowerLessThanUpper));
		}

		private Variable GetMinimum(Namespace rootNamespace)
		{
			return Minimum.GetSchema(rootNamespace).Variables.Single();
		}

		private Variable GetMaximum(Namespace rootNamespace)
		{
			return Maximum.GetSchema(rootNamespace).Variables.Single();
		}

		private Calculation GetLowerLessThanUpper(Namespace rootNamespace, Variable minimum, Variable maximum)
		{
			return new Calculation<bool>(
				rootNamespace + LowerLessThanUpperVariableName,
				Expression.LessThanOrEqual(minimum.ToExpression(), maximum.ToExpression()));
		}
	}
}