Use Archive;
DECLARE @sql AS NVARCHAR(MAX) =''
DECLARE @user_id AS NVARCHAR(MAX) ='5272'
DECLARE @role_id AS NVARCHAR(MAX) ='1'   --14 осн ред -- 8 супер 

SET @sql = 'DELETE FROM [dbo].[_user_role_base] WHERE id_user = ' + @user_id
EXEC sp_executesql 	@sql

SET @sql = 'INSERT INTO [dbo].[_user_role_base]([id_user],[id_role],[id_base],[del])VALUES'+
'('+@user_id+','+@role_id+',1,0),'+
'('+@user_id+','+@role_id+',2,0),'+
'('+@user_id+','+@role_id+',3,0),'+
'('+@user_id+','+@role_id+',5,0),'+
'('+@user_id+','+@role_id+',6,0),'+
'('+@user_id+','+@role_id+',7,0),'+
'('+@user_id+','+@role_id+',8,0),'+
'('+@user_id+','+@role_id+',9,0),'+
'('+@user_id+','+@role_id+',10,0),'+
'('+@user_id+','+@role_id+',11,0),'+
'('+@user_id+','+@role_id+',16,0),'+
'('+@user_id+','+@role_id+',17,0),'+
'('+@user_id+','+@role_id+',18,0),'+
'('+@user_id+','+@role_id+',22,0)'
EXEC sp_executesql 	@sql
