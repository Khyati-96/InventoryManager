using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryMasterNew.Models;

namespace InventoryMasterNew.Models.ViewModels
{
    public class UpdateItem
    {

        public ItemDto SelectedItem { get; set; }

        public IEnumerable<AisleDto> AisleOptions { get; set; }
    }
}