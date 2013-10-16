declare @tempChangeValue varchar(255), @count number(10);
select @count=count(*)  from {0} where {4}='{5}' and {3} is null;
if(@count>0)then
    select  @tempChangeValue=CONCAT({7}) from {0} where {4}='{5}' and {3} is null;
    if (@tempChangeValue<>CONCAT({8})) then 
        	select '0';
            update {0} set {3}={6} where {4}='{5}' and {3} is null;
            insert into {0}({10} {1},{2},{3})values({11}{6},{6},null);
    else 
        	select '1';
            update {0} set {1}={6}{9} where {4}='{5}' and {3} is null;
    end;
else
		select '2';
	    insert into {0}({10}{1},{2},{3})values({11}{6},{6},null);  
end ;