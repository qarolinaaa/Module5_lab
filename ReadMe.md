# StoreProductManager

## Разработка веб-сервисов — Module 05

Проект демонстрирует взаимодействие приложения **ASP.NET Core MVC** с **ASP.NET Core Web API** для управления товарами.

Проект состоит из двух приложений:

* **StoreApi** — Web API для работы с товарами.
* **StoreClient** — клиентское ASP.NET Core MVC-приложение, которое отправляет HTTP-запросы к Web API.

---

## Структура проекта

```text
StoreProductManager/
│
├── README.md
│
├── StoreApi/
│   ├── Controllers/
│   │   └── StoreProductsController.cs
│   ├── Models/
│   │   └── StoreProduct.cs
│   ├── Program.cs
│   └── StoreApi.csproj
│
└── StoreClient/
    └── StoreClient/
        ├── Controllers/
        │   └── StoreProductController.cs
        ├── Models/
        │   └── StoreProduct.cs
        ├── Services/
        │   ├── IStoreProductApiService.cs
        │   └── StoreProductApiService.cs
        ├── Views/
        │   ├── StoreProduct/
        │   │   ├── Index.cshtml
        │   │   ├── Details.cshtml
        │   │   ├── Create.cshtml
        │   │   └── Edit.cshtml
        │   └── _ViewImports.cshtml
        ├── Program.cs
        └── StoreClient.csproj
```

---

## Технологии

* C#
* .NET 9
* ASP.NET Core Web API
* ASP.NET Core MVC
* HTTP Client
* `IHttpClientFactory`
* Swagger / OpenAPI
* Razor Views
* Bootstrap

---

## Модель товара

Для товара используются следующие поля:

| Поле        | Тип     | Описание                 |
| ----------- | ------- | ------------------------ |
| Id          | int     | Уникальный идентификатор |
| Name        | string  | Название товара          |
| Description | string  | Описание товара          |
| Price       | decimal | Цена товара              |
| Quantity    | int     | Количество товара        |

---

## Web API

Web API предоставляет следующие HTTP-методы:

| Метод  | URL                  | Назначение                   |
| ------ | -------------------- | ---------------------------- |
| GET    | `/api/products`      | Получить список всех товаров |
| GET    | `/api/products/{id}` | Получить товар по ID         |
| POST   | `/api/products`      | Добавить новый товар         |
| PUT    | `/api/products/{id}` | Изменить товар               |
| DELETE | `/api/products/{id}` | Удалить товар                |

API также поддерживает проверку входных данных и возвращает соответствующие HTTP-коды:

* `200 OK` — успешный запрос;
* `201 Created` — товар успешно создан;
* `204 No Content` — товар успешно удалён;
* `400 Bad Request` — некорректные данные;
* `404 Not Found` — товар не найден;
* `500 Internal Server Error` — ошибка сервера.

---

## Начальные данные

При запуске Web API используются тестовые товары:

1. Notebook — учебная тетрадь;
2. Mouse — беспроводная мышь;
3. Keyboard — механическая клавиатура.

---

## Swagger

Для тестирования Web API используется Swagger.

После запуска API Swagger доступен по адресу:

```text
http://localhost:5036/swagger/index.html
```

Через Swagger можно проверить все основные операции:

* получение списка товаров;
* получение товара по ID;
* создание товара;
* изменение товара;
* удаление товара.

---

## StoreClient

`StoreClient` представляет собой ASP.NET Core MVC-приложение.

Клиент содержит:

* список товаров;
* страницу просмотра товара;
* форму добавления товара;
* форму изменения товара;
* удаление товара с подтверждением;
* обработку ошибок API.

Для HTTP-запросов используется `IHttpClientFactory`.

В приложении зарегистрирован именованный HTTP-клиент:

```csharp
builder.Services.AddHttpClient("StoreApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7168/");
});
```

---

## Сервис работы с API

В проекте используется отдельный сервис:

```text
IStoreProductApiService
StoreProductApiService
```

Сервис отвечает за взаимодействие с Web API и выполняет следующие операции:

* `GetAllAsync()` — получение списка товаров;
* `GetByIdAsync()` — получение товара по ID;
* `CreateAsync()` — создание товара;
* `UpdateAsync()` — изменение товара;
* `DeleteAsync()` — удаление товара.

---

## Запуск проекта

### 1. Запуск Web API

Открыть терминал и перейти в папку:

```bash
cd ~/StoreProductManager/StoreApi
```

Запустить:

```bash
dotnet run
```

После запуска открыть Swagger:

```text
http://localhost:5036/swagger/index.html
```

---

### 2. Запуск Client

Открыть второй терминал:

```bash
cd ~/StoreProductManager/StoreClient/StoreClient
```

Запустить:

```bash
dotnet run
```

После запуска приложение доступно по адресу:

```text
http://localhost:5233
```

---

## Проверка работы

Для проверки проекта необходимо:

1. Запустить Web API.
2. Открыть Swagger.
3. Проверить GET `/api/products`.
4. Запустить StoreClient.
5. Открыть клиентское приложение.
6. Проверить операции добавления, просмотра, изменения и удаления товара.

---

## Обработка ошибок

В клиентском приложении предусмотрена обработка основных HTTP-ответов API.

При возникновении ошибки пользователю выводится понятное сообщение вместо завершения работы приложения с необработанным исключением.

Примеры сообщений:

```text
Товар не найден.
Некорректные данные товара.
Ошибка сервера API.
Не удалось подключиться к Web API.
```

---

## Результат

В рамках работы реализовано клиент-серверное взаимодействие между ASP.NET Core MVC-приложением и ASP.NET Core Web API.

Web API предоставляет CRUD-операции для управления товарами, а клиентское приложение использует HTTP-запросы для работы с этими данными.
