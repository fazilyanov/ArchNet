/****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
SELECT 'COPY \\SKY-SP-SQL1.STG.LAN\ArchiveScanFiles$\complect\archive\'+a.[file]+' D:\14\'
  FROM [Archive].[dbo].[complect_archive] b 
  left join [Archive].[dbo].[complect_docversion] a on a.id_archive=b.id 
  where a.del=0 and b.del=0 and b.id_frm_contr=16140 and b.date_doc>= CONVERT(DATETIME,'01.07.2015 00:00',104) and b.[date_doc]<= CONVERT(DATETIME,'30.09.2015 00:00',104) and b.id_doctree=5023