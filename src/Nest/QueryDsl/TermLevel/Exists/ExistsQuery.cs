﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<ExistsQuery>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IExistsQuery : IFieldNameQuery
	{
	}

	public class ExistsQuery : FieldNameQuery, IExistsQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);

		protected override void WrapInContainer(IQueryContainer c) => c.Exists = this;
		internal static bool IsConditionless(IExistsQuery q) => q.Field.IsConditionless();
	}

	public class ExistsQueryDescriptor<T> 
		: FieldNameQueryDescriptor<ExistsQueryDescriptor<T>, IExistsQuery, T>
		, IExistsQuery where T : class
	{
		private IExistsQuery Self => this;
		bool IQuery.Conditionless => ExistsQuery.IsConditionless(this);
	}
}