﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAMail.Infrastructure.Persistence
{
    public interface IDeleteMailingList
    {
        void Delete(string id);
    }
}