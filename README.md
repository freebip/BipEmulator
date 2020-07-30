# Bip Emulator

Эмулятор окружения BipOS для часов Amazfit Bip

## Назначение

Написание и отладка приложений для часов Amazfit Bip под управлением мода BipOS (0.5.X)
в среде Microsoft Visual Studio 2019

![alt-текст](https://github.com/freebip/BipEmulator/raw/master/images/main.png "Главное окно")

## Описание 

Эмулятор состоит из двух модулей: Host и Proxy.
Proxy обрабатывает вызовы функций библиотеки Libbip поступающие от программы, работа которой
происходит в эмуляторе. Часть этих вызовов модуль Proxy обрабатывает сам, часть перенаправляет
модулю Host. 
На модуль Host возложены более высокоуровневые задачи:
* Обработка вызовов графических функций и отображение результата их работы на пользовательском интерфейсе.
* Обработка ввода команд пользователя, специфичных для эмулируемого устройства.
* Эмуляция специфичных данных эмулируемого устройства (Давление, Геолокационные координаты и пр.)
* Отоброжение отладочных данных программы

## Изображения


![alt-текст](https://github.com/freebip/BipEmulator/raw/master/images/options.png "Настройки")

![alt-текст](https://github.com/freebip/BipEmulator/raw/master/images/options.png "Отладочный вывод")


## Использование

* Добавьте исходный код в Proxy модуль.
* Откомпилируйте Proxy модуль.
* Запустите Host модуль.
* Завершите исполнение эмулятора
* Исправьте исходный код
* Повторять со второго пункта бесконечное кол-во раз

## Особенности использования

В данном проекте используется связка C# <-> CLI/C++. 
Поэтому добавляемые исходные файлы должны иметь расширение .cpp (не относится к заголовочным файлам)
***Но*** код должен быть написан в соответствии со стандартами чистого Си т.к. в последствии его предстоит
собирать с помощью gnu toolchain под Си.

Так как при использовании расширения .cpp мы переводим исходные файлы в разряд C++, то на код накладываются
более суровые ограничения (например, в плане приведения типов по-умолчанию)

Было так:
```
void* get_ptr_temp_buf_2();
struct calend_**    calend_p = get_ptr_temp_buf_2();
```
Надо так:
```
void* get_ptr_temp_buf_2();
struct calend_**    calend_p = (calend_**) get_ptr_temp_buf_2();
```

Proxy модуль является динамически линкуемой библиотекой (.dll), от которой зависит компиляция Host модуля.
Поэтому после всех изменений принудительно откомпилируйте Proxy модуль, а после этого запускайте программу.

## Пример

Для демонстрации возможностей эмулятора в него добавлено приложение [Календарь](https://github.com/MNVolkov/Calend)

![alt-текст](https://github.com/freebip/BipEmulator/raw/master/images/explorer.png "Исходные фаайлы календаря")

## Примечание

У проекта WIP статус. Обработка части функций библиотеки Libbip не реализована.


