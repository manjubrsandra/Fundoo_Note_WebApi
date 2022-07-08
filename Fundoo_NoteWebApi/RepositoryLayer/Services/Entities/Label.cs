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
        [Required]
        public String LabelName { get; set; }
       

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
       


        [Required]
        [ForeignKey("Notes")]
        public int NoteId { get; set; }
        



    }
}
