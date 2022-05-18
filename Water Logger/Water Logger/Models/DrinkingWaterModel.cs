using System.ComponentModel.DataAnnotations;

namespace Water_Logger.Models
{
    public class DrinkingWaterModel
    {
        public int Id { get; set; }
        //[DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
    }
}
