# OrderFlowAPI

OrderFlowAPI — это система обработки заказов, демонстрирующая асинхронную архитектуру с использованием ASP.NET Core и RabbitMQ. Она состоит из двух основных сервисов: Order Service (клиентская часть) и Processing Service (фоновый воркер).

---

## 🚧 Архитектура приложения

### Сервисы:

1. **Order Service (Клиентская часть):**
   - REST API для создания заказов.
   - Публикует сообщения в очередь RabbitMQ.

2. **Processing Service (Worker):**
   - Фоновый сервис, подписанный на очередь RabbitMQ.
   - Обрабатывает заказы (меняет статус, пишет в базу, логирует).

---

## 🧱 Используемые технологии:

| Компонент            | Технология                            |
|----------------------|---------------------------------------|
| API                 | ASP.NET Core Web API                 |
| Фоновый процесс     | ASP.NET Core Worker Service          |
| Очередь сообщений   | RabbitMQ                              |
| Коммуникация        | MassTransit (или Raw RabbitMQ)       |
| База данных         | PostgreSQL / SQL Server / SQLite     |
| OR/M                | Entity Framework Core                |
| Docker              | Для RabbitMQ и всего проекта (опционально) |

---

## ⚙️ Возможности:

- Асинхронная обработка заказов.
- Использование брокера сообщений (RabbitMQ).
- Архитектура "публишер → очередь → воркер".
- Применение чистой архитектуры (DDD-lite, CQRS, MediatR).
- Логирование и обработка ошибок.
- Возможность запуска через Docker Compose.

---

<img src="https://github.com/user-attachments/assets/b23e77bb-0c03-4ec1-9780-37a8e75c6d60" width="300" alt="Architecture Diagram" />

<img src="https://github.com/user-attachments/assets/a558a954-ce18-4afe-840f-9112a05d2eff" width="300" alt="Architecture Diagram" />






