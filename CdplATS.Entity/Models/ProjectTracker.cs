using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeVrikATS.Entity.Models
{

    public class ProjectTrackerCellViewModel
    {
        public int ValueId { get; set; }
        public string Value { get; set; }
        public string? ValueColorCode { get; set; }

        public int RowId { get; set; }
        public string RowName { get; set; }
        public int RowSortOrder { get; set; }
        public string? RowColorCode { get; set; }


        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnSortOrder { get; set; }
        public int Status { get; set; }
    }

    public class ProjectTrackerCellAddEdit
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string? Name { get; set; }

        public int? RowId { get; set; }
        public int? ColumnId { get; set; }
        public string? Value { get; set; }

        public string? Direction { get; set; }
        public int TargetId { get; set; }
        public int? ColorCode { get; set; }
        
    }

}
