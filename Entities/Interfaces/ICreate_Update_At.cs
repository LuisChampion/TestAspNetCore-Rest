﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface ICreate_Update_At: ICreate_At
    {        
        DateTime Update_At { get; set; }
    }
}
