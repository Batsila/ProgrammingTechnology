using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Managers.Exceptions
{
    /// <summary>
    /// Custom incorrect data exception
    /// </summary>
    [Serializable]
    public class IncorrectDataException : Exception
    {
        public IncorrectDataException() { }
        public IncorrectDataException(string message) : base(message) { }
        public IncorrectDataException(string message, Exception inner) : base(message, inner) { }
        protected IncorrectDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
