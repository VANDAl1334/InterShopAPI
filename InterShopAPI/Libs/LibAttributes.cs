using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Libs;

/// <summary>
/// Класс атрибута для сравнения даты с указаным диапазоном 
/// </summary>
public class DateCompareAttribute : ValidationAttribute
{
    private DateOnly minDate;
    private DateOnly maxDate;

    public DateCompareAttribute(string minDate, string maxDate)
    {
        this.minDate = DateOnly.Parse(minDate);
        this.maxDate = DateOnly.Parse(maxDate);
    }
    public override bool IsValid(object? value)
    {
        DateOnly? date = value as DateOnly?;
        return minDate < date && date >= maxDate;
    }
}