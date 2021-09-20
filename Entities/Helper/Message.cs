using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Helper
{
    [DataContract]
    public class Message<T>
    {
        [DataMember(Name = "IsSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "ReturnMessage")]
        public string ReturnMessage { get; set; }

        [DataMember(Name = "Data")]
        public T Data { get; set; }
    }
}
