using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Core.Domain.Event
{
    public abstract class Event
    {
        public DateTime Timestamp { get; protected set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
