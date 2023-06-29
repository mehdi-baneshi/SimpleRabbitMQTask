using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Core.Domain.Bus
{
    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : Event.Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler
    {
    }
}
