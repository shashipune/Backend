using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssignment.Core
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            CreateDate= DateTime.Now;
            UpdateDate= DateTime.Now;
            CreateBy = "";
            UpdateBy = "";
        }
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }

        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
