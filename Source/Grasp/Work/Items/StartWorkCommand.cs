﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class StartWorkCommand : Command
	{
		public static readonly Field<Guid> WorkItemIdField = Field.On<StartWorkCommand>.For(x => x.WorkItemId);
		public static readonly Field<string> DescriptionField = Field.On<StartWorkCommand>.For(x => x.Description);
		public static readonly Field<TimeSpan> RetryIntervalField = Field.On<StartWorkCommand>.For(x => x.RetryInterval);

		public StartWorkCommand(Guid workItemId, string description, TimeSpan retryInterval)
		{
			Contract.Requires(workItemId != Guid.Empty);
			Contract.Requires(description != null);
			Contract.Requires(retryInterval >= TimeSpan.Zero);

			WorkItemId = workItemId;
			Description = description;
			RetryInterval = retryInterval;
		}

		public Guid WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
		public string Description { get { return GetValue(DescriptionField); } private set { SetValue(DescriptionField, value); } }
		public TimeSpan RetryInterval { get { return GetValue(RetryIntervalField); } private set { SetValue(RetryIntervalField, value); } }
	}
}