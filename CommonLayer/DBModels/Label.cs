using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace CommonLayer.DBModels
{
    public class Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }
        public string Name { get; set; }

        // Navigation Properties
        public List<NoteLabel> NoteLabels { get; set; }
        public List<UserLabel> UserLabels { get; set; }
    }
}
