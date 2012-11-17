﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public sealed class StartWorkHandler : Notion, IHandler<StartWorkCommand>
	{
		public static readonly Field<IRepository<WorkItem>> _workItemRepositoryField = Field.On<StartWorkHandler>.For(x => x._workItemRepository);

		private IRepository<WorkItem> _workItemRepository { get { return GetValue(_workItemRepositoryField); } set { SetValue(_workItemRepositoryField, value); } }

		public StartWorkHandler(IRepository<WorkItem> workItemRepository)
		{
			Contract.Requires(workItemRepository != null);

			_workItemRepository = workItemRepository;
		}

		public Task HandleAsync(StartWorkCommand c)
		{
			return _workItemRepository.SaveAsync(new WorkItem(c.WorkItemId, c.Description, c.RetryInterval));
		}
	}
}