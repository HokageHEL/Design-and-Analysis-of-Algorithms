# Лабораторна робота 5: Планувальник маршрутів між містами

## Опис проекту

Консольний застосунок для пошуку найкоротших шляхів між містами з використанням **Алгоритму Дійкстри** (Dijkstra's Algorithm). Програма дозволяє знаходити оптимальні маршрути, відображати мережу міст та візуалізувати результати.

## Структура проекту

```
├── City.cs                        # Модель міста
├── Road.cs                        # Модель дороги між містами
├── Graph.cs                       # Граф міст (список суміжності)
├── DijkstraAlgorithm.cs          # Алгоритм Дійкстри
├── GraphVisualizer.cs            # Візуалізація графа
├── Program.cs                     # Головне меню та логіка
```

## Функціональність

- Перегляд всіх міст та з'єднань
- Пошук найкоротшого шляху між двома містами
- Покрокове відображення роботи алгоритму Дійкстри
- Візуалізація графа міст
- Візуалізація знайденого маршруту

## Завдання для реалізації

### 1. `Graph.cs:23`
**Метод:** `AddRoad(City from, City to, int distance)`

**Мета:** Додати двонаправлену дорогу між містами.

**Вимоги:**
- Використати клас `Road` для створення з'єднань

**Приклад:**
```csharp
// Після виклику AddRoad(kyiv, lviv, 540):
// adjacencyList[kyiv] містить Road(kyiv → lviv, 540)
// adjacencyList[lviv] містить Road(lviv → kyiv, 540)
```

---

### 2. `Graph.cs:35`
**Метод:** `GetRoadsFrom(City city)`

**Мета:** Отримати список доріг, що виходять з вказаного міста.

**Вимоги:**
- Якщо місто не існує, повернути порожній список `List<Road>`

**Використання:** Цей метод використовується алгоритмом Дійкстри для обходу сусідніх міст.

---

### 3. `DijkstraAlgorithm.cs:14`
**Метод:** `FindShortestPaths(City start)`

**Мета:** Реалізувати алгоритм Дійкстри для пошуку найкоротших шляхів від стартового міста до всіх інших.

**Вимоги:**

- `Dictionary<City, int> distances` - відстані до кожного міста
- `Dictionary<City, City?> previous` - попереднє місто у найкоротшому шляху

**Повернути:** Кортеж `(distances, previous)` для подальшої реконструкції шляху

---

## Приклад виводу

```
=== Starting Dijkstra's Algorithm from Kyiv ===

Visiting: Kyiv (distance: 0 km)
  Checking neighbor: Chernihiv (current distance: ∞ km, via Kyiv: 145 km)
    ✓ Better path found to Chernihiv: 145 km
  Checking neighbor: Vinnytsia (current distance: ∞ km, via Kyiv: 270 km)
    ✓ Better path found to Vinnytsia: 270 km
  ...

=== Shortest Path from Kyiv to Zaporizhzhia ===
Total Distance: 565 km

Route:
  Kyiv -> Dnipro -> Zaporizhzhia
```

## Візуалізація

Програма генерує HTML-файли з візуалізацією:
- `city_network.html` - повна мережа міст
- `path_[StartCity]_to_[EndCity].html` - знайдений маршрут

