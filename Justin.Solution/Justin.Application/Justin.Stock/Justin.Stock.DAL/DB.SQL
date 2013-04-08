CREATE TABLE [MyStocks] (
  [Code] NVARCHAR(15) NOT NULL UNIQUE, 
  [No] NVARCHAR(15) NOT NULL, 
  [Name] NVARCHAR(15) NOT NULL, 
  [SpellingInShort] NVARCHAR(15) NOT NULL, 
  [WarnPrice_Min] FLOAT, 
  [WarnPrice_Max] FLOAT, 
  [WarnPercent_Min] FLOAT, 
  [WarnPercent_Max] FLOAT, 
  [BuyPrice] FLOAT, 
  [BuyCount] INTEGER, 
  [ShowInFolatWindow] BOOLEAN, 
  [Order] INTEGER, 
  [HasProfitOrLoss] FLOAT);