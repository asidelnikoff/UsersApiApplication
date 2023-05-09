# UsersApiApplication

## Техническое задание

Необходимо реализовать API приложение на ASP.NET Core (5 или более поздняя версия).
*Требования бизнес-логики и ограничения:*
 - Формат запроса/ответа должен быть **JSON**.
 - Методы API должныть асинхронными.
 - В качестве СУБД необходимо использовать PostgreSQL.
 - В качестве ORM необходимо использовать EntityFrameworkCore.
 - В качестве моделей данных должны использоваться следующие сущности:
      **user**(id, login, password, created_date, user_group_id, user_state_id)
      **user_group**(id, code, description)
      **user_state**(id, code, description)
 1) Приложение должно позволять добавлять/удалять/получать пользователей. Получить можно как одного, так и всех пользователей (добавление/удаление только по одному). При получениии пользователей должна возвращаться полная информация о них (с user_group и user_state).
 2) Система должна не позволять иметь более одного пользователя с user_group.code = "Admin".
 3) После успешной регистрации нового пользователя, ему должен быть выставлен статус "Active". Добавление нового пользователя должно занимать 5 сек. За это время при попытке добавления пользователя с таким же login должна возвращаться ошибка.
 4) Удаление пользователя должно осуществляться не путем физического удаления из таблицы, а путем выставления статуса "Blocked" у пользователя.
 5) Допускается добавлять вспомогательные данные в существующие таблицы.

## Разработчики

- [Alexander Sidelnikov](github.com/sidlenikoff)
