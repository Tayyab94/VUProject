using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Vu_Project.Models
{
    public class PostVisit
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public DateTime  VistTime { get; set; }

        [ForeignKey("PostId")]

        public virtual Post Post { get; set; }


    }
}