﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Reflection;
using Grasp.Messaging;
using Grasp.Work.Persistence;

namespace Grasp.Work
{
	/// <summary>
	/// A versioned consistency boundary in which all events occur on the same timeline
	/// </summary>
	public abstract class Aggregate : PersistentNotion<Guid>
	{
		public static readonly Field<Guid> RevisionIdField = Field.On<Aggregate>.For(x => x.RevisionId);
		public static readonly Field<ManyInOrder<Event>> _unobservedEventsField = Field.On<Aggregate>.For(x => x._unobservedEvents);

		private ManyInOrder<Event> _unobservedEvents { get { return GetValue(_unobservedEventsField); } set { SetValue(_unobservedEventsField, value); } }

		protected Aggregate() : base(Guid.NewGuid())
		{
			_unobservedEvents = new ManyInOrder<Event>();
		}

		public Guid RevisionId { get { return GetValue(RevisionIdField); } private set { SetValue(RevisionIdField, value); } }

		public IEnumerable<Event> ObserveEvents()
		{
			var events = _unobservedEvents.ToList();

			ClearUnobservedEvents();

			return events;
		}

		public void SetVersion(AggregateVersion version)
		{
			Contract.Requires(version != null);

			ClearUnobservedEvents();

			foreach(var @event in version)
			{
				ApplyEvent(@event, isNew: false);
			}

			RevisionId = version.RevisionId;
		}

		protected void SetId(Guid id)
		{
			SetValue(PersistentId.ValueField, id);
		}

		// TODO: Set WhenCreated and WhenModified automatically by detecting field changes?

		protected void SetWhenCreated(DateTime when)
		{
			SetValue(Lifetime.WhenCreatedField, when);
		}

		protected void SetWhenModified(DateTime when)
		{
			SetValue(Lifetime.WhenModifiedField, when);
		}

		protected void Announce(Event @event)
		{
			ApplyEvent(@event, isNew: true);
		}

		private void ApplyEvent(Event @event, bool isNew)
		{
			DispatchToImplicitObservers(@event);

			if(isNew)
			{
				RecordUnobservedEvent(@event);
			}
		}

		private void DispatchToImplicitObservers(Event @event)
		{
			// This is some neat trickery from a CQRS tutorial repository:
			//
			// https://github.com/gregoryyoung/m-r/blob/master/SimpleCQRS/Domain.cs
			//
			// It performs overload resolution, at runtime, on the methods of this instance. As this includes private methods,
			// we can define a simple convention that aggregates respond to specific event types via private methods named "Handle",
			// differentiated by event type.
			//
			// It may require some abstraction in the future, but works just fine for now.
			//
			// The original inspiration can be found at https://github.com/davidebbo/ReflectionMagic and is reflected (pun intended) in the DynamicReflector class.

			DynamicReflector.For(this).Observe(@event);
		}

		private void RecordUnobservedEvent(Event @event)
		{
			((IList<Event>) _unobservedEvents).Add(@event);
		}

		private void ClearUnobservedEvents()
		{
			((ICollection<Event>) _unobservedEvents).Clear();
		}
	}
}