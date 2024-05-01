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
Для того, чтоб сделать ваш проект локальным для сети и вы могли ползоваться им с другого устройства или программы,
то вам необходимо зайти в launchSettings.json и поменять во всех строках значение localhost на свой ip сети.
Его вы можете узнать по cmd ipconfig  IPv4-адрес. Вставить его
