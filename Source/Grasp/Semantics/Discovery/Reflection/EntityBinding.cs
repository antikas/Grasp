﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class EntityBinding : TypeBinding
	{
		public static Field<Many<MemberBinding>> MemberBindingsField = Field.On<EntityBinding>.Backing(x => x.MemberBindings);

		public Many<MemberBinding> MemberBindings { get { return GetValue(MemberBindingsField); } }

		public override TypeModel GetTypeModel()
		{
			return Type == typeof(Field) || typeof(Notion).IsAssignableFrom(Type) ? GetObjectModel() : GetValueModel() as TypeModel;
		}

		private EntityModel GetObjectModel()
		{
			var x = new EntityModel();

			EntityModel.TypeField.Set(x, Type);
			EntityModel.FieldsField.Set(x, new Many<Field>(GetFields()));

			return x;
		}

		private ValueModel GetValueModel()
		{
			// TODO: Why don't structs add anything to the equation?

			var x = new ValueModel();

			TypeModel.TypeField.Set(x, Type);

			return x;
		}

		private IEnumerable<Field> GetFields()
		{
			return MemberBindings.Select(memberBinding => memberBinding.Field);
		}
	}
}