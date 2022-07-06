using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer.Note
{
    public class NoteReminderModel
    {
        [Required]

        public DateTime Reminder { get; set; }
    }
}
