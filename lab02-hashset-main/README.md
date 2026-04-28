# Лабораторна робота 2: Хеш-таблиці

## Опис

Реалізація хеш-множини на основі офіційної імплементації Microsoft .NET Framework 4.0. Проект демонструє роботу з хеш-таблицями, розв'язанням колізій та практичним застосуванням множин.

## Структура

```
HashSetLab/
├── CustomHashSet.cs      - хеш-множина (ДЛЯ РЕАЛІЗАЦІЇ)
├── DuplicateDetector.cs  - пошук дублікатів (ДЛЯ РЕАЛІЗАЦІЇ)
├── HashHelpers.cs        - допоміжні методи
├── TestData.cs          - тестові дані
└── Program.cs           - демонстрація роботи
```

## Завдання для реалізації

### 1. CustomHashSet.cs

**Contains(string? item)** - перевірка наявності елемента
- Повернути `true`, якщо елемент знайдено

**Intersection(CustomHashSet other)** - перетин множин (A ∩ B)
- Повернути новий об'єкт CustomHashSet що містить перетин

### 2. DuplicateDetector.cs

**FindDuplicates(string[] words)** - знаходження дублікатів

**FindCommonWords(string text1, string text2)** - спільні слова

**CompareTexts(string text1, string text2)** - порівняння текстів
