# Сервис строковых замен

Данный сервис позволяет упростить получение шаблона для создания похожих текстовых документов, а так же построение новых документов на основе шаблона.

Функциональность сервиса описывается интерфейсом _IReplaceHelper_:
- Получение заготовки для замен по двум экземплярам документов

  _ReplaceHarvestResponse GetReplaceHarvest(string input1, string input2)_;

- Получение шаблона по одному документу и параметрам

  _string GetReplaceTemplate(string input1, GetTemplateReplaceQuery query)_

- Получение документа по шаблону и параметрам

  _string GetStrByTemplate(string template, GetStrReplaceQuery replaceQuery)_

Примеры:

Пусть есть два документа

    а. Съешь ещё этих мягких французских булок, да выпей чаю
       Съешь ещё этих мягких французских булок, да выпей чаю
       Съешь ещё этих мягких французских булок, да выпей чаю
       Съешь ещё этих мягких французских булок, да выпей чаю
       Съешь ещё этих мягких французских булок, да выпей чаю   
       
    b. Съешь больше этих мягких французских булок, да выпей чаю
       Съешь ещё этих мягких китайских булок, да выпей чаю
       Съешь ещё этих мягких французских булок, да выпей чаю
       Съешь меньше этих мягких французских круассанов, да выпей кофе
       Съешь ещё этих мягких российских булок, да выпей кофе


1. Получение заготовки на основе двух документов.
   Результатом для докуметов _a_ и _b_ будет являться список из замен всех отличающихся строк и указанием позиций:
<details> 
  <summary>JSON-результат</summary>
	<pre><code class="json">
{
  "Id": "00000000-0000-0000-0000-000000000000",
  "Replacements": [
    {
      "Id": "00000000-0000-0000-0000-000000000000",
      "FirstStr": "ещё",
      "SecondStr": "больше",
      "Positions": [
        {
          "FirstIndex": 6,
          "SecondIndex": 6
        }
      ]
    },
    {
      "Id": "00000000-0000-0000-0000-000000000000",
      "FirstStr": "французских",
      "SecondStr": "китайских",
      "Positions": [
        {
          "FirstIndex": 77,
          "SecondIndex": 80
        }
      ]
    },
    {
      "Id": "00000000-0000-0000-0000-000000000000",
      "FirstStr": "ещё",
      "SecondStr": "меньше",
      "Positions": [
        {
          "FirstIndex": 171,
          "SecondIndex": 172
        }
      ]
    },
    {
      "Id": "00000000-0000-0000-0000-000000000000",
      "FirstStr": "булок",
      "SecondStr": "круассанов",
      "Positions": [
        {
          "FirstIndex": 199,
          "SecondIndex": 203
        }
      ]
    },
    {
      "Id": "00000000-0000-0000-0000-000000000000",
      "FirstStr": "чаю",
      "SecondStr": "кофе",
      "Positions": [
        {
          "FirstIndex": 215,
          "SecondIndex": 224
        },
        {
          "FirstIndex": 270,
          "SecondIndex": 279
        }
      ]
    },
    {
      "Id": "00000000-0000-0000-0000-000000000000",
      "FirstStr": "французских",
      "SecondStr": "российских",
      "Positions": [
        {
          "FirstIndex": 242,
          "SecondIndex": 252
        }
      ]
    }
  ]
}
</code></pre>
</details>

2. Получение шаблона по документу и параметрам.
   Мы можем настраивать какая подстрока будет на месте замены.
   Построим шаблон на основе заготовки из первого примера.
   Предположим, что мы хотим, чтобы замена оборачивалась фигурными скобками _{}_ а текст замены брался как текст из первого документа и позиции в первом документе.
   Тогда результатом будет следующий шаблон

```
Съешь {ещё6} этих мягких французских булок, да выпей чаю
Съешь ещё этих мягких {французских77} булок, да выпей чаю
Съешь ещё этих мягких французских булок, да выпей чаю
Съешь {ещё171} этих мягких французских {булок199}, да выпей {чаю215_270}
Съешь ещё этих мягких {французских242} булок, да выпей {чаю215_270}
```

3. Получение документа по шаблону.
   Если мы возьмём шаблон из второго примера, и сформируем запрос:
```c#
new GetStrReplaceQuery
        (
            [
                new GetStrReplacementQuery("ещё6", "больше"),
                new GetStrReplacementQuery("французских77", "китайских"),
                new GetStrReplacementQuery("ещё171", "меньше"),
                new GetStrReplacementQuery("булок199", "круассанов"),
                new GetStrReplacementQuery("чаю215_270", "кофе"),
                new GetStrReplacementQuery("французских242", "российских")
            ]
        )
```

то результатом применения такого запроса будет строка _b_.
