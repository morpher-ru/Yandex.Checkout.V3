namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Содержит фильтры по времени создания и свойство Limit.
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// Фильтр по времени создания 
        /// </summary>
        public DateFilter CreatedAt { get; set; }
        
        /// <summary>
        /// Размер выдачи результатов запроса — количество объектов, передаваемых в ответе. Возможные значения: от 1 до 100
        /// </summary>
        public int? Limit { get; set; }
    }
}
