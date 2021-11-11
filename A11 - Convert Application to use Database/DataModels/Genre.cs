﻿using System;
using System.Collections.Generic;

namespace A11___Convert_Application_to_use_Database.DataModels
{
    public class Genre
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres {get;set;}
    }
}
