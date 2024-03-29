USE [A]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'main_docversion_archive'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'main_docversion_archive'

GO
/****** Object:  Trigger [UpdateDoctreePre]    Script Date: 27.04.2016 10:01:29 ******/
DROP TRIGGER [dbo].[UpdateDoctreePre]
GO
/****** Object:  FullTextIndex     Script Date: 27.04.2016 10:01:29 ******/
DROP FULLTEXT INDEX ON [dbo].[main_archive]

GO
/****** Object:  Index [IX_main_person_name]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_person_name] ON [dbo].[main_person]
GO
/****** Object:  Index [IX_main_person_id_depart]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_person_id_depart] ON [dbo].[main_person]
GO
/****** Object:  Index [IX_main_person_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_person_del] ON [dbo].[main_person]
GO
/****** Object:  Index [IX_main_docversion_nn]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_docversion_nn] ON [dbo].[main_docversion]
GO
/****** Object:  Index [IX_main_docversion_main]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_docversion_main] ON [dbo].[main_docversion]
GO
/****** Object:  Index [IX_main_docversion_id_quality]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_docversion_id_quality] ON [dbo].[main_docversion]
GO
/****** Object:  Index [IX_main_docversion_id_archive_id]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_docversion_id_archive_id] ON [dbo].[main_docversion]
GO
/****** Object:  Index [IX_main_docversion_id_archive]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_docversion_id_archive] ON [dbo].[main_docversion]
GO
/****** Object:  Index [IX_main_docversion_id]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_docversion_id] ON [dbo].[main_docversion]
GO
/****** Object:  Index [IX_main_docversion_del_id_archive]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_docversion_del_id_archive] ON [dbo].[main_docversion]
GO
/****** Object:  Index [IX_main_docversion_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_docversion_del] ON [dbo].[main_docversion]
GO
/****** Object:  Index [IX_main_docversion_date_upd]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_docversion_date_upd] ON [dbo].[main_docversion]
GO
/****** Object:  Index [IX_main_docversion_date_reg]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_docversion_date_reg] ON [dbo].[main_docversion]
GO
/****** Object:  Index [IX_main_docversion_barcode]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_docversion_barcode] ON [dbo].[main_docversion]
GO
/****** Object:  Index [UIX_main_archive_id]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [UIX_main_archive_id] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_supervisor_checked]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_supervisor_checked] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_summ]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_summ] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_id_user]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_id_user] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_id_state]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_id_state] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_id_prjcode]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_id_prjcode] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_id_perf]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_id_perf] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_id_parent]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_id_parent] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_id_frm_prod]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_id_frm_prod] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_id_frm_dev]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_id_frm_dev] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_id_frm_contr]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_id_frm_contr] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_id_doctype]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_id_doctype] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_id_doctree]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_id_doctree] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_id_depart]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_id_depart] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_hidden]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_hidden] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_docpack]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_docpack] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_date_upd]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_date_upd] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_date_doc]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_date_doc] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_main_archive_accept]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_main_archive_accept] ON [dbo].[main_archive]
GO
/****** Object:  Index [_dta_index_main_archive_27_411200565__K22]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [_dta_index_main_archive_27_411200565__K22] ON [dbo].[main_archive]
GO
/****** Object:  Index [_dta_index_main_archive_27_411200565__K20_K1_K10_K2]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [_dta_index_main_archive_27_411200565__K20_K1_K10_K2] ON [dbo].[main_archive]
GO
/****** Object:  Index [_dta_index_main_archive_27_411200565__K2_K1_K20]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [_dta_index_main_archive_27_411200565__K2_K1_K20] ON [dbo].[main_archive]
GO
/****** Object:  Index [_dta_index_main_archive_27_411200565__K11_K22_K1]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [_dta_index_main_archive_27_411200565__K11_K22_K1] ON [dbo].[main_archive]
GO
/****** Object:  Index [_dta_index_main_archive_27_411200565__K10_K22_K1]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [_dta_index_main_archive_27_411200565__K10_K22_K1] ON [dbo].[main_archive]
GO
/****** Object:  Index [IX_yesno]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_yesno] ON [dbo].[_yesno]
GO
/****** Object:  Index [IX_user_setting_last_upd]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_setting_last_upd] ON [dbo].[_user_setting]
GO
/****** Object:  Index [IX_user_setting_key]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_setting_key] ON [dbo].[_user_setting]
GO
/****** Object:  Index [IX_user_setting_id_user]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_setting_id_user] ON [dbo].[_user_setting]
GO
/****** Object:  Index [IX_user_role_base_id_user]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_role_base_id_user] ON [dbo].[_user_role_base]
GO
/****** Object:  Index [IX_user_role_base_id_role]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_role_base_id_role] ON [dbo].[_user_role_base]
GO
/****** Object:  Index [IX_user_role_base_id_base]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_role_base_id_base] ON [dbo].[_user_role_base]
GO
/****** Object:  Index [IX__user_role_base_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX__user_role_base_del] ON [dbo].[_user_role_base]
GO
/****** Object:  Index [IX__user_role_base]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX__user_role_base] ON [dbo].[_user_role_base]
GO
/****** Object:  Index [IX_user_access_id_user]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_access_id_user] ON [dbo].[_user_access]
GO
/****** Object:  Index [IX_user_access_id_access]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_access_id_access] ON [dbo].[_user_access]
GO
/****** Object:  Index [IX_user_watch]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_watch] ON [dbo].[_user]
GO
/****** Object:  Index [IX_user_sname]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_sname] ON [dbo].[_user]
GO
/****** Object:  Index [IX_user_name]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_name] ON [dbo].[_user]
GO
/****** Object:  Index [IX_user_mail]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_mail] ON [dbo].[_user]
GO
/****** Object:  Index [IX_user_login]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_login] ON [dbo].[_user]
GO
/****** Object:  Index [IX_user_id_department]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_id_department] ON [dbo].[_user]
GO
/****** Object:  Index [IX_user_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_user_del] ON [dbo].[_user]
GO
/****** Object:  Index [_dta_index__user_27_1761907702__K6_K1_3]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [_dta_index__user_27_1761907702__K6_K1_3] ON [dbo].[_user]
GO
/****** Object:  Index [IX_truefalse]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_truefalse] ON [dbo].[_truefalse]
GO
/****** Object:  Index [IX_table_name]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_table_name] ON [dbo].[_table]
GO
/****** Object:  Index [IX_table_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_table_del] ON [dbo].[_table]
GO
/****** Object:  Index [IX_role_where_id_table]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_role_where_id_table] ON [dbo].[_role_where]
GO
/****** Object:  Index [IX_role_where_id_role]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_role_where_id_role] ON [dbo].[_role_where]
GO
/****** Object:  Index [IX_role_where_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_role_where_del] ON [dbo].[_role_where]
GO
/****** Object:  Index [IX_role_access_id_role]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_role_access_id_role] ON [dbo].[_role_access]
GO
/****** Object:  Index [IX_role_access_id_access]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_role_access_id_access] ON [dbo].[_role_access]
GO
/****** Object:  Index [IX_role_access_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_role_access_del] ON [dbo].[_role_access]
GO
/****** Object:  Index [IX_logtype_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_logtype_del] ON [dbo].[_logtype]
GO
/****** Object:  Index [IX_log_when]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_log_when] ON [dbo].[_log]
GO
/****** Object:  Index [IX_log_id_user]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_log_id_user] ON [dbo].[_log]
GO
/****** Object:  Index [IX_log_id_type]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_log_id_type] ON [dbo].[_log]
GO
/****** Object:  Index [IX_journal_when]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_journal_when] ON [dbo].[_journal]
GO
/****** Object:  Index [IX_journal_id_user]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_journal_id_user] ON [dbo].[_journal]
GO
/****** Object:  Index [IX_journal_id_table]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_journal_id_table] ON [dbo].[_journal]
GO
/****** Object:  Index [IX_journal_id_edittype]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_journal_id_edittype] ON [dbo].[_journal]
GO
/****** Object:  Index [IX_journal_id_base]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_journal_id_base] ON [dbo].[_journal]
GO
/****** Object:  Index [IX_journal_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_journal_del] ON [dbo].[_journal]
GO
/****** Object:  Index [IX_journal_change_id]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_journal_change_id] ON [dbo].[_journal]
GO
/****** Object:  Index [_dta_index__journal_27_362846613__K6_K10_K2_K4_1]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [_dta_index__journal_27_362846613__K6_K10_K2_K4_1] ON [dbo].[_journal]
GO
/****** Object:  Index [_dta_index__journal_27_362846613__K3_K2_K5_K6_K1_9]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [_dta_index__journal_27_362846613__K3_K2_K5_K6_K1_9] ON [dbo].[_journal]
GO
/****** Object:  Index [IX_frm_name_full]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_frm_name_full] ON [dbo].[_frm]
GO
/****** Object:  Index [IX_frm_name]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_frm_name] ON [dbo].[_frm]
GO
/****** Object:  Index [IX_frm_inn]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_frm_inn] ON [dbo].[_frm]
GO
/****** Object:  Index [IX_frm_id_1c]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_frm_id_1c] ON [dbo].[_frm]
GO
/****** Object:  Index [IX_frm_del_id]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_frm_del_id] ON [dbo].[_frm]
GO
/****** Object:  Index [IX_frm_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_frm_del] ON [dbo].[_frm]
GO
/****** Object:  Index [IX_edittype_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_edittype_del] ON [dbo].[_edittype]
GO
/****** Object:  Index [IX_doctype_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_doctype_del] ON [dbo].[_doctype]
GO
/****** Object:  Index [IX_doctree_pre_tree_parent]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_doctree_pre_tree_parent] ON [dbo].[_doctree_pre]
GO
/****** Object:  Index [IX_doctree_pre_top_parent]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_doctree_pre_top_parent] ON [dbo].[_doctree_pre]
GO
/****** Object:  Index [IX_doctree_pre_pos]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_doctree_pre_pos] ON [dbo].[_doctree_pre]
GO
/****** Object:  Index [IX_doctree_name]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_doctree_name] ON [dbo].[_doctree]
GO
/****** Object:  Index [IX_doctree_id_parent]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_doctree_id_parent] ON [dbo].[_doctree]
GO
/****** Object:  Index [IX_doctree_form]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_doctree_form] ON [dbo].[_doctree]
GO
/****** Object:  Index [_dta_index__doctree_27_753202975__K5_K1_3]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [_dta_index__doctree_27_753202975__K5_K1_3] ON [dbo].[_doctree]
GO
/****** Object:  Index [IX_id_frm]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_id_frm] ON [dbo].[_base]
GO
/****** Object:  Index [IX_base_name]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_base_name] ON [dbo].[_base]
GO
/****** Object:  Index [IX_base_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_base_del] ON [dbo].[_base]
GO
/****** Object:  Index [IX_base_active]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_base_active] ON [dbo].[_base]
GO
/****** Object:  Index [IX_access_type]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_access_type] ON [dbo].[_access]
GO
/****** Object:  Index [IX_access_del]    Script Date: 27.04.2016 10:01:29 ******/
DROP INDEX [IX_access_del] ON [dbo].[_access]
GO
/****** Object:  View [dbo].[main_docversion_archive]    Script Date: 27.04.2016 10:01:29 ******/
DROP VIEW [dbo].[main_docversion_archive]
GO
/****** Object:  Table [dbo].[main_person]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[main_person]
GO
/****** Object:  Table [dbo].[main_docversion]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[main_docversion]
GO
/****** Object:  Table [dbo].[main_archive]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[main_archive]
GO
/****** Object:  Table [dbo].[_yesno]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_yesno]
GO
/****** Object:  Table [dbo].[_user_setting]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_user_setting]
GO
/****** Object:  Table [dbo].[_user_role_base]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_user_role_base]
GO
/****** Object:  Table [dbo].[_user_access]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_user_access]
GO
/****** Object:  Table [dbo].[_user]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_user]
GO
/****** Object:  Table [dbo].[_truefalse]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_truefalse]
GO
/****** Object:  Table [dbo].[_table]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_table]
GO
/****** Object:  Table [dbo].[_site_setting]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_site_setting]
GO
/****** Object:  Table [dbo].[_role_where]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_role_where]
GO
/****** Object:  Table [dbo].[_role_access]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_role_access]
GO
/****** Object:  Table [dbo].[_role]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_role]
GO
/****** Object:  Table [dbo].[_logtype]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_logtype]
GO
/****** Object:  Table [dbo].[_log]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_log]
GO
/****** Object:  Table [dbo].[_journal]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_journal]
GO
/****** Object:  Table [dbo].[_frm]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_frm]
GO
/****** Object:  Table [dbo].[_edittype]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_edittype]
GO
/****** Object:  Table [dbo].[_doctype]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_doctype]
GO
/****** Object:  Table [dbo].[_doctree_pre]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_doctree_pre]
GO
/****** Object:  Table [dbo].[_doctree]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_doctree]
GO
/****** Object:  Table [dbo].[_base]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_base]
GO
/****** Object:  Table [dbo].[_access]    Script Date: 27.04.2016 10:01:29 ******/
DROP TABLE [dbo].[_access]
GO
/****** Object:  StoredProcedure [dbo].[PreStructur]    Script Date: 27.04.2016 10:01:29 ******/
DROP PROCEDURE [dbo].[PreStructur]
GO
/****** Object:  StoredProcedure [dbo].[PreDocTree]    Script Date: 27.04.2016 10:01:29 ******/
DROP PROCEDURE [dbo].[PreDocTree]
GO
/****** Object:  StoredProcedure [dbo].[PreDepartmentTree1c]    Script Date: 27.04.2016 10:01:29 ******/
DROP PROCEDURE [dbo].[PreDepartmentTree1c]
GO
/****** Object:  StoredProcedure [dbo].[PreDepartmentTree]    Script Date: 27.04.2016 10:01:29 ******/
DROP PROCEDURE [dbo].[PreDepartmentTree]
GO
/****** Object:  StoredProcedure [dbo].[GetTreeStructur]    Script Date: 27.04.2016 10:01:29 ******/
DROP PROCEDURE [dbo].[GetTreeStructur]
GO
/****** Object:  StoredProcedure [dbo].[GetTreeDoctree]    Script Date: 27.04.2016 10:01:29 ******/
DROP PROCEDURE [dbo].[GetTreeDoctree]
GO
/****** Object:  StoredProcedure [dbo].[GetTreeArchive]    Script Date: 27.04.2016 10:01:29 ******/
DROP PROCEDURE [dbo].[GetTreeArchive]
GO
/****** Object:  StoredProcedure [dbo].[GetParentTreeShortDog]    Script Date: 27.04.2016 10:01:29 ******/
DROP PROCEDURE [dbo].[GetParentTreeShortDog]
GO
/****** Object:  StoredProcedure [dbo].[GetParentTreeShort]    Script Date: 27.04.2016 10:01:29 ******/
DROP PROCEDURE [dbo].[GetParentTreeShort]
GO
/****** Object:  StoredProcedure [dbo].[GetParentTreeDog]    Script Date: 27.04.2016 10:01:29 ******/
DROP PROCEDURE [dbo].[GetParentTreeDog]
GO
/****** Object:  StoredProcedure [dbo].[GetParentTree]    Script Date: 27.04.2016 10:01:29 ******/
DROP PROCEDURE [dbo].[GetParentTree]
GO
/****** Object:  FullTextCatalog [main_catalog_doctext]    Script Date: 27.04.2016 10:01:29 ******/
GO
DROP FULLTEXT CATALOG [main_catalog_doctext]
GO
/****** Object:  FullTextCatalog [main_catalog_doctext]    Script Date: 27.04.2016 10:01:29 ******/
CREATE FULLTEXT CATALOG [main_catalog_doctext]WITH ACCENT_SENSITIVITY = OFF

