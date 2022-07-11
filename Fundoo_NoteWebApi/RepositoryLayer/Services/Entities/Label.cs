using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entities
{ 

    [Keyless]
    public class Label
    {
        
        public String LabelName { get; set; }
       

       
        [ForeignKey("User")]
        public int UserId { get; set; }
       


        [ForeignKey("Notes")]
        public int NoteId { get; set; }
        



    }
}
