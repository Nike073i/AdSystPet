namespace AdSyst.Common.BusinessLayer.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Проверка перечисления на отсутствие элементов 
        /// </summary>
        /// <value>
        /// Возвращает true, если коллекция пустая либо является null. Иначе false
        /// </value>
        /// <param name="enumerable">Проверяемое перечисление</param>
        /// <typeparam name="T">Тип элементов перечисления</typeparam>
        /// <returns>Результат проверки отсутствия</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable) =>
            enumerable?.Any() != true;
    }
}
