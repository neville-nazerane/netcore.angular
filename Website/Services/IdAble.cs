using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Services
{
    public class IdAble
    {

        public string ID { get; set; }

        public string NewID()
        {
            ID = Guid.NewGuid().ToString();
            return ID;
        }

        public static implicit operator string(IdAble id) => id.ID;

    }
}
