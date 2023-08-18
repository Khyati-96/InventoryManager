using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory_Manager.Models
{
    public class Aisle
    {

        [Key]

        public int AisleId { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public int AisleCap { get; set; }

        // list of items
        public ICollection<Item> Item { get; set; }
    }

    public class AisleDto
    {
        public int AisleId { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public int AisleCap { get; set; }

        // list of items
        public ICollection<Item> Item { get; set; }
    }

    public class AisleViewModel
    {
        public int AisleId { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public int AisleCap { get; set; } = 0;


        public IEnumerable<ItemDto> Item { get; set; }
        public IEnumerable<AisleDto> AisleList { get; internal set; }
    }
}