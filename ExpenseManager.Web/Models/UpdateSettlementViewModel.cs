using ExpenseManager.Settlement.Dto;

namespace ExpenseManager.Web.Models
{
    public class UpdateSettlementViewModel
    {
        public UpdateSettlementViewModel()
        {

            UpdateSettlementDto = new UpdateSettlementDto();
            ItemViewModel = new ItemViewModel();
            SettlementTypeModel = new ItemViewModel();
        }
        public UpdateSettlementDto UpdateSettlementDto { get; set; }
        public ItemViewModel ItemViewModel { get; set; }
        public ItemViewModel SettlementTypeModel { get; set; }
    }
}