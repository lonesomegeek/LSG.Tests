# Benchmarking Tests - SQL IN vs OR

The other day, I was having a chat with one of my collegue [yden063] and he was asking me: 
> Collegue: How can I do a dynamic comparaison (multiple ORs ie.: X = A OR X = B OR ...) with LINQ in C# (Dynamic Where)... 

> Me: Why do you want to do that, will it be easier to write down with an X IN (A, B, C) statement with LINQ (Contains)?

> Collegue: Is it the same performance?

> Me: Mmm... do not know, let's test both method and see!

And now, you need yo know that I love these kind of challenges that, from nothing, to build something to prove or refute our thinkings. By the way, at that time, I was personaly convinced that OR vs IN were giving the same performance. You will have to read 'til the end to know if I was right ! :)

# Way of comparing
To create a bench test that was "solid", I've created a console app that creates random values using [Bogus] library into a table named *AccountsWithoutIndex*. After this table is filled, I copy all the generated data to another table which has an index on the Country field *AccountsWithIndex*.

Note: Console app code is [here](./LSG.BenchTests.SqlInVersusOr/Program.cs)

I've created two runs to compare. The results were too sparse with 10k elements (less than 0.01EOC), this is why I've did a second run with 1M elements.:
- Run 1: 10k elements
- Run 2: 1M elements 

Machine specifications: Windows 10 2004, Dell XPS15 (9560), i7-7700HQ (2.8Ghz), 16GB Ram, using SQL Server 2019 Developer


Here are the commands executed in SQL Server:
```sql
SELECT * FROM dbo.AccountsWithoutIndex WHERE Country = 'Comoros' OR Country = 'Greece' OR Country = 'Micronesia'
SELECT * FROM dbo.AccountsWithoutIndex WHERE Country IN ('Comoros', 'Greece', 'Micronesia')

SELECT * FROM dbo.AccountsWithIndex WHERE Country = 'Comoros' OR Country = 'Greece' OR Country = 'Micronesia'
SELECT * FROM dbo.AccountsWithIndex WHERE Country IN ('Comoros', 'Greece', 'Micronesia')
```

I will not present the results of the 1st run (10k elements) because it was too fast to produce a real difference. But, for the second run, here are the results:

| Table | Using OR | Using IN |
|-------|----------|----------|
| AccountsWithoutIndex | 12EOC | 12EOC |
| AccountsWithIndex | 8.42EOC | 8.42EOC |

Result... it is exactly the same time. I do not know how it really works in the backend of SQL Server but it seems to be converting in the end OR statements in IN clause or vice-versa.

Thanks for reading. I've had a lot of fun doing that ;)

<!-- References -->
[Bogus]: https://github.com/bchavez/Bogus
[yden063]: https://www.github.com/yden063
