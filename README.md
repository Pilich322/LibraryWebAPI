Для работы необходимо поменять имя сервера Бд в файле appsettings.json
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server="ВАШЕ ИМЯ СЕРВЕРА";Database=Library;Trusted_Connection=true;Encrypt=false"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

Выполнить миграцию базы данных для добовления её в MS SQL
```
add-migration
Имя вашей миграции
update-database
```
