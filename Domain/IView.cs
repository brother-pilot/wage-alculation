﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wageсalculation.Domain
{
    public interface IView
    {

        //строка состояния
        string Status { get; set; }

        public void ShowDo(Dictionary<int, string> commandKey) { }
    }
}