GO
/****** Object:  StoredProcedure [dbo].[GetParentTree]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetParentTree] 
(
	@p_base nvarchar(254), -- Имя текущей базы данных
	@p_id nvarchar(254) = '0' 
)
AS
BEGIN
	DECLARE @sql AS NVARCHAR(MAX) =''
	DECLARE @top_parent AS int =0;

	-- Находим верхнего родителя
SET @sql =
	'WITH cteup AS(
		SELECT
			T.[id],
			T.[id_parent],
			0 AS lvl
		FROM '+ @p_base+'_archive T
		WHERE T.[id] = ' + @p_id + '
		UNION ALL
		SELECT
			T.[id],
			T.[id_parent],
			[cteup].[lvl] + 1 AS lvl
		FROM '+ @p_base+'_archive T
			JOIN [cteup] ON T.[id] = [cteup].[id_parent]
	)
	SELECT top 1 @top_parent=[id]
	FROM cteup
	ORDER BY [cteup].[lvl] DESC;';

EXEC sp_executesql 	@sql, N'@top_parent nvarchar(MAX) OUTPUT', @top_parent= @top_parent OUTPUT

IF OBJECT_ID(N'tempdb..#result', N'U') IS NOT NULL drop table #result;
CREATE TABLE #result (rn int identity not null,ID int,id_doctree int, name NVARCHAR(max), docpack int, [content] NVARCHAR(max), id_parent int, lvl int,cp int);

IF OBJECT_ID(N'tempdb..#result2', N'U') IS NOT NULL drop table #result2;
CREATE TABLE #result2 (ID int, id_doctree int, treetext NVARCHAR(max), docpack int, [content] NVARCHAR(max), tree_parent int, tree_level int, tree_leaf bit, pos int, tree_loaded bit,tree_expanded bit,top_parent int);
	
IF OBJECT_ID(N'tempdb..#tosort', N'U') IS NOT NULL drop table #tosort;
CREATE TABLE #tosort (ID int, id_doctree int, name NVARCHAR(MAX), docpack int, [content] NVARCHAR(max), id_parent int);
	
SET @sql ='WITH ctedown AS(
	SELECT
		T.[id],
		T.[id_doctree],
		T.[num_doc],
		T.[docpack],
		 T.[content],
		T.[id_parent]
	FROM '+ @p_base+'_archive T
	WHERE T.id =' + CAST(@top_parent as nvarchar)+'
	UNION ALL
	SELECT
		T.[id],
		T.[id_doctree],
		T.[num_doc],
		T.[docpack],
		T.[content],
		T.[id_parent]
	FROM '+ @p_base+'_archive T
		INNER JOIN [ctedown] ON T.[id_parent] = [ctedown].[id]
)
insert into #tosort (ID, id_doctree, name, [docpack],[content], id_parent)
SELECT * FROM ctedown';
EXEC sp_executesql 	@sql;


	WITH
	NumberedRecords(ID, id_doctree, [Name], [docpack],[content], id_parent, N, L) AS
	(
		SELECT ID, id_doctree, name, [docpack],[content], id_parent, CAST(STR(ROW_NUMBER()OVER(ORDER BY name, ID),11) AS VARCHAR(MAX)), Cast(0 as int) FROM #tosort
	),
	Tree(ID, id_doctree, [Name], [docpack],[content], id_parent, NodePath, lvl, cp) AS
	(
		SELECT ID, id_doctree, [Name], [docpack],[content], id_parent, N, L, @top_parent as cp FROM NumberedRecords WHERE ID=@top_parent
		UNION ALL
		SELECT T.ID, T.id_doctree, T.[Name], T.[docpack],T.[content], T.id_parent, Tree.NodePath+T.N, Tree.lvl+1, @top_parent as cp
		FROM Tree JOIN NumberedRecords T ON Tree.ID=T.id_parent
	)	
	insert into #result (ID, id_doctree, name, [docpack],[content], id_parent,lvl,cp) select ID,id_doctree,[Name], [docpack],[content], id_parent,lvl,cp FROM Tree ORDER BY NodePath OPTION(MAXRECURSION 0);


	

--update
insert into #result2 select id, id_doctree, name, [docpack],[content], id_parent, lvl, 
CASE WHEN
    (SELECT COUNT(*) AS Expr1
        FROM #result WHERE        
            (id_parent = A.ID)
    ) = 0 THEN 1 ELSE 0 END AS tree_leaf, rn, 1, 1,cp  from #result a order by rn
	
--select a.*,b.name as doctree from #result2 a left join _doctree b on a.id_doctree=b.id order by a.pos

SET @sql ='
	select a.*, ar.date_doc,d.name as doctype, f.name as frm, ar.summ, ar.docpack, 
	pr.code_new as prjcode, p.name as perf, s.name as state, dp.name as depart,ar.prim,
	b.name as doctree, c.[file] 
	from #result2 a 
	left join ' + @p_base + '_archive ar ON a.id = ar.id 
	left join _doctree b on a.id_doctree=b.id 
	left join _doctype d on ar.id_doctype=d.id 
	left join _frm f on ar.id_frm_contr=f.id 
	left join _prjcode pr on ar.id_prjcode=pr.id 
	left join _state s on ar.id_state=s.id 
	left join ' + @p_base + '_department dp on ar.id_depart=dp.id 
	left join ' + @p_base + '_person p on ar.id_perf=p.id 
	left join ' + @p_base + '_docversion c on a.id=c.id_archive and c.main=1 and c.del=0 
	order by a.pos';

	EXEC sp_executesql 	@sql;



END




GO
/****** Object:  StoredProcedure [dbo].[GetParentTreeDog]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetParentTreeDog] 
(
	@p_base nvarchar(254), -- Имя текущей базы данных
	@p_id nvarchar(254) = '0' 
)
AS
BEGIN
	DECLARE @sql AS NVARCHAR(MAX) =''
	DECLARE @top_parent AS int =0;


	IF OBJECT_ID(N'tempdb..#children__doctree_id_doctree', N'U') IS NOT NULL drop table #children__doctree_id_doctree;
	CREATE TABLE #children__doctree_id_doctree (child int);
	INSERT #children__doctree_id_doctree VALUES (7);
	WHILE @@ROWCOUNT > 0 
	BEGIN 
		INSERT #children__doctree_id_doctree 
		SELECT a.id 
		FROM _doctree a 
			JOIN #children__doctree_id_doctree c ON a.id_parent = c.child 
		WHERE NOT EXISTS(SELECT 1 FROM #children__doctree_id_doctree WHERE child = a.id) 
	END;

	-- Находим верхнего родителя
SET @sql =
	'WITH cteup AS(
		SELECT
			T.[id],
			T.[id_parent],
			0 AS lvl
		FROM '+ @p_base+'_archive T
		WHERE T.[id] = ' + @p_id + '
		UNION ALL
		SELECT
			T.[id],
			T.[id_parent],
			[cteup].[lvl] + 1 AS lvl
		FROM '+ @p_base+'_archive T
			JOIN [cteup] ON T.[id] = [cteup].[id_parent]
	)
	SELECT top 1 @top_parent=[id]
	FROM cteup
	ORDER BY [cteup].[lvl] DESC;';

EXEC sp_executesql 	@sql, N'@top_parent nvarchar(MAX) OUTPUT', @top_parent= @top_parent OUTPUT

IF OBJECT_ID(N'tempdb..#result', N'U') IS NOT NULL drop table #result;
CREATE TABLE #result (rn int identity not null,ID int,id_doctree int, name NVARCHAR(max), docpack int, [content] NVARCHAR(max),id_parent int, lvl int,cp int);

IF OBJECT_ID(N'tempdb..#result2', N'U') IS NOT NULL drop table #result2;
CREATE TABLE #result2 (ID int, id_doctree int, treetext NVARCHAR(max), docpack int,[content] NVARCHAR(max), tree_parent int, tree_level int, tree_leaf bit, pos int, tree_loaded bit,tree_expanded bit,top_parent int);
	
IF OBJECT_ID(N'tempdb..#tosort', N'U') IS NOT NULL drop table #tosort;
CREATE TABLE #tosort (ID int, id_doctree int, name NVARCHAR(MAX), docpack int,[content] NVARCHAR(max), id_parent int);
	
SET @sql ='WITH ctedown AS(
	SELECT
		T.[id],
		T.[id_doctree],
		T.[num_doc],
		T.[docpack],
		T.[content],
		T.[id_parent]
	FROM '+ @p_base+'_archive T
	WHERE T.id =' + CAST(@top_parent as nvarchar)+'
	UNION ALL
	SELECT
		T.[id],
		T.[id_doctree],
		T.[num_doc],
		T.[docpack],
		T.[content],
		T.[id_parent]
	FROM '+ @p_base+'_archive T
		INNER JOIN [ctedown] ON T.[id_parent] = [ctedown].[id]
)
insert into #tosort (ID, id_doctree, name, [docpack],[content], id_parent)
SELECT * FROM ctedown where id_doctree NOT IN (select child from #children__doctree_id_doctree)';
EXEC sp_executesql 	@sql;


	WITH
	NumberedRecords(ID, id_doctree, [Name], [docpack],[content], id_parent, N, L) AS
	(
		SELECT ID, id_doctree, name, [docpack],[content], id_parent, CAST(STR(ROW_NUMBER()OVER(ORDER BY name, ID),11) AS VARCHAR(MAX)), Cast(0 as int) FROM #tosort
	),
	Tree(ID, id_doctree, [Name], [docpack],[content], id_parent, NodePath, lvl, cp) AS
	(
		SELECT ID, id_doctree, [Name], [docpack],[content], id_parent, N, L, @top_parent as cp FROM NumberedRecords WHERE ID=@top_parent
		UNION ALL
		SELECT T.ID, T.id_doctree, T.[Name], T.[docpack],T.[content], T.id_parent, Tree.NodePath+T.N, Tree.lvl+1, @top_parent as cp
		FROM Tree JOIN NumberedRecords T ON Tree.ID=T.id_parent
	)	
	insert into #result (ID, id_doctree, name, [docpack],[content], id_parent,lvl,cp) select ID,id_doctree,[Name], [docpack],[content], id_parent,lvl,cp FROM Tree ORDER BY NodePath OPTION(MAXRECURSION 0);


	

--update
insert into #result2 select id, id_doctree, name, [docpack],[content], id_parent, lvl, 
CASE WHEN
    (SELECT COUNT(*) AS Expr1
        FROM #result WHERE        
            (id_parent = A.ID)
    ) = 0 THEN 1 ELSE 0 END AS tree_leaf, rn, 1, 1,cp  from #result a order by rn
	
--select a.*,b.name as doctree from #result2 a left join _doctree b on a.id_doctree=b.id order by a.pos
SET @sql ='
	select a.*,b.name as doctree,c.[file] 
	from #result2 a 
	left join _doctree b on a.id_doctree=b.id 
	left join '+ @p_base+'_docversion c on a.id=c.id_archive and c.main=1 and c.del=0
	order by a.pos';

	EXEC sp_executesql 	@sql;



END





GO
/****** Object:  StoredProcedure [dbo].[GetParentTreeShort]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetParentTreeShort] 
(
	@p_base nvarchar(254), -- Имя текущей базы данных
	@p_id nvarchar(254) = '0' 
)
AS
BEGIN

	DECLARE @sql AS NVARCHAR(MAX) =''
	DECLARE @top_parent AS int =0;
	
	-- Находим верхнего родителя
	
	IF OBJECT_ID(N'tempdb..#r1', N'U') IS NOT NULL drop table #r1;
	CREATE TABLE #r1 (ID int, id_doctree int, treetext NVARCHAR(max), docpack int, [content] NVARCHAR(max), tree_parent int, tree_level int,  pos int, tree_loaded bit, tree_expanded bit, top_parent int);
	
	SET @sql =
		'WITH cteup AS(
			SELECT
				T.[id],
				T.[id_doctree],
				T.[num_doc],
				T.[docpack],
				T.[content],
				T.[id_parent],
				0 AS lvl
			FROM '+ @p_base+'_archive T
			WHERE T.[id] = ' + @p_id + '
			UNION ALL
			SELECT
				T.[id],
				T.[id_doctree],
				T.[num_doc],
				T.[docpack],
				T.[content],
				T.[id_parent],
				[cteup].[lvl] + 1 AS lvl
			FROM '+ @p_base+'_archive T
			JOIN [cteup] ON T.[id] = [cteup].[id_parent]
		)
		INSERT INTO #r1 (ID, id_doctree, treetext, docpack, [content], tree_parent, tree_level, tree_loaded, tree_expanded) 
		SELECT ID, id_doctree, num_doc, docpack, [content], id_parent, ROW_NUMBER()OVER(ORDER BY lvl desc)-1, 1, 1 FROM cteup
		;';
	
	EXEC sp_executesql @sql;
		SET @sql ='
	select a.*, CASE WHEN
		(SELECT COUNT(*) AS Expr1
			FROM #r1 WHERE        
				(tree_parent = A.ID)
		) = 0 THEN 1 ELSE 0 END AS tree_leaf,
	b.name as doctree,c.[file] 
	from #r1 a 
	left join _doctree b on a.id_doctree=b.id 
	left join '+ @p_base+'_docversion c on a.id=c.id_archive and c.main=1 and c.del=0
	';

	EXEC sp_executesql 	@sql;


--	SELECT * FROM #r1 order by tree_level desc;

	--SELECT top 1 @top_parent=[id]
	--FROM #r1 where is_medparent=1 order by lvl asc;

	----SELECT @top_parent AS tp;

	--IF @top_parent=0
	--BEGIN
	--	SELECT top 1 @top_parent=[id]
	--	FROM #r1 order by lvl desc;
	--	--SELECT @top_parent AS tp;
	--END

	--IF OBJECT_ID(N'tempdb..#result', N'U') IS NOT NULL drop table #result;
	--CREATE TABLE #result (rn int identity not null,ID int,id_doctree int, name NVARCHAR(max), docpack int,[content] NVARCHAR(max),id_parent int, lvl int,cp int);

	--IF OBJECT_ID(N'tempdb..#result2', N'U') IS NOT NULL drop table #result2;
	--CREATE TABLE #result2 (ID int, id_doctree int, treetext NVARCHAR(max), docpack int, [content] NVARCHAR(max), tree_parent int, tree_level int, tree_leaf bit, pos int, tree_loaded bit, tree_expanded bit, top_parent int);
	
	--IF OBJECT_ID(N'tempdb..#tosort', N'U') IS NOT NULL drop table #tosort;
	--CREATE TABLE #tosort (ID int, id_doctree int, name NVARCHAR(MAX), id_parent int, docpack int, [content] NVARCHAR(max));
	
	--SET @sql ='WITH ctedown AS(
	--	SELECT
	--		T.[id],
	--		T.[id_doctree],
	--		T.[num_doc],
	--		T.[docpack],
	--		T.[content],
	--		T.[id_parent]
	--	FROM '+ @p_base+'_archive T
	--	WHERE T.id =' + CAST(@top_parent as nvarchar)+'
	--	UNION ALL
	--	SELECT
	--		T.[id],
	--		T.[id_doctree],
	--		T.[num_doc],
	--		T.[docpack],
	--		T.[content],
	--		T.[id_parent]
	--	FROM '+ @p_base+'_archive T
	--		INNER JOIN [ctedown] ON T.[id_parent] = [ctedown].[id] AND T.[is_medparent] = 0
	--)
	--insert into #tosort (ID, id_doctree, name, docpack,[content], id_parent)
	--SELECT * FROM ctedown';
	--EXEC sp_executesql 	@sql;


	--	WITH
	--	NumberedRecords(ID, id_doctree, [Name], docpack,[content], id_parent, N, L) AS
	--	(
	--		SELECT ID, id_doctree, name, docpack,[content], id_parent, CAST(STR(ROW_NUMBER()OVER(ORDER BY name, ID),11) AS VARCHAR(MAX)), Cast(0 as int) FROM #tosort
	--	),
	--	Tree(ID, id_doctree, [Name], docpack,[content], id_parent, NodePath, lvl, cp) AS
	--	(
	--		SELECT ID, id_doctree, [Name], docpack,[content], id_parent, N, L, @top_parent as cp FROM NumberedRecords WHERE ID=@top_parent
	--		UNION ALL
	--		SELECT T.ID, T.id_doctree, T.[Name], T.docpack,T.[content], T.id_parent, Tree.NodePath+T.N, Tree.lvl+1, @top_parent as cp
	--		FROM Tree JOIN NumberedRecords T ON Tree.ID=T.id_parent
	--	)	
	--	insert into #result (ID, id_doctree, name, docpack,[content], id_parent,lvl,cp) select ID,id_doctree,[Name],docpack,[content], id_parent,lvl,cp FROM Tree ORDER BY NodePath OPTION(MAXRECURSION 0);


	

	----update
	--insert into #result2 select id, id_doctree, name, docpack,[content], id_parent, lvl, 
	--CASE WHEN
	--	(SELECT COUNT(*) AS Expr1
	--		FROM #result WHERE        
	--			(id_parent = A.ID)
	--	) = 0 THEN 1 ELSE 0 END AS tree_leaf, rn, 1, 1,cp  from #result a order by rn
	
	----select a.*,b.name as doctree from #result2 a left join _doctree b on a.id_doctree=b.id order by a.pos

	--SET @sql ='
	--select a.*,b.name as doctree,c.[file] 
	--from #result2 a 
	--left join _doctree b on a.id_doctree=b.id 
	--left join '+ @p_base+'_docversion c on a.id=c.id_archive and c.main=1 and c.del=0
	--order by a.pos';

	--EXEC sp_executesql 	@sql;

END





GO
/****** Object:  StoredProcedure [dbo].[GetParentTreeShortDog]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetParentTreeShortDog] 
(
	@p_base nvarchar(254), -- Имя текущей базы данных
	@p_id nvarchar(254) = '0' 
)
AS
BEGIN

	DECLARE @sql AS NVARCHAR(MAX) =''
	DECLARE @top_parent AS int =0;

	IF OBJECT_ID(N'tempdb..#children__doctree_id_doctree', N'U') IS NOT NULL drop table #children__doctree_id_doctree;
	CREATE TABLE #children__doctree_id_doctree (child int);
	INSERT #children__doctree_id_doctree VALUES (15);
	WHILE @@ROWCOUNT > 0 
	BEGIN 
		INSERT #children__doctree_id_doctree 
		SELECT a.id 
		FROM _doctree a 
			JOIN #children__doctree_id_doctree c ON a.id_parent = c.child 
		WHERE NOT EXISTS(SELECT 1 FROM #children__doctree_id_doctree WHERE child = a.id) 
	END;

	--select * from #children__doctree_id_doctree order by child;
	
	-- Находим верхнего родителя
	IF OBJECT_ID(N'tempdb..#r1', N'U') IS NOT NULL drop table #r1;
	CREATE TABLE #r1 (ID int, id_doctree int, treetext NVARCHAR(max), docpack int, [content] NVARCHAR(max), tree_parent int, tree_level int,  pos int, tree_loaded bit, tree_expanded bit, top_parent int);
	
	SET @sql =
		'WITH cteup AS(
			SELECT
				T.[id],
				T.[id_doctree],
				T.[num_doc],
				T.[docpack],
				T.[content],
				T.[id_parent],
				0 AS lvl
			FROM '+ @p_base+'_archive T
			WHERE T.[id] = ' + @p_id + ' and T.[id_doctree] in (select child from #children__doctree_id_doctree)
			UNION ALL
			SELECT
				T.[id],
				T.[id_doctree],
				T.[num_doc],
				T.[docpack],
				T.[content],
				T.[id_parent],
				[cteup].[lvl] + 1 AS lvl
			FROM '+ @p_base+'_archive T
			JOIN [cteup] ON T.[id] = [cteup].[id_parent]
		)
		INSERT INTO #r1 (ID, id_doctree, treetext, docpack, [content], tree_parent, tree_level, tree_loaded, tree_expanded) 
		SELECT ID, id_doctree, num_doc, docpack, [content], id_parent, ROW_NUMBER()OVER(ORDER BY lvl desc)-1, 1, 1 FROM cteup
		;';
	
	EXEC sp_executesql @sql;
		SET @sql ='
	select a.*, CASE WHEN
		(SELECT COUNT(*) AS Expr1
			FROM #r1 WHERE        
				(tree_parent = A.ID)
		) = 0 THEN 1 ELSE 0 END AS tree_leaf,
	b.name as doctree,c.[file] 
	from #r1 a 
	left join _doctree b on a.id_doctree=b.id 
	left join '+ @p_base+'_docversion c on a.id=c.id_archive and c.main=1 and c.del=0
	';

	EXEC sp_executesql 	@sql;

END






GO
/****** Object:  StoredProcedure [dbo].[GetTreeArchive]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		fazl
-- Create date: 02.04.2014
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetTreeArchive] 
(
	@p_base nvarchar(254), -- Имя текущей базы данных
	@p_id_archive int,
	@p_fulltree bit
)
AS
BEGIN
	DECLARE @sql AS NVARCHAR(MAX) =''	

	CREATE TABLE #AllArchive (ID int, name NVARCHAR(MAX), id_parent int);
	SET @sql = 'INSERT INTO #AllArchive SELECT a.id,''* ''+b.name+'' - ''+a.name as name,id_parent from dbo.'+@p_base+'_archive a left join '+@p_base+'_docname b on a.id_docname=b.id where a.id =' + CAST(@p_id_archive as nvarchar(max))
	EXEC sp_executesql 	@sql;
	-- Найдем всех родителей
	DECLARE @added AS  int=1
	while @added>0
	begin
		SET @sql = 'insert into #AllArchive SELECT a.id,b.name+'' - ''+a.name as name,id_parent from dbo.'+@p_base+'_archive a left join '+@p_base+'_docname b on a.id_docname=b.id where a.id in (select id_parent from #AllArchive) and a.id not in (select id from #AllArchive)'
		EXEC sp_executesql 	@sql;
		SET @added= @@ROWCOUNT;
	end
	IF (@p_fulltree=1)
	BEGIN
		SET @sql = 'INSERT INTO #AllArchive SELECT a.id,b.name+'' - ''+a.name as name,id_parent from dbo.'+@p_base+'_archive a left join '+@p_base+'_docname b on a.id_docname=b.id where a.id_parent in (select id from #AllArchive) and a.id not in (select id from #AllArchive)'
		EXEC sp_executesql 	@sql;
		SET @sql = 'INSERT INTO #AllArchive SELECT a.id,b.name+'' - ''+a.name as name,id_parent from dbo.'+@p_base+'_archive a left join '+@p_base+'_docname b on a.id_docname=b.id where a.id_parent in (select id from #AllArchive) and a.id not in (select id from #AllArchive)'
		EXEC sp_executesql 	@sql;
		SET @sql = 'INSERT INTO #AllArchive SELECT a.id,b.name+'' - ''+a.name as name,id_parent from dbo.'+@p_base+'_archive a left join '+@p_base+'_docname b on a.id_docname=b.id where a.id_parent in (select id from #AllArchive) and a.id not in (select id from #AllArchive)'
		EXEC sp_executesql 	@sql;
		SET @sql = 'INSERT INTO #AllArchive SELECT a.id,b.name+'' - ''+a.name as name,id_parent from dbo.'+@p_base+'_archive a left join '+@p_base+'_docname b on a.id_docname=b.id where a.id_parent in (select id from #AllArchive) and a.id not in (select id from #AllArchive)'
		EXEC sp_executesql 	@sql;
		SET @sql = 'INSERT INTO #AllArchive SELECT a.id,b.name+'' - ''+a.name as name,id_parent from dbo.'+@p_base+'_archive a left join '+@p_base+'_docname b on a.id_docname=b.id where a.id_parent in (select id from #AllArchive) and a.id not in (select id from #AllArchive)'
		EXEC sp_executesql 	@sql;
	END
	--select * from #AllArchive
	DECLARE @cur_parent AS int =0
	DECLARE @i AS int =0
	DECLARE @n_parents AS int =0
	CREATE TABLE #MainParents (rnum int,id int)
	CREATE TABLE #result (rn int identity not null,ID int, name NVARCHAR(MAX), id_parent int, lvl int, inlist NVARCHAR(MAX));
	CREATE TABLE #result2 (ID int, name NVARCHAR(MAX), tree_parent int, tree_level int, tree_leaf bit, tree_loaded bit,tree_expanded bit);
	SET @sql = 'INSERT INTO #MainParents SELECT ROW_NUMBER()OVER(ORDER BY [name]), id FROM #AllArchive WHERE id_parent=0 order by name'
	EXEC sp_executesql 	@sql
	--select * from #MainParents
	SET @n_parents=@@ROWCOUNT
	SET @i = 1
	WHILE @i <= @n_parents
	BEGIN
		SELECT @cur_parent = id FROM #MainParents WHERE rnum = @i
		;WITH
		 NumberedRecords(ID, [Name], id_parent, N, L) AS
			(
				SELECT ID, [Name], id_parent, CAST(STR(ROW_NUMBER()OVER(ORDER BY [Name],ID),11) AS VARCHAR(MAX)), Cast(0 as int)  
				FROM #AllArchive
			),
		 Tree(ID, [Name], id_parent, NodePath, lvl) AS
			(
				SELECT ID, [Name], id_parent, N, L FROM NumberedRecords WHERE ID=@cur_parent
				UNION ALL
				SELECT T.ID, T.[Name] , T.id_parent, Tree.NodePath+T.N, Tree.lvl+1
				FROM Tree JOIN NumberedRecords T ON Tree.ID=T.id_parent
			)	
		insert into #result (ID, name, id_parent,lvl) select ID,[Name],id_parent,lvl as inlist FROM Tree ORDER BY NodePath OPTION(MAXRECURSION 0);
		SET @i = @i + 1
	END
	--select * from #result
	
	/*CREATE TABLE #ChStructur (child int)
	select * from #result
	SET @n_parents=@@ROWCOUNT
	SET @i = 1
	DECLARE @cur_id AS int =0
	DECLARE @inlist AS NVARCHAR(MAX)
	WHILE @i <= @n_parents
	BEGIN
		select @cur_id=id from #result where rn=@i
		TRUNCATE TABLE #ChStructur
		INSERT #ChStructur VALUES(@cur_id)
		SET @sql ='
		WHILE @@ROWCOUNT > 0
		BEGIN
			INSERT #ChStructur
				SELECT a.id
				FROM '+@p_base+'_archive a JOIN #ChStructur c ON a.id_parent = c.child
				WHERE NOT EXISTS(SELECT 1 FROM #ChStructur WHERE child = a.id)
		END'
		EXEC sp_executesql 	@sql
		SET @inlist='-1'
		SELECT @inlist=@inlist+','+CAST(a.child as varchar(100)) FROM #ChStructur a 
		Update #result SET inlist = @inlist WHERE rn=@i
		SET @i = @i + 1
	END
	select * from #result

	SET @sql ='INSERT into '+@p_base+'_structur_list select ID, name, id_parent, lvl, inlist from #result'
	EXEC sp_executesql 	@sql
rn int identity not null,ID int, name NVARCHAR(MAX), id_parent int, lvl NVARCHAR(MAX), inlist NVARCHAR(MAX)
ID int, name NVARCHAR(MAX), tree_parent int, tree_level NVARCHAR(MAX), inlist NVARCHAR(MAX), tree_leaf bit, tree_loaded bit,tree_expanded bit*/
	insert into #result2 select -id, name, id_parent, lvl, 
	CASE WHEN
        (SELECT COUNT(*) AS Expr1
            FROM #result WHERE        
                (id_parent = A.ID)
        ) = 0 THEN 1 ELSE 0 END AS tree_leaf, 1, 1  from #result a order by rn
	select *  from #result2
	drop table #MainParents;
	drop table #AllArchive;
	drop table #result;
	drop table #result2;
	--drop table #ChStructur;
	
