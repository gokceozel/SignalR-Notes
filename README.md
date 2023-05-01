# SignalR-Notlarım

Projeyi geliştirirken pivot table kullanmak zorundak kaldım.
https://learn.microsoft.com/en-us/sql/t-sql/queries/from-using-pivot-and-unpivot?view=sql-server-ver16


SELECT ImmigrationDate, [1], [2], [3],[4],[5] FROM
(select [City],[Count], Cast([ImmigrationDate] as date) as ImmigrationDate from Populations ) as PopulationT
PIVOT
(SUM(Count) FOR City IN ([1], [2], [3],[4],[5])) AS PTable
order by ImmigrationDate asc
