using System.Collections.ObjectModel;

namespace Magic_Redone.Simple
{
    internal class ModCollections
    {
        public static ObservableCollection<string> FormOptions { get; } =
            [
                " ",
                "Вихрь",
                "Дождь",
                "Конус",
                "Круг",
                "Куб",
                "Овал",
                "Полусфера",
                "Сфера",
                "Треугольник"
            ];

        public static ObservableCollection<string> MethodOptions { get; } =
            [
                " ",
                "Воплощение",
                "Выпуск",
                "Дестабилизация",
                "Излучение",
                "Касание",
                "Луч",
                "Насыщение",
                "Самопоток",
                "Снаряд",
                "Точка",
                "Покров"
            ];

        public static ObservableCollection<string> TimeCastOptions { get; } =
            [
                "Защитное",
                "1 секунда",
                "2 секунды",
                "3 секунды",
                "5 секунд",
                "Минута",
                "15 минут",
                "30 минут",
                "Час",
                "2 часа",
                "12 часов",
                "24 часа",
                "3 суток"
            ];

        public static ObservableCollection<string> TimeExistOptions { get; } =
            [
                "1 секунда",
                "15 минут",
                "Час",
                "12 часов",
                "Сутки",
                "Неделя",
                "Месяц",
                "6 Месяцев",
                "Год"
            ];
    }
}