END






GO
/****** Object:  StoredProcedure [dbo].[GetTreeDoctree]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		fazl
-- Create date: 06.05.2014
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetTreeDoctree] 
(
	@p_base nvarchar(254), -- Имя текущей базы данных
	@p_where nvarchar(254)='',
	@p_doctype nvarchar(254) = '0', -- Какую из основных веток показывать
	@p_doctype_acc nvarchar(254) = '0', -- Тип документа
	@p_doctype_dog nvarchar(254) = '0', -- Тип документа
	@p_doctype_ord nvarchar(254) = '0', -- Тип документа
	@p_doctype_oth nvarchar(254) = '0', -- Тип документа
	@p_doctype_empl nvarchar(254) = '0', -- Тип документа
	@p_doctype_ohs nvarchar(254) = '0', -- Тип документа
	@p_doctype_tech nvarchar(254) = '0' -- Тип документа
)
AS
BEGIN
DECLARE @sql AS NVARCHAR(MAX) =''

DECLARE @doctype AS NVARCHAR(MAX) =' a.top_parent in ( -1'
	IF (@p_doctype='10000')
	BEGIN
		IF (@p_doctype_acc='1') SET @doctype=@doctype +',7'
		IF (@p_doctype_dog='1') SET @doctype=@doctype +',15'
		IF (@p_doctype_ord='1') SET @doctype=@doctype +',2'
		IF (@p_doctype_oth='1') SET @doctype=@doctype +',56'
		IF (@p_doctype_empl='1') SET @doctype=@doctype +',50'
		IF (@p_doctype_ohs='1') SET @doctype=@doctype +',60'
		IF (@p_doctype_tech='1') SET @doctype=@doctype +',11'
	END
	ELSE SET @doctype=@doctype +',' + @p_doctype

IF (@p_where='')
BEGIN
	SET @sql = N'SELECT a.* FROM ['+@p_base+'_doctree_pre] a WHERE ' + @doctype+')'
	EXEC sp_executesql 	@sql
END
ELSE
BEGIN
	CREATE TABLE #doctree_temp(id int,treetext nvarchar(250),tree_loaded bit,tree_parent int,tree_level int,tree_leaf int,pos int,tree_expanded bit);
	
	SET @sql = N'INSERT INTO #doctree_temp SELECT a.id,a.treetext,a.tree_loaded,a.tree_parent,a.tree_level,a.tree_leaf,a.pos,1 as tree_expanded FROM ['+@p_base+'_doctree_pre] a WHERE treetext LIKE ''%'+@p_where+'%'' AND '+ @doctype +')' 
	EXEC sp_executesql 	@sql;
	
	SET @sql = N'INSERT INTO #doctree_temp SELECT a.id,a.treetext,a.tree_loaded,a.tree_parent,a.tree_level,a.tree_leaf,a.pos,1 as tree_expanded FROM ['+@p_base+'_doctree_pre] a WHERE a.id in (select tree_parent from #doctree_temp) and a.id not in (select id from #doctree_temp)'
	EXEC sp_executesql 	@sql;
	SET @sql = N'INSERT INTO #doctree_temp SELECT a.id,a.treetext,a.tree_loaded,a.tree_parent,a.tree_level,a.tree_leaf,a.pos,1 as tree_expanded FROM ['+@p_base+'_doctree_pre] a WHERE a.id in (select tree_parent from #doctree_temp) and a.id not in (select id from #doctree_temp)'
	EXEC sp_executesql 	@sql;
	SET @sql = N'INSERT INTO #doctree_temp SELECT a.id,a.treetext,a.tree_loaded,a.tree_parent,a.tree_level,a.tree_leaf,a.pos,1 as tree_expanded FROM ['+@p_base+'_doctree_pre] a WHERE a.id in (select tree_parent from #doctree_temp) and a.id not in (select id from #doctree_temp)'
	EXEC sp_executesql 	@sql;
	SET @sql = N'INSERT INTO #doctree_temp SELECT a.id,a.treetext,a.tree_loaded,a.tree_parent,a.tree_level,a.tree_leaf,a.pos,1 as tree_expanded FROM ['+@p_base+'_doctree_pre] a WHERE a.id in (select tree_parent from #doctree_temp) and a.id not in (select id from #doctree_temp)'
	EXEC sp_executesql 	@sql;
	SET @sql = N'INSERT INTO #doctree_temp SELECT a.id,a.treetext,a.tree_loaded,a.tree_parent,a.tree_level,a.tree_leaf,a.pos,1 as tree_expanded FROM ['+@p_base+'_doctree_pre] a WHERE a.id in (select tree_parent from #doctree_temp) and a.id not in (select id from #doctree_temp)'
	EXEC sp_executesql 	@sql;
	SELECT * FROM #doctree_temp ORDER BY pos
	IF OBJECT_ID(N'tempdb..#doctree_temp', N'U') IS NOT NULL drop table #doctree_temp
END

END



GO
/****** Object:  StoredProcedure [dbo].[GetTreeStructur]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		fazl
-- Create date: 06.05.2014
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetTreeStructur] 
(
	@p_base nvarchar(254), -- Имя текущей базы данных
	@p_where nvarchar(254)=''
)
AS
BEGIN
DECLARE @sql AS NVARCHAR(MAX) =''

IF (@p_where='')
BEGIN
	SET @sql = N'SELECT a.* FROM ['+@p_base+'_structur_pre] a '
	EXEC sp_executesql 	@sql
END
ELSE
BEGIN
	CREATE TABLE #structur_temp(id int,name nvarchar(250),tree_loaded bit,tree_parent int,tree_level int,tree_leaf int,pos int,tree_expanded bit);
	
	SET @sql = N'INSERT INTO #structur_temp SELECT a.id,a.name,a.tree_loaded,a.tree_parent,a.tree_level,1 as tree_leaf,a.pos,1 as tree_expanded FROM ['+@p_base+'_structur_pre] a WHERE name LIKE ''%'+@p_where+'%''' 
	EXEC sp_executesql 	@sql;
	
	SET @sql = N'INSERT INTO #structur_temp SELECT a.id,a.name,a.tree_loaded,a.tree_parent,a.tree_level,a.tree_leaf,a.pos,1 as tree_expanded FROM ['+@p_base+'_structur_pre] a WHERE a.id in (select tree_parent from #structur_temp) and a.id not in (select id from #structur_temp)'
	EXEC sp_executesql 	@sql;
	SET @sql = N'INSERT INTO #structur_temp SELECT a.id,a.name,a.tree_loaded,a.tree_parent,a.tree_level,a.tree_leaf,a.pos,1 as tree_expanded FROM ['+@p_base+'_structur_pre] a WHERE a.id in (select tree_parent from #structur_temp) and a.id not in (select id from #structur_temp)'
	EXEC sp_executesql 	@sql;
	SET @sql = N'INSERT INTO #structur_temp SELECT a.id,a.name,a.tree_loaded,a.tree_parent,a.tree_level,a.tree_leaf,a.pos,1 as tree_expanded FROM ['+@p_base+'_structur_pre] a WHERE a.id in (select tree_parent from #structur_temp) and a.id not in (select id from #structur_temp)'
	EXEC sp_executesql 	@sql;
	SET @sql = N'INSERT INTO #structur_temp SELECT a.id,a.name,a.tree_loaded,a.tree_parent,a.tree_level,a.tree_leaf,a.pos,1 as tree_expanded FROM ['+@p_base+'_structur_pre] a WHERE a.id in (select tree_parent from #structur_temp) and a.id not in (select id from #structur_temp)'
	EXEC sp_executesql 	@sql;
	SET @sql = N'INSERT INTO #structur_temp SELECT a.id,a.name,a.tree_loaded,a.tree_parent,a.tree_level,a.tree_leaf,a.pos,1 as tree_expanded FROM ['+@p_base+'_structur_pre] a WHERE a.id in (select tree_parent from #structur_temp) and a.id not in (select id from #structur_temp)'
	EXEC sp_executesql 	@sql;
	SELECT * FROM #structur_temp ORDER BY pos
	IF OBJECT_ID(N'tempdb..#structur_temp', N'U') IS NOT NULL drop table #structur_temp
END

END




GO
/****** Object:  StoredProcedure [dbo].[PreDepartmentTree]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		fazl
-- Create date: 25.06.2014
-- Description:	ПЕРЕПИСАТЬ!!!! Полный бардак!
-- =============================================
CREATE PROCEDURE [dbo].[PreDepartmentTree] @from nvarchar(30) = NULL, @to nvarchar(30) = NULL
AS
BEGIN
	DECLARE @sql AS NVARCHAR(MAX) =''	
	CREATE TABLE #AllDoctree (ID int, name NVARCHAR(MAX), id_parent int);

	SET @sql = 'INSERT INTO #AllDoctree SELECT id, name ,id_parent from [dbo].['+@from+'] where del=0 order by name'
	EXEC sp_executesql 	@sql;

	DECLARE @cur_parent AS int =0
	DECLARE @i AS int =0
	DECLARE @n_parents AS int =0
	
	CREATE TABLE #MainParents (rnum int,id int)
	CREATE TABLE #result (rn int identity not null,ID int, name NVARCHAR(max), id_parent int, lvl int,cp int);
	CREATE TABLE #result2 (ID int, treetext NVARCHAR(max),  tree_parent int, tree_level int, tree_leaf bit, pos int, tree_loaded bit,tree_expanded bit,top_parent int);
	
	SET @sql = 'INSERT INTO #MainParents SELECT ROW_NUMBER()OVER(ORDER BY [name]), id FROM #AllDoctree WHERE id_parent=0 order by name'
	EXEC sp_executesql 	@sql
	--select * from #MainParents
	
	SET @n_parents=@@ROWCOUNT
	SET @i = 1
	WHILE @i <= @n_parents
	BEGIN
		SELECT @cur_parent = id FROM #MainParents WHERE rnum = @i
		;WITH
		 NumberedRecords(ID, [Name], id_parent, N, L) AS
			(
				SELECT ID, [Name],  id_parent, CAST(STR(ROW_NUMBER()OVER(ORDER BY [Name],ID),11) AS VARCHAR(MAX)), Cast(0 as int)  
				FROM #AllDoctree
			),
		 Tree(ID, [Name],  id_parent, NodePath, lvl, cp) AS
			(
				SELECT ID, [Name], id_parent, N, L, @cur_parent as cp FROM NumberedRecords WHERE ID=@cur_parent
				UNION ALL
				SELECT T.ID, T.[Name],  T.id_parent, Tree.NodePath+T.N, Tree.lvl+1, @cur_parent as cp
				FROM Tree JOIN NumberedRecords T ON Tree.ID=T.id_parent
			)	
		insert into #result (ID, name, id_parent,lvl,cp) select ID,[Name], id_parent,lvl,cp FROM Tree ORDER BY NodePath OPTION(MAXRECURSION 0);
		SET @i = @i + 1
	END
	--select * from #result
	
	

	--update
	insert into #result2 select id, name, id_parent, lvl, 
	CASE WHEN
        (SELECT COUNT(*) AS Expr1
            FROM #result WHERE        
                (id_parent = A.ID)
        ) = 0 THEN 1 ELSE 0 END AS tree_leaf, rn, 1, 0,cp  from #result a order by rn
	


	--SET @sql = 'INSERT INTO #AllDoctree SELECT id,name,id_parent, from dbo._doctree order by name'
	
	SET @sql = 'TRUNCATE TABLE [dbo].['+@to+']'
	EXEC sp_executesql 	@sql;

	SET @sql = 'INSERT INTO [dbo].['+@to+'] 
	([id],[treetext],[tree_loaded],[tree_parent],[tree_level],[tree_leaf],[pos],[top_parent],[tree_expanded])	
	SELECT 
	 [id],[treetext],[tree_loaded],[tree_parent],[tree_level],[tree_leaf],[pos],[top_parent],[tree_expanded]
	FROM #result2 order by pos'
	EXEC sp_executesql 	@sql;

	drop table #MainParents;
	drop table #AllDoctree;
	drop table #result;
	drop table #result2;

END







GO
/****** Object:  StoredProcedure [dbo].[PreDepartmentTree1c]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		fazl
-- Create date: 25.06.2014
-- Description:	ПЕРЕПИСАТЬ!!!! Полный бардак!
-- =============================================
CREATE PROCEDURE [dbo].[PreDepartmentTree1c] 
AS
BEGIN
	DECLARE @sql AS NVARCHAR(MAX) =''	
	CREATE TABLE #AllDoctree (ID nvarchar(50), name NVARCHAR(MAX), id_parent nvarchar(50));
	IF NOT EXISTS (SELECT name FROM tempdb..sysindexes WHERE name = 'IDX_#AllDoctreePreDepartmentTree1cID')
		CREATE NONCLUSTERED INDEX IDX_#AllDoctreePreDepartmentTree1cID ON [dbo].[#AllDoctree] (ID) ON [PRIMARY]
	IF NOT EXISTS (SELECT name FROM tempdb..sysindexes WHERE name = 'IDX_#AllDoctreePreDepartmentTree1cparent')
	CREATE NONCLUSTERED INDEX IDX_#AllDoctreePreDepartmentTree1cparent ON [dbo].[#AllDoctree] (id_parent) ON [PRIMARY]

	SET @sql = '
	INSERT INTO #AllDoctree 
	SELECT id+''-13'',
	org_name,
	'''' as parent
	from
	[dbo].[_organization_1c]
	UNION ALL
	SELECT id, 
	[full_name],  
	CASE 
      WHEN [parent_ID] = '''' THEN [organization_id]+''-13''   
	  ELSE [parent_ID]
	END as parent 
	FROM [dbo].[_department_1c]'
	
	EXEC sp_executesql 	@sql;
	--select * from #alldoctree;

	DECLARE @cur_parent AS nvarchar(50) = ''
	DECLARE @i AS int =0
	DECLARE @n_parents AS int =0
	
	CREATE TABLE #MainParents (rnum int,id nvarchar(50))
	CREATE TABLE #result (rn int identity not null,ID nvarchar(50), name NVARCHAR(max), id_parent nvarchar(50), lvl int, cp nvarchar(50));
	CREATE TABLE #result2 (ID nvarchar(50), treetext NVARCHAR(max),  tree_parent nvarchar(50), tree_level int, tree_leaf bit, pos int, tree_loaded bit,tree_expanded bit,top_parent nvarchar(50));
	
	SET @sql = 'INSERT INTO #MainParents SELECT ROW_NUMBER()OVER(ORDER BY [name]), id FROM #AllDoctree WHERE id_parent='''' order by name'
	EXEC sp_executesql 	@sql
	
	
	SET @n_parents=@@ROWCOUNT
	SET @i = 1
	WHILE @i <= @n_parents
	BEGIN
		SELECT @cur_parent = id FROM #MainParents WHERE rnum = @i
		;WITH
		 NumberedRecords(ID, [Name], id_parent, N, L) AS
			(
				SELECT ID, [Name],  id_parent, CAST(STR(ROW_NUMBER()OVER(ORDER BY [Name],ID),16) AS VARCHAR(MAX)), Cast(0 as int)  
				FROM #AllDoctree
			),
		 Tree(ID, [Name],  id_parent, NodePath, lvl, cp) AS
			(
				SELECT ID, [Name], id_parent, N, L, @cur_parent as cp FROM NumberedRecords WHERE ID=@cur_parent
				UNION ALL
				SELECT T.ID, T.[Name],  T.id_parent, Tree.NodePath+T.N, Tree.lvl+1, @cur_parent as cp
				FROM Tree JOIN NumberedRecords T ON Tree.ID=T.id_parent
			)	
		insert into #result (ID, name, id_parent,lvl,cp) select ID,[Name], id_parent, lvl, cp FROM Tree ORDER BY NodePath OPTION(MAXRECURSION 50);
		SET @i = @i + 1
	END
	select * from #result
	
	

	--update
	insert into #result2 select id, name, id_parent, lvl, 
	CASE WHEN
        (SELECT COUNT(*) AS Expr1
            FROM #result WHERE        
                (id_parent = A.ID)
        ) = 0 THEN 1 ELSE 0 END AS tree_leaf, rn, 1, 0,cp  from #result a order by rn
	


	--SET @sql = 'INSERT INTO #AllDoctree SELECT id,name,id_parent, from dbo._doctree order by name'
	
	SET @sql = 'TRUNCATE TABLE [dbo].[_department_1c_pre]'
	EXEC sp_executesql 	@sql;

	SET @sql = 'INSERT INTO [dbo].[_department_1c_pre] 
	([id],[treetext],[tree_loaded],[tree_parent],[tree_level],[tree_leaf],[pos],[top_parent],[tree_expanded])	
	SELECT 
	 [id],[treetext],[tree_loaded],[tree_parent],[tree_level],[tree_leaf],[pos],[top_parent],[tree_expanded]
	FROM #result2 order by pos'
	EXEC sp_executesql 	@sql;

	drop table #MainParents;
	drop table #AllDoctree;
	drop table #result;
	drop table #result2;

END








GO
/****** Object:  StoredProcedure [dbo].[PreDocTree]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		fazl
-- Create date: 25.06.2014
-- Description:	ПЕРЕПИСАТЬ!!!! Полный бардак!
-- =============================================
CREATE PROCEDURE [dbo].[PreDocTree] 
AS
BEGIN
	DECLARE @sql AS NVARCHAR(MAX) =''	
	CREATE TABLE #AllDoctree (ID int, name NVARCHAR(MAX), id_parent int);

	SET @sql = 'INSERT INTO #AllDoctree SELECT id, (CASE WHEN LEN(form) > 0 THEN (name + '' ('' + form + '')'') ELSE name END) as name ,id_parent from dbo._doctree where del=0 order by name'
	EXEC sp_executesql 	@sql;

	DECLARE @cur_parent AS int =0
	DECLARE @i AS int =0
	DECLARE @n_parents AS int =0
	
	CREATE TABLE #MainParents (rnum int,id int)
	CREATE TABLE #result (rn int identity not null,ID int, name NVARCHAR(max), id_parent int, lvl int,cp int);
	CREATE TABLE #result2 (ID int, treetext NVARCHAR(max),  tree_parent int, tree_level int, tree_leaf bit, pos int, tree_loaded bit,tree_expanded bit,top_parent int);
	
	SET @sql = 'INSERT INTO #MainParents SELECT ROW_NUMBER()OVER(ORDER BY [name]), id FROM #AllDoctree WHERE id_parent=0 order by name'
	EXEC sp_executesql 	@sql
	--select * from #MainParents
	
	SET @n_parents=@@ROWCOUNT
	SET @i = 1
	WHILE @i <= @n_parents
	BEGIN
		SELECT @cur_parent = id FROM #MainParents WHERE rnum = @i
		;WITH
		 NumberedRecords(ID, [Name], id_parent, N, L) AS
			(
				SELECT ID, [Name],  id_parent, CAST(STR(ROW_NUMBER()OVER(ORDER BY [Name],ID),11) AS VARCHAR(MAX)), Cast(0 as int)  
				FROM #AllDoctree
			),
		 Tree(ID, [Name],  id_parent, NodePath, lvl, cp) AS
			(
				SELECT ID, [Name], id_parent, N, L, @cur_parent as cp FROM NumberedRecords WHERE ID=@cur_parent
				UNION ALL
				SELECT T.ID, T.[Name],  T.id_parent, Tree.NodePath+T.N, Tree.lvl+1, @cur_parent as cp
				FROM Tree JOIN NumberedRecords T ON Tree.ID=T.id_parent
			)	
		insert into #result (ID, name, id_parent,lvl,cp) select ID,[Name], id_parent,lvl,cp FROM Tree ORDER BY NodePath OPTION(MAXRECURSION 0);
		SET @i = @i + 1
	END
	--select * from #result
	
	

	--update
	insert into #result2 select id, name, id_parent, lvl, 
	CASE WHEN
        (SELECT COUNT(*) AS Expr1
            FROM #result WHERE        
                (id_parent = A.ID)
        ) = 0 THEN 1 ELSE 0 END AS tree_leaf, rn, 1, 0,cp  from #result a order by rn
	


	--SET @sql = 'INSERT INTO #AllDoctree SELECT id,name,id_parent, from dbo._doctree order by name'
	
	SET @sql = 'TRUNCATE TABLE [dbo].[_doctree_pre]'
	EXEC sp_executesql 	@sql;

	SET @sql = 'INSERT INTO [dbo].[_doctree_pre] 
	([id],[treetext],[tree_loaded],[tree_parent],[tree_level],[tree_leaf],[pos],[top_parent],[tree_expanded])	
	SELECT 
	 [id],[treetext],[tree_loaded],[tree_parent],[tree_level],[tree_leaf],[pos],[top_parent],[tree_expanded]
	FROM #result2 order by pos'
	EXEC sp_executesql 	@sql;



	drop table #MainParents;
	drop table #AllDoctree;
	drop table #result;
	drop table #result2;

	
END






GO
/****** Object:  StoredProcedure [dbo].[PreStructur]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		fazl
-- Create date: 25.06.2014
-- Description:	ПЕРЕПИСАТЬ!!!! Полный бардак!
-- =============================================
CREATE PROCEDURE [dbo].[PreStructur] 
AS
BEGIN
	DECLARE @sql AS NVARCHAR(MAX) =''	
	CREATE TABLE #AllStructur (ID int, name NVARCHAR(MAX), id_parent int);

	SET @sql = 'INSERT INTO #AllStructur SELECT id,name,id_parent from dbo._structur order by name'
	EXEC sp_executesql 	@sql;

	DECLARE @cur_parent AS int =0
	DECLARE @i AS int =0
	DECLARE @n_parents AS int =0
	
	CREATE TABLE #MainParents (rnum int,id int)
	CREATE TABLE #result (rn int identity not null,ID int, name NVARCHAR(250), id_parent int, lvl int,cp int);
	CREATE TABLE #result2 (ID int, treetext NVARCHAR(250), tree_parent int, tree_level int, tree_leaf bit, pos int, tree_loaded bit,tree_expanded bit,top_parent int);
	
	SET @sql = 'INSERT INTO #MainParents SELECT ROW_NUMBER()OVER(ORDER BY [name]), id FROM #AllStructur WHERE id_parent=0 order by name'
	EXEC sp_executesql 	@sql
	--select * from #MainParents
	
	SET @n_parents=@@ROWCOUNT
	SET @i = 1
	WHILE @i <= @n_parents
	BEGIN
		SELECT @cur_parent = id FROM #MainParents WHERE rnum = @i
		;WITH
		 NumberedRecords(ID, [Name], id_parent, N, L) AS
			(
				SELECT ID, [Name], id_parent, CAST(STR(ROW_NUMBER()OVER(ORDER BY [Name],ID),11) AS VARCHAR(MAX)), Cast(0 as int)  
				FROM #AllStructur
			),
		 Tree(ID, [Name], id_parent, NodePath, lvl, cp) AS
			(
				SELECT ID, [Name], id_parent, N, L, @cur_parent as cp FROM NumberedRecords WHERE ID=@cur_parent
				UNION ALL
				SELECT T.ID, T.[Name] , T.id_parent, Tree.NodePath+T.N, Tree.lvl+1, @cur_parent as cp
				FROM Tree JOIN NumberedRecords T ON Tree.ID=T.id_parent
			)	
		insert into #result (ID, name, id_parent,lvl,cp) select ID,[Name],id_parent,lvl,cp FROM Tree ORDER BY NodePath OPTION(MAXRECURSION 0);
		SET @i = @i + 1
	END
	--select * from #result
	
	

	--update
	insert into #result2 select id, name, id_parent, lvl, 
	CASE WHEN
        (SELECT COUNT(*) AS Expr1
            FROM #result WHERE        
                (id_parent = A.ID)
        ) = 0 THEN 1 ELSE 0 END AS tree_leaf, rn, 1, 0,cp  from #result a order by rn
	


	SET @sql = 'INSERT INTO #AllStructur SELECT id,name,id_parent from dbo._structur order by name'
	
	SET @sql = 'TRUNCATE TABLE [dbo].[_structur_pre]'
	EXEC sp_executesql 	@sql;

	SET @sql = 'INSERT INTO [dbo].[_structur_pre] 
	([id],[treetext],[tree_loaded],[tree_parent],[tree_level],[tree_leaf],[pos],[top_parent],[tree_expanded])	
	SELECT 
	 [id],[treetext],[tree_loaded],[tree_parent],[tree_level],[tree_leaf],[pos],[top_parent],[tree_expanded]
	FROM #result2 order by pos'
	EXEC sp_executesql 	@sql;



	drop table #MainParents;
	drop table #AllStructur;
	drop table #result;
	drop table #result2;

	
END







GO
/****** Object:  Table [dbo].[_access]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_access](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[type] [tinyint] NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__access_del]  DEFAULT ((0)),
 CONSTRAINT [PK_access] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_access_name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_base]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_base](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[namerus] [nvarchar](100) NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__bases_enabled]  DEFAULT ((0)),
	[tabindex] [int] NOT NULL CONSTRAINT [DF__base_tabindex]  DEFAULT ((0)),
	[active] [bit] NOT NULL CONSTRAINT [DF__base_active]  DEFAULT ((1)),
	[id_frm] [int] NOT NULL CONSTRAINT [DF__base_id_frm]  DEFAULT ((0)),
 CONSTRAINT [PK_base] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_doctree]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_doctree](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_parent] [int] NOT NULL,
	[name] [nvarchar](250) NOT NULL,
	[form] [nvarchar](100) NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__doctree_del]  DEFAULT ((0)),
 CONSTRAINT [PK_doctree] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_doctree_pre]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_doctree_pre](
	[id] [int] NOT NULL,
	[treetext] [nvarchar](max) NOT NULL,
	[tree_loaded] [bit] NOT NULL,
	[tree_parent] [int] NOT NULL,
	[tree_level] [int] NOT NULL,
	[tree_leaf] [bit] NOT NULL,
	[pos] [int] NOT NULL,
	[top_parent] [int] NOT NULL,
	[tree_expanded] [bit] NOT NULL,
 CONSTRAINT [PK_doctree_pre] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_doctype]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_doctype](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__doctype_del]  DEFAULT ((0)),
 CONSTRAINT [PK_a_doctype] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_edittype]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[_edittype](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__edittype_del]  DEFAULT ((0)),
 CONSTRAINT [PK_edittype] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[_frm]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_frm](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](120) NOT NULL,
	[name_full] [nvarchar](254) NOT NULL,
	[inn] [nvarchar](15) NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__frm_del]  DEFAULT ((0)),
	[id_1c] [nvarchar](50) NOT NULL CONSTRAINT [DF__frm_id_uso]  DEFAULT (''),
 CONSTRAINT [PK_frm] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_journal]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_journal](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[when] [datetime] NOT NULL,
	[id_user] [int] NOT NULL,
	[id_edittype] [tinyint] NOT NULL,
	[id_base] [tinyint] NOT NULL CONSTRAINT [DF__journal_id_base]  DEFAULT ((0)),
	[id_table] [tinyint] NOT NULL,
	[change_id] [int] NOT NULL,
	[changes] [nvarchar](max) NOT NULL,
	[score] [tinyint] NOT NULL CONSTRAINT [DF__journal_score]  DEFAULT ((0)),
	[del] [bit] NOT NULL CONSTRAINT [DF__journal_del]  DEFAULT ((0)),
 CONSTRAINT [PK_journal] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_log]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_log](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NOT NULL,
	[when] [smalldatetime] NOT NULL,
	[id_type] [int] NOT NULL CONSTRAINT [DF__log_id_type]  DEFAULT ((0)),
	[what] [nvarchar](max) NOT NULL CONSTRAINT [DF__log_what]  DEFAULT (''),
	[del] [bit] NOT NULL CONSTRAINT [DF_log_del]  DEFAULT ((0)),
 CONSTRAINT [PK_log] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_logtype]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_logtype](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF_logtype_del]  DEFAULT ((0)),
 CONSTRAINT [PK_logtype] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_role]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__roles_deleted]  DEFAULT ((0)),
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_role_access]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_role_access](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_access] [int] NOT NULL,
	[id_role] [int] NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__role_access_del]  DEFAULT ((0)),
 CONSTRAINT [PK_role_access] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_role_where]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_role_where](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_role] [int] NOT NULL,
	[id_table] [int] NOT NULL,
	[value] [nvarchar](max) NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__where_del]  DEFAULT ((0)),
 CONSTRAINT [PK_role_where] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_site_setting]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_site_setting](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[key] [nvarchar](100) NOT NULL,
	[value] [nvarchar](max) NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF_site_setting_del]  DEFAULT ((0)),
 CONSTRAINT [PK_site_setting] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_table]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_table](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](250) NULL,
	[common] [bit] NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__table_del]  DEFAULT ((0)),
 CONSTRAINT [PK_table] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_truefalse]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_truefalse](
	[id] [smallint] NOT NULL,
	[name] [nchar](3) NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF_truefalse_del]  DEFAULT ((0))
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_user]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](100) NOT NULL,
	[name] [nvarchar](250) NOT NULL,
	[mail] [nvarchar](100) NOT NULL,
	[watch] [tinyint] NOT NULL CONSTRAINT [DF__user_watch]  DEFAULT ((1)),
	[del] [bit] NOT NULL CONSTRAINT [DF__users_deleted]  DEFAULT ((0)),
	[id_department] [int] NOT NULL CONSTRAINT [DF__user_id_department]  DEFAULT ((0)),
	[sname] [nvarchar](250) NOT NULL CONSTRAINT [DF__user_sname]  DEFAULT (''),
 CONSTRAINT [PK__user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_user_access]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_user_access](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NOT NULL,
	[id_access] [int] NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF_user_access_del]  DEFAULT ((0)),
 CONSTRAINT [PK_user_access] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_user_role_base]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_user_role_base](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NOT NULL,
	[id_role] [int] NOT NULL,
	[id_base] [int] NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__users_roles_bases_deleted]  DEFAULT ((0)),
 CONSTRAINT [PK_user_role_base] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_user_setting]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_user_setting](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NOT NULL CONSTRAINT [DF__setting_id_user]  DEFAULT ((0)),
	[key] [nvarchar](100) NULL,
	[value] [nvarchar](max) NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__user_setting_del]  DEFAULT ((0)),
	[last_upd] [datetime] NULL,
 CONSTRAINT [PK_user_setting] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_yesno]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_yesno](
	[id] [smallint] NOT NULL,
	[name] [nchar](3) NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF__yesno_del]  DEFAULT ((0))
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[main_archive]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[main_archive](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[num_doc] [nvarchar](250) NOT NULL,
	[date_doc] [date] NULL,
	[date_upd] [datetime] NULL,
	[prim] [nvarchar](254) NOT NULL,
	[content] [nvarchar](254) NOT NULL CONSTRAINT [DF_main_archive_content]  DEFAULT (''),
	[id_frm_contr] [int] NOT NULL CONSTRAINT [DF_main_archive_id_frm2]  DEFAULT ((0)),
	[id_frm_prod] [int] NOT NULL CONSTRAINT [DF_main_archive_id_frm_prod]  DEFAULT ((0)),
	[id_frm_dev] [int] NOT NULL CONSTRAINT [DF_main_archive_id_frm_dev]  DEFAULT ((0)),
	[id_doctree] [int] NOT NULL CONSTRAINT [DF_main_archive_id_docname]  DEFAULT ((0)),
	[id_doctype] [int] NOT NULL CONSTRAINT [DF_main_archive_id_doctype]  DEFAULT ((0)),
	[id_user] [int] NOT NULL CONSTRAINT [DF_main_archive_id_user]  DEFAULT ((0)),
	[summ] [numeric](19, 2) NOT NULL CONSTRAINT [DF_main_archive_summ]  DEFAULT ((0)),
	[docpack] [int] NOT NULL CONSTRAINT [DF_main_archive_docpack]  DEFAULT ((0)),
	[id_depart] [int] NOT NULL CONSTRAINT [DF_main_archive_id_depart]  DEFAULT ((0)),
	[id_perf] [int] NOT NULL CONSTRAINT [DF_main_archive_id_perf]  DEFAULT ((0)),
	[doctext] [text] NOT NULL,
	[id_prjcode] [int] NOT NULL CONSTRAINT [DF_main_archive_id_prj_code]  DEFAULT ((0)),
	[hidden] [tinyint] NOT NULL CONSTRAINT [DF_main_archive_hiden]  DEFAULT ((1)),
	[id_parent] [int] NOT NULL CONSTRAINT [DF_main_archive_id_parent]  DEFAULT ((0)),
	[indexd] [nvarchar](20) NOT NULL CONSTRAINT [DF_main_archive_indexd]  DEFAULT (''),
	[del] [bit] NOT NULL CONSTRAINT [DF_main_archive_del]  DEFAULT ((0)),
	[id_state] [int] NOT NULL CONSTRAINT [DF_main_archive_id_state]  DEFAULT ((0)),
	[accept] [tinyint] NOT NULL CONSTRAINT [DF_main_archive_accept]  DEFAULT ((0)),
	[servprim] [nvarchar](254) NOT NULL CONSTRAINT [DF_main_archive_servprim]  DEFAULT (''),
	[supervisor_checked] [tinyint] NOT NULL CONSTRAINT [DF_main_archive_supervisor_checked]  DEFAULT ((1)),
 CONSTRAINT [PK_main_archive] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[main_docversion]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[main_docversion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_archive] [int] NOT NULL CONSTRAINT [DF_main_docversion_id_archive]  DEFAULT ((0)),
	[nn] [int] NOT NULL,
	[date_reg] [date] NULL,
	[file] [nvarchar](250) NOT NULL,
	[main] [tinyint] NOT NULL CONSTRAINT [DF_main_docversion_main]  DEFAULT ((0)),
	[date_trans] [date] NULL,
	[barcode] [numeric](10, 0) NOT NULL CONSTRAINT [DF_main_docversion_barcode]  DEFAULT ((0)),
	[id_source] [int] NULL CONSTRAINT [DF_main_docversion_id_source]  DEFAULT ((0)),
	[id_status] [int] NOT NULL CONSTRAINT [DF_main_docversion_id_status]  DEFAULT ((0)),
	[file_size] [int] NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF_main_docversion_del]  DEFAULT ((0)),
	[id_quality] [tinyint] NOT NULL CONSTRAINT [DF_main_docversion_id_quality]  DEFAULT ((1)),
 CONSTRAINT [PK_main_docversion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[main_person]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[main_person](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](25) NOT NULL,
	[name_full] [nvarchar](50) NOT NULL,
	[del] [bit] NOT NULL CONSTRAINT [DF_main_person_del]  DEFAULT ((0)),
	[id_depart] [int] NOT NULL CONSTRAINT [DF_main_person_id_depart]  DEFAULT ((0)),
	[id_1c] [nvarchar](50) NOT NULL CONSTRAINT [DF_main_person_id_1c]  DEFAULT (''),
	[prim] [nvarchar](100) NOT NULL CONSTRAINT [DF_main_person_prim]  DEFAULT (''),
	[id_status] [int] NOT NULL CONSTRAINT [DF_main_person_id_status]  DEFAULT ((1)),
 CONSTRAINT [PK_main_person] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[main_docversion_archive]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[main_docversion_archive]
AS
SELECT        a.id, a.id_archive, a.nn, a.date_reg, a.[file], a.main, a.date_trans, a.barcode, a.id_source, a.id_status, a.file_size, a.del, b.docpack, b.id_doctree, b.id_frm_contr, 
                         b.id_doctype, b.id_user, b.date_doc, b.num_doc, b.id_perf, b.summ, b.id_depart, b.date_upd, b.id_state, b.[content], a.id_quality, '' AS tp
FROM            dbo.main_docversion AS a LEFT OUTER JOIN
                         dbo.main_archive AS b ON a.id_archive = b.id

GO
/****** Object:  Index [IX_access_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_access_del] ON [dbo].[_access]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_access_type]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_access_type] ON [dbo].[_access]
(
	[type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_base_active]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_base_active] ON [dbo].[_base]
(
	[active] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_base_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_base_del] ON [dbo].[_base]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_base_name]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_base_name] ON [dbo].[_base]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_id_frm]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_id_frm] ON [dbo].[_base]
(
	[id_frm] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [_dta_index__doctree_27_753202975__K5_K1_3]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [_dta_index__doctree_27_753202975__K5_K1_3] ON [dbo].[_doctree]
(
	[del] ASC,
	[id] ASC
)
INCLUDE ( 	[name]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_doctree_form]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_doctree_form] ON [dbo].[_doctree]
(
	[form] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_doctree_id_parent]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_doctree_id_parent] ON [dbo].[_doctree]
(
	[id_parent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_doctree_name]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_doctree_name] ON [dbo].[_doctree]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_doctree_pre_pos]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_doctree_pre_pos] ON [dbo].[_doctree_pre]
(
	[pos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_doctree_pre_top_parent]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_doctree_pre_top_parent] ON [dbo].[_doctree_pre]
(
	[top_parent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_doctree_pre_tree_parent]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_doctree_pre_tree_parent] ON [dbo].[_doctree_pre]
(
	[tree_parent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_doctype_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_doctype_del] ON [dbo].[_doctype]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_edittype_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_edittype_del] ON [dbo].[_edittype]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_frm_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_frm_del] ON [dbo].[_frm]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_frm_del_id]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_frm_del_id] ON [dbo].[_frm]
(
	[del] ASC,
	[id] ASC
)
INCLUDE ( 	[name]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_frm_id_1c]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_frm_id_1c] ON [dbo].[_frm]
(
	[id_1c] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_frm_inn]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_frm_inn] ON [dbo].[_frm]
(
	[inn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_frm_name]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_frm_name] ON [dbo].[_frm]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_frm_name_full]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_frm_name_full] ON [dbo].[_frm]
(
	[name_full] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [_dta_index__journal_27_362846613__K3_K2_K5_K6_K1_9]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [_dta_index__journal_27_362846613__K3_K2_K5_K6_K1_9] ON [dbo].[_journal]
(
	[id_user] ASC,
	[when] ASC,
	[id_base] ASC,
	[id_table] ASC,
	[id] ASC
)
INCLUDE ( 	[score]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [_dta_index__journal_27_362846613__K6_K10_K2_K4_1]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [_dta_index__journal_27_362846613__K6_K10_K2_K4_1] ON [dbo].[_journal]
(
	[id_table] ASC,
	[del] ASC,
	[when] ASC,
	[id_edittype] ASC
)
INCLUDE ( 	[id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_journal_change_id]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_journal_change_id] ON [dbo].[_journal]
(
	[change_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_journal_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_journal_del] ON [dbo].[_journal]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_journal_id_base]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_journal_id_base] ON [dbo].[_journal]
(
	[id_base] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_journal_id_edittype]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_journal_id_edittype] ON [dbo].[_journal]
(
	[id_edittype] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_journal_id_table]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_journal_id_table] ON [dbo].[_journal]
(
	[id_table] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_journal_id_user]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_journal_id_user] ON [dbo].[_journal]
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_journal_when]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_journal_when] ON [dbo].[_journal]
(
	[when] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_log_id_type]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_log_id_type] ON [dbo].[_log]
(
	[id_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_log_id_user]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_log_id_user] ON [dbo].[_log]
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_log_when]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_log_when] ON [dbo].[_log]
(
	[when] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_logtype_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_logtype_del] ON [dbo].[_logtype]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_role_access_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_role_access_del] ON [dbo].[_role_access]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_role_access_id_access]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_role_access_id_access] ON [dbo].[_role_access]
(
	[id_access] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_role_access_id_role]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_role_access_id_role] ON [dbo].[_role_access]
(
	[id_role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_role_where_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_role_where_del] ON [dbo].[_role_where]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_role_where_id_role]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_role_where_id_role] ON [dbo].[_role_where]
(
	[id_role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_role_where_id_table]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_role_where_id_table] ON [dbo].[_role_where]
(
	[id_table] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_table_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_table_del] ON [dbo].[_table]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_table_name]    Script Date: 27.04.2016 10:01:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_table_name] ON [dbo].[_table]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_truefalse]    Script Date: 27.04.2016 10:01:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_truefalse] ON [dbo].[_truefalse]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [_dta_index__user_27_1761907702__K6_K1_3]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [_dta_index__user_27_1761907702__K6_K1_3] ON [dbo].[_user]
(
	[del] ASC,
	[id] ASC
)
INCLUDE ( 	[name]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_del] ON [dbo].[_user]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_id_department]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_id_department] ON [dbo].[_user]
(
	[id_department] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_user_login]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_login] ON [dbo].[_user]
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_user_mail]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_mail] ON [dbo].[_user]
(
	[mail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_user_name]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_name] ON [dbo].[_user]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_user_sname]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_sname] ON [dbo].[_user]
(
	[sname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_watch]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_watch] ON [dbo].[_user]
(
	[watch] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_access_id_access]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_access_id_access] ON [dbo].[_user_access]
(
	[id_access] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_access_id_user]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_access_id_user] ON [dbo].[_user_access]
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX__user_role_base]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX__user_role_base] ON [dbo].[_user_role_base]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX__user_role_base_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX__user_role_base_del] ON [dbo].[_user_role_base]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_role_base_id_base]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_role_base_id_base] ON [dbo].[_user_role_base]
(
	[id_base] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_role_base_id_role]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_role_base_id_role] ON [dbo].[_user_role_base]
(
	[id_role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_role_base_id_user]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_role_base_id_user] ON [dbo].[_user_role_base]
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_setting_id_user]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_setting_id_user] ON [dbo].[_user_setting]
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_user_setting_key]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_setting_key] ON [dbo].[_user_setting]
(
	[key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_setting_last_upd]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_user_setting_last_upd] ON [dbo].[_user_setting]
(
	[last_upd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_yesno]    Script Date: 27.04.2016 10:01:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_yesno] ON [dbo].[_yesno]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [_dta_index_main_archive_27_411200565__K10_K22_K1]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [_dta_index_main_archive_27_411200565__K10_K22_K1] ON [dbo].[main_archive]
(
	[id_doctree] ASC,
	[del] ASC,
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [_dta_index_main_archive_27_411200565__K11_K22_K1]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [_dta_index_main_archive_27_411200565__K11_K22_K1] ON [dbo].[main_archive]
(
	[id_doctype] ASC,
	[del] ASC,
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [_dta_index_main_archive_27_411200565__K2_K1_K20]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [_dta_index_main_archive_27_411200565__K2_K1_K20] ON [dbo].[main_archive]
(
	[num_doc] ASC,
	[id] ASC,
	[id_parent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [_dta_index_main_archive_27_411200565__K20_K1_K10_K2]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [_dta_index_main_archive_27_411200565__K20_K1_K10_K2] ON [dbo].[main_archive]
(
	[id_parent] ASC,
	[id] ASC,
	[id_doctree] ASC,
	[num_doc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [_dta_index_main_archive_27_411200565__K22]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [_dta_index_main_archive_27_411200565__K22] ON [dbo].[main_archive]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_accept]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_accept] ON [dbo].[main_archive]
(
	[accept] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_date_doc]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_date_doc] ON [dbo].[main_archive]
(
	[date_doc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_date_upd]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_date_upd] ON [dbo].[main_archive]
(
	[date_upd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_docpack]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_docpack] ON [dbo].[main_archive]
(
	[docpack] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_hidden]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_hidden] ON [dbo].[main_archive]
(
	[hidden] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_id_depart]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_id_depart] ON [dbo].[main_archive]
(
	[id_depart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_id_doctree]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_id_doctree] ON [dbo].[main_archive]
(
	[id_doctree] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_id_doctype]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_id_doctype] ON [dbo].[main_archive]
(
	[id_doctype] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_id_frm_contr]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_id_frm_contr] ON [dbo].[main_archive]
(
	[id_frm_contr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_id_frm_dev]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_id_frm_dev] ON [dbo].[main_archive]
(
	[id_frm_dev] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_id_frm_prod]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_id_frm_prod] ON [dbo].[main_archive]
(
	[id_frm_prod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_id_parent]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_id_parent] ON [dbo].[main_archive]
(
	[id_parent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_id_perf]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_id_perf] ON [dbo].[main_archive]
(
	[id_perf] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_id_prjcode]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_id_prjcode] ON [dbo].[main_archive]
(
	[id_prjcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_id_state]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_id_state] ON [dbo].[main_archive]
(
	[id_state] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_id_user]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_id_user] ON [dbo].[main_archive]
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_summ]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_summ] ON [dbo].[main_archive]
(
	[summ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_archive_supervisor_checked]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_archive_supervisor_checked] ON [dbo].[main_archive]
(
	[supervisor_checked] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UIX_main_archive_id]    Script Date: 27.04.2016 10:01:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UIX_main_archive_id] ON [dbo].[main_archive]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_docversion_barcode]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_docversion_barcode] ON [dbo].[main_docversion]
(
	[barcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_docversion_date_reg]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_docversion_date_reg] ON [dbo].[main_docversion]
(
	[date_reg] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_docversion_date_upd]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_docversion_date_upd] ON [dbo].[main_docversion]
(
	[date_trans] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_docversion_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_docversion_del] ON [dbo].[main_docversion]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_docversion_del_id_archive]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_docversion_del_id_archive] ON [dbo].[main_docversion]
(
	[del] ASC
)
INCLUDE ( 	[id_archive]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_docversion_id]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_docversion_id] ON [dbo].[main_docversion]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_docversion_id_archive]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_docversion_id_archive] ON [dbo].[main_docversion]
(
	[id_archive] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_docversion_id_archive_id]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_docversion_id_archive_id] ON [dbo].[main_docversion]
(
	[id_archive] ASC,
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_docversion_id_quality]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_docversion_id_quality] ON [dbo].[main_docversion]
(
	[id_quality] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_docversion_main]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_docversion_main] ON [dbo].[main_docversion]
(
	[main] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_docversion_nn]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_docversion_nn] ON [dbo].[main_docversion]
(
	[nn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_person_del]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_person_del] ON [dbo].[main_person]
(
	[del] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_main_person_id_depart]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_person_id_depart] ON [dbo].[main_person]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_main_person_name]    Script Date: 27.04.2016 10:01:29 ******/
CREATE NONCLUSTERED INDEX [IX_main_person_name] ON [dbo].[main_person]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  FullTextIndex     Script Date: 27.04.2016 10:01:29 ******/
CREATE FULLTEXT INDEX ON [dbo].[main_archive](
[doctext] LANGUAGE 'Neutral')
KEY INDEX [UIX_main_archive_id]ON ([main_catalog_doctext], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)


GO
/****** Object:  Trigger [dbo].[UpdateDoctreePre]    Script Date: 27.04.2016 10:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fazl
-- Create date: 26.06.2014
-- Description:	Обновляет таблицу для отрисовки дерева
-- =============================================
CREATE TRIGGER [dbo].[UpdateDoctreePre] 
   ON  [dbo].[_doctree] 
   AFTER INSERT,DELETE,UPDATE
AS 
BEGIN
	SET NOCOUNT ON;
	EXEC [dbo].[PreDocTree]
END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4[30] 2[40] 3) )"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 262
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 6
               Left = 250
               Bottom = 252
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 4
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 26
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'main_docversion_archive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'main_docversion_archive'
GO
