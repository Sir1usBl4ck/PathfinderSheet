using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PathfinderSheetModels;

namespace View.Services
{
    public class InventoryItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate GeneralDataTemplate { get; set; }
        public DataTemplate WearableDataTemplate { get; set; }
        public DataTemplate WandDataTemplate { get; set; }
        public DataTemplate ConsumableDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item,
            DependencyObject container)
        {
            if (item is WearableItem)
            {
                return WearableDataTemplate;
            }
            if (item is IConsumable )
            {
                return ConsumableDataTemplate;
            }

            return GeneralDataTemplate;


        }
    }
}
