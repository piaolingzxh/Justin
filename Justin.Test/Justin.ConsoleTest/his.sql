/// <summary>
/// 0:表名<para />
/// 1:抽取时间字段名<para />
/// 2:开始时间字段名<para />
/// 3:结束时间字段名<para />
/// 4:业务键字段名<para />
/// 5:业务键值<para />
/// 6:当前抽取时间<para />
/// 7:缓慢变化维字段名拼接， 若多个字段，使用分隔符进行拼接，如："ltrim({1}),'{0}',ltrim({1})" ,0:分隔符,1:字段1名字,2:字段2名字<para />
/// 8:缓慢变化维字段值拼接， 若多个字段，使用分隔符进行拼接  如："'{1}','{0}','{2}'" ,0:分隔符,1:字段1的值,2:字段2的值<para />
/// 9:需要时刻更新的字段名及值拼接（Update子句）  数据格式："" 或者 ",field='value'" 常用字段： Value、Display<para />
/// 10:insert语句字段名子句拼接 （常用字段：",ValueField,displayField,IdField,PIdField,sys_Pkey"）,备注：以逗号为分隔符 ,一定以逗号结尾<para />
/// 11:insert语句Values子句拼接（常用字段：",'value','displayValue','IdValue','PIdValue','sys_PkeyValue'"）,备注：以逗号为分隔符 ,一定以逗号结尾<para />
/// 12:缓慢变化维判断,如" and IdField='IdValue' and PIdField='PIdValue'",注：前空格和单引号<para />
/// 13:syskey字段名<para />
/// 14:syskey字段值<para />

/// </summary>
--说明：在DW维度表中，相同Biz（业务键）值的多条记录，有且仅有一条记录的endtimeF字段值是Null，以下所有，均依据此结论
--1、当要插入一条记录时，该记录的业务键若在维度表中不存在即@count<0，可以直接插入。若存在，则转下一步
--2、若维度表里已存在该记录，且该记录是最新的，本次数据没有发生变化（缓慢变化）。则只更新无关字段，比如display和LoadTimeF，否则转下一步
--3、若当前记录有变化，则更新原纪录endtimeF为now ，同时判断数据变化性质，若变回原来的数据，则跳到步骤4,否则跳到步骤5
--4、若为变化为原来信息，则重新启用原来的记录，即更新endtimeF=null,loadtimeF=now
--5、若变化为新的信息，则插入新的记录

declare @count number(10),@count1 number(10),@count2 number(10);
select @count=count(*) from {0} where {4}='{5}' and {3} is null;--@count必为0(未插入过该业务键数据)或者1(最后一条插入的数据)
if(@count>0)then--当该Biz记录被插入过时，判断该条记录当前有无发生变化
--[
	select @count1=count(*) from {0} where {4}='{5}' and {3} is null{12};
    if (@count1<0) then--当前记录发生变化，则关闭该业务键最后一条记录，然后判断该条记录变化性质（继续变化为新的，变回原来的：指endtimeF已经不为null的记录）
    --[step 3
        update {0} set {3}={6},{1}={6} where {4}='{5}' and {3} is null;-- 关闭该业务键最后一条记录，只会更新一条，将其由新状态，改为老状态
        --判断是否为变回原来信息,注：原来信息也只存在一条
        select @count2=count(*) from {0} where {4}='{5}'{12};
        if(@count2>0)--变回原先信息,则重新启用原来的记录
        --[step 4
        	update {0} set {3}=null,{1}={6} where {4}='{5}'{12};--只更新一条老记录
        --]
        else--没有变回原先信息
        --[step 5
            insert into {0}({4},{1},{2},{13},{10})
            values('{5}',{6},{6},'{14}',{11});
        --]
        end;
		--select '1';
    --]
    else--无发生变化只需要更新（缓慢变化）无关字段
    --[step 2 select '1';	 
        update {0} set {1}={6}{9} where  {4}='{5}' and {3} is null{12};--只会更新一条新记录
    --]
    end;
--]
else--当该Biz没有被插入过时，可以直接插入该条记录
--[step 1	select '2';    
	insert into {0}({4},{1},{2},{13},{10})
            values('{5}',{6},{6},'{14}',{11});
--]
end ;
