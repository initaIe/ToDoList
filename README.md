# ToDoList

`ToDoList ASP.NET backend` тестовое задание.

## Содержание

1. [Установка](#установка)
2. [Использование](#использование)
3. [Дополнительно](#дополнительно)
4. [Features](#features)

## Установка

### Шаги для установки

1. Клонируйте репозиторий:
    ```bash
    git clone https://github.com/initaIe/ToDoList.git
    ```
2. Перейдите в:
    ```bash
    ToDoList\ToDoList.Backend
    ```
3. Запустите powershell файл для создания мигарций:
    ```bash
    hard-migrations.ps1
    ```
Или запустите их в ручную в определенной последовательности:
1)
```bash
dotnet ef database drop -f -c ToDoListWriteDbContext -p .\src\ToDoList.Infrastructure\ -s .\src\ToDoList.Presentation\
```
2)
```bash
dotnet ef migrations add Init -c ToDoListWriteDbContext -p .\src\ToDoList.Infrastructure\ -s .\src\ToDoList.Presentation\
```
3)
```bash
dotnet ef database update -c ToDoListWriteDbContext -p .\src\ToDoList.Infrastructure\ -s .\src\ToDoList.Presentation\
```

## Использование

1)
> Перейдите в проект Presentation и запустите его или выполните выполните:
![Screenshot](./assets/StartStep1.png)
![Screenshot](./assets/StartStep2.png)
```bash
dotnet run
```
2)
> Перейдите на:
```bash
http://localhost:5231/swagger/index.html
```
3)
> Зарегистрируйтесь.
> Введите валидный email address. Пример: "zxc@zxc.zxc".
> Введите пароль. Минимальная длина 5, максимальная длина 64. Пример: "qwerty12345".
> Введите username. Минимальная длина 3, максимальная длина 32, можно использовать только буквы и цифры. Пример: "qwertyzxc123".
![Screenshot](./assets/Register.png)
![Screenshot](./assets/RegisterResult.png)

4)
> Авторизуйтесь используя данные введенные ранее.
![Screenshot](./assets/Login.png)
![Screenshot](./assets/LoginResult.png)

5)
> Используйте ранее полученный AccessToken.
![Screenshot](./assets/BearerStep1.png)
![Screenshot](./assets/BearerStep2.png)

6)
> Теперь вы можете использовать данный функционал:
![Screenshot](./assets/Features.png)

7)
> При необходимости воспользуйтесь refresh-tokens и получите новый AccessToken:
![Screenshot](./assets/RefreshTokens.png)
![Screenshot](./assets/RefreshTokensResult.png)

## Дополнительно

> При необходимости вы можете изменить: настройки JWTBearer, время жизни рефреш сессии и строку подключения к Sqlite в "appsettings.Development.json"

## Features

> POST /api/auth/sessions - Логин, возвращает string AccessToken и guid RefreshToken.
> POST /api/auth/tokens-renewal - Обновление токенов, возвращает новый string AccessToken и новый guid RefreshToken.
> POST /api/auth/users - Регистрация, возвращает Guid созданного юзера.

> POST /api/todoitems - Создание, возвращает Guid созданного ToDoItem_а.
> GET /api/todoitems - Возвращает ToDoItem по его Id.
> GET /api/todoitems/{id} - Возвращает все ToDoItems.
> PUT /api/todoitems/{id} - Изменение, возвращает Guid измененного ToDoItem_a.
> DELETE /api/todoitems/{id} - Регистрация, возвращает Guid удаленного ToDoItem_a.

При дублях, конфликтах и ошибках на стороне сервера возвращает соответствующие статус коды.
