using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A11___Convert_Application_to_use_Database.DataModels
{
    public class MovieGenre
    {
    public int Id {get;set;}
    public virtual Movie Movie { get; set; }
    public virtual Genre Genre { get; set; }
    }
}
