# SignalR-Notlarım

![image](https://github.com/gokceozel/SignalR-Notes/assets/11633936/3ab9faac-1174-436b-9797-ddf8130b45d5 | width=400)

Code First kullanarak oluşturduğum için <b>dotnet ef database update</b> komutunu unutmayın.


Projeyi kodlarken pivot table'ı tercih ettim. Pivot table tercih etme sebebim kullanacağım chart'ın anlaşılır olması için bana gelen verilerin aşağıdaki gibi gözükmesi gerekiyordu.

| Tarih      | İzmir | İstanbul | Ankara |
|------------|-------|----------|--------|
| 01.05.2023 | 50    | 150      | 30     |
| 02.05.2023 | 100   | 80       | 40     |


Pivot table ayrıntılı kullanımına bakabilirsiniz.
https://learn.microsoft.com/en-us/sql/t-sql/queries/from-using-pivot-and-unpivot?view=sql-server-ver16



```sql
SELECT ImmigrationDate, [1], [2], [3], [4], [5] 
    FROM 
        (SELECT [City], [Count], CAST([ImmigrationDate] AS date) AS ImmigrationDate FROM Populations) AS PopulationT
        PIVOT
        (
            SUM([Count]) FOR [City] IN ([1], [2], [3], [4], [5])
        ) AS PTable
    ORDER BY ImmigrationDate ASC



Population service içinde sql tarafında pivot table kullanarak aldığım dataları GetChartList metodu ile okudum. Dikkat edilmesi gereken noktalar şu şekilde
System.DBNull = Sql'den okuduğum veride null gelen alanı temsil eder.
reader.GetDateTime(0) veri tabanında okuduğum Pivot table ile gelen 0. stunu temsil eder.


