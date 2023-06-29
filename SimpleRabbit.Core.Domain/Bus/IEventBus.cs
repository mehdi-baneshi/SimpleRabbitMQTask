using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Core.Domain.Bus
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : Event.Event;

        void Subscribe<T, TH>()
            where T : Event.Event
            where TH : IEventHandler<T>;
    }
}
