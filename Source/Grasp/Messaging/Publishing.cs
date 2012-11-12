﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// Sends event and command messages along the ambient message channel
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Publishing
	{
		public static Task AnnounceAsync<TPublisher>(this TPublisher publisher, Event @event) where TPublisher : Notion, IPublisher
		{
			Contract.Requires(publisher != null);
			Contract.Requires(@event != null);

			return Message.Channel(publisher).PublishAsync(@event);
		}

		public static Task IssueAsync<TPublisher>(this TPublisher publisher, Command command) where TPublisher : Notion, IPublisher
		{
			Contract.Requires(publisher != null);
			Contract.Requires(command != null);

			return Message.Channel(publisher).PublishAsync(command);
		}
	}
}