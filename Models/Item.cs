using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventoryMasterNew.Models
{
    public class Item
    {
        [Key]
        // id can be the sku number
        public int Id { get; set; }

        public string ItemName { get; set; }

        public string ItemType { get; set; }

        public int ItemCount { get; set; } = 0;

        // for getting only date
        [Column(TypeName = "Date")]
        public DateTime BBD { get; set; }

        // aisle can have multiple items
        [ForeignKey("Aisle")]
        public int AisleId { get; set; }
        // 
        public virtual Aisle Aisle { get; set; }
    }

    public class ItemDto
    { 
    public int Id { get; set; }

    public string ItemName { get; set; }

    public string ItemType { get; set; }

    public int ItemCount { get; set; } = 0;

    // for getting only date
    [Column(TypeName = "Date")]
    public DateTime BBD { get; set; }

    [ForeignKey("Aisle")]
    public int AisleId { get; set; }
    // 
    public virtual Aisle Aisle { get; set; }
        public object AvailableAisle { get; internal set; }
        public IEnumerable<AisleDto> ResponsibleAisle { get; internal set; }
    }

    public class ItemViewModel
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public string ItemType { get; set; }

        public int ItemCount { get; set; } = 0;
       
        public DateTime BBD { get; set; }

        public IEnumerable<AisleDto> AisleList { get; set; }
        

    }
}