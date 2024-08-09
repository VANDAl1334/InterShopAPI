using InterShopAPI.Models;

namespace InterShopAPI.DTO;

public class ProductVariantDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsMain { get; set; }

    public virtual ICollection<PriceHistoryDTO> PriceHistories { get; set; }
    public virtual ICollection<ProductVariantCharacteristicsDTO> ProductVariantCharacteristics { get; set; }
}