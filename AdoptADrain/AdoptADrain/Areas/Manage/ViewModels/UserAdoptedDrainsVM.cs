﻿using AdoptADrain.Areas.Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptADrain.Areas.Manage.ViewModels
{
    public class UserAdoptedDrainsVM
    {
        public List<DrainDTO> AdoptedDrains { get; set; }
    }
}
