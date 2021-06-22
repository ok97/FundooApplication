using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.DBModels
{
    public class UserLabel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserLabelId { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }

        public int LabelId { get; set; }
        public Label Label { get; set; }
    }
}
