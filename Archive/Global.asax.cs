using System;
using System.Web.Routing;

namespace ArchNet
{
    public class Global : System.Web.HttpApplication
    {
        public void Application_OnStart()
        {
            Application["ActiveSession"] = 0;
            RegRoutes(RouteTable.Routes);
        }

        public void Session_OnStart()
        {
            Application.Lock();
            Application["ActiveSession"] = (int)Application["ActiveSession"] + 1;
            Application.UnLock();
        }

        public void Session_OnEnd()
        {
            Application.Lock();
            Application["ActiveSession"] = (int)Application["ActiveSession"] - 1;
            Application.UnLock();
        }

        private void RegRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("default", "default/{p_base}", "~/Default.aspx");
            routes.MapPageRoute("archive", "archive/{p_base}/{p_page}", "~/Archive/Archive.aspx");
            routes.MapPageRoute("user", "admin/user", "~/Admin/User.aspx");
            routes.MapPageRoute("role", "admin/role", "~/Admin/Role.aspx");
            routes.MapPageRoute("journalcommon", "admin/journalcommon", "~/Admin/JournalCommon.aspx");
            routes.MapPageRoute("sesval", "admin/sesval", "~/Admin/SesVal.aspx");
            routes.MapPageRoute("base", "admin/base", "~/Admin/Base.aspx");

            routes.MapPageRoute("table", "admin/table", "~/Admin/Table.aspx");
            routes.MapPageRoute("twit", "admin/twit", "~/Admin/Twit.aspx");
            routes.MapPageRoute("frm", "sprav/frm", "~/sprav/Frm.aspx");

            routes.MapPageRoute("doctree", "sprav/doctree", "~/sprav/Doctree.aspx");
            routes.MapPageRoute("person", "sprav/{p_base}/person", "~/sprav/Person.aspx");
            routes.MapPageRoute("help", "help", "~/Site/Help.aspx");
            routes.MapPageRoute("blog", "blog", "~/Site/Blog.aspx");
            routes.MapPageRoute("docversion", "docversion/{p_base}", "~/Archive/Docversion.aspx");
            routes.MapPageRoute("archivedel", "archivedel/{p_base}/{p_page}", "~/Archive/ArchiveDel.aspx");
            routes.MapPageRoute("changedoctype", "changedoctype/{p_base}", "~/Archive/ChangeDoctype.aspx");
            /*
             routes.MapPageRoute("archivedub", "archivedub/{p_base}", "~/Archive/ArchiveDub.aspx");
             routes.MapPageRoute("changedoctype", "changedoctype/{p_base}", "~/Archive/ChangeDoctype.aspx");
             routes.MapPageRoute("docversion", "docversion/{p_base}", "~/Archive/Docversion.aspx");
             routes.MapPageRoute("getfile", "getfile/{p_base}/{id_archive}/{id_docversion}/{barcode}", "~/Archive/GetFile.aspx");
             routes.MapPageRoute("complect", "complect", "~/Archive/Complect.aspx");
             routes.MapPageRoute("complectlist", "complectlist", "~/Archive/ComplectList.aspx");
             routes.MapPageRoute("complectnew", "complectnew", "~/Archive/ComplectNew.aspx");
             routes.MapPageRoute("complectnewlistarchive", "complectnewlistarchive", "~/Archive/ComplectNewListArchive.aspx");
             routes.MapPageRoute("complectnewlist", "complectnewlist", "~/Archive/ComplectNewList.aspx");
             routes.MapPageRoute("complectrepair", "complectrepair", "~/Archive/ComplectRepair.aspx");
             routes.MapPageRoute("complectrepairlist", "complectrepairlist", "~/Archive/ComplectRepairList.aspx");
             routes.MapPageRoute("archivedepart", "archivedepart/{p_base}", "~/Archive/ArchiveDepart.aspx");
             routes.MapPageRoute("regstor", "regstor", "~/Archive/Regstor.aspx");
             routes.MapPageRoute("regstorlist", "regstorlist", "~/Archive/RegstorList.aspx");
             routes.MapPageRoute("archivestructur", "archivestructur/{p_base}", "~/Archive/ArchiveStructur.aspx");
             routes.MapPageRoute("copytobase", "copytobase/{p_base}/{p_page}/{id}", "~/Archive/CopyToBase.aspx");
             //
             routes.MapPageRoute("journal", "service/{p_base}/journal", "~/service/Journal.aspx");
             routes.MapPageRoute("settings", "service/{p_base}/settings", "~/service/settings.aspx");
             routes.MapPageRoute("fillbybarcode", "service/{p_base}/fillbybarcode", "~/service/FillByBarcode.aspx");
             routes.MapPageRoute("checkbybarcode", "service/{p_base}/checkbybarcode", "~/service/CheckByBarcode.aspx");
             routes.MapPageRoute("archiveperform", "service/{p_base}/archiveperform", "~/service/ArchivePerform.aspx");
             routes.MapPageRoute("archivedocpackdown", "service/{p_base}/archivedocpackdown", "~/service/ArchiveDocpackFileDownload.aspx");
             routes.MapPageRoute("barcodesearch", "service/{p_base}/barcodesearch", "~/service/BarcodeSearch.aspx");
             routes.MapPageRoute("complectregstorcompare", "complectregstorcompare", "~/Archive/ComplectRegstorCompare.aspx");
             routes.MapPageRoute("bulkedit", "service/{p_base}/{p_page}/bulkedit", "~/Archive/ArchiveBulkEdit.aspx");
             routes.MapPageRoute("mailgroup", "service/mailgroup", "~/service/MailGroup.aspx");
             routes.MapPageRoute("userbarcode", "service/userbarcode", "~/service/UserBarcode.aspx");
             routes.MapPageRoute("shutdown", "service/shutdown", "~/Start.aspx");
             routes.MapPageRoute("archivecheckbox", "service/{p_base}/archivecheckbox/", "~/Archive/ArchiveCheckbox.aspx");
             routes.MapPageRoute("log", "service/log/", "~/service/Log.aspx");
             routes.MapPageRoute("monitor", "service/monitor/", "~/service/Monitor.aspx");
             routes.MapPageRoute("updatemonitor", "service/updatemonitor/", "~/service/UpdateMonitor.aspx");
             //
             routes.MapPageRoute("updatedubat", "routine/{p_base}/updatedubat/", "~/routine/UpdateDublicateArchiveTable.aspx");
             //

             routes.MapPageRoute("bu", "admin/bu/{act}", "~/Admin/BlockUser.aspx");
             routes.MapPageRoute("userrolebase", "admin/userrolebase", "~/Admin/UserRoleBase.aspx");
             routes.MapPageRoute("uservisit", "admin/uservisit", "~/Admin/UserVisit.aspx");
             routes.MapPageRoute("usersetting", "admin/usersetting", "~/Admin/UserSetting.aspx");
             routes.MapPageRoute("journalcommon", "admin/journalcommon", "~/Admin/JournalCommon.aspx");
             
             
             routes.MapPageRoute("access", "admin/access", "~/Admin/AccessKeys.aspx");
             routes.MapPageRoute("base", "admin/base", "~/Admin/Base.aspx");
             routes.MapPageRoute("table", "admin/table", "~/Admin/Table.aspx");
             routes.MapPageRoute("field", "admin/field", "~/Admin/Field.aspx");
             routes.MapPageRoute("deleteoldfiles", "routine/deleteoldfiles", "~/routine/DeleteOldFiles.aspx");
             

             routes.MapPageRoute("ad", "ad", "~/routine/LoadAD.aspx");
             //routes.MapPageRoute("cc", "cc", "~/Admin/ClearCache.aspx");
             //
             routes.MapPageRoute("rating", "rating", "~/Admin/Rating.aspx");
             routes.MapPageRoute("workhour", "workhour", "~/Admin/UserWorkHour.aspx");
             routes.MapPageRoute("score", "score", "~/Admin/UserScore.aspx");
             routes.MapPageRoute("normhour", "normhour", "~/Admin/NormHour.aspx");
             //
             routes.MapPageRoute("frm", "sprav/frm", "~/sprav/Frm.aspx");
             // routes.MapPageRoute("depart", "sprav/{p_base}/depart", "~/sprav/Depart.aspx"); старые подразделения
             routes.MapPageRoute("person", "sprav/{p_base}/person", "~/sprav/Person.aspx");
             routes.MapPageRoute("country", "sprav/{p_base}/country", "~/sprav/Country.aspx");
             routes.MapPageRoute("region", "sprav/{p_base}/region", "~/sprav/Region.aspx");
             routes.MapPageRoute("town", "sprav/{p_base}/town", "~/sprav/Town.aspx");
             routes.MapPageRoute("prjcode", "sprav/prjcode", "~/sprav/PrjCode.aspx");
             routes.MapPageRoute("doctree", "sprav/doctree", "~/sprav/Doctree.aspx");
             routes.MapPageRoute("storage", "sprav/storage", "~/sprav/Storage.aspx");
             routes.MapPageRoute("regstor_group", "sprav/regstor_group", "~/sprav/Regstor_group.aspx");
             routes.MapPageRoute("department", "sprav/{p_base}/department", "~/sprav/Department.aspx");
             routes.MapPageRoute("departmentpre", "sprav/{p_base}/departmentpre", "~/sprav/DepartmentPre.aspx");
             routes.MapPageRoute("department1c", "sprav/department1c", "~/sprav/Department1c.aspx");
             routes.MapPageRoute("person1c", "sprav/person1c", "~/sprav/Person1c.aspx");
             routes.MapPageRoute("event", "sprav/event", "~/sprav/Event.aspx");
             //
             routes.MapPageRoute("help", "help", "~/Site/Help.aspx");
             routes.MapPageRoute("blog", "blog", "~/Site/Blog.aspx");
             //
             routes.MapPageRoute("letgetcards", "{p_base}/letgetcards/{id_archive}/{docpack}", "~/ajax/LetGetCards.aspx");
             routes.MapPageRoute("letgetcardsalt", "{p_base}/letgetcardsalt/{justdog}/{id}", "~/ajax/LetGetCardsAlt.aspx");
             routes.MapPageRoute("letgetcardsalthelp0", "letgetcardsalthelp0", "~/ajax/LetGetCardsAltHelp0.aspx");
             routes.MapPageRoute("letgetcardsalthelp1", "letgetcardsalthelp1", "~/ajax/LetGetCardsAltHelp1.aspx");
             //
             routes.MapPageRoute("ntdstart", "ntdstart", "~/ntd/NtdStart.aspx");
             routes.MapPageRoute("ntd", "ntd/list", "~/ntd/Ntd.aspx");
             routes.MapPageRoute("ntdcategory", "ntd/category", "~/sprav/Ntd_Category.aspx");
             routes.MapPageRoute("ntddepart", "ntd/depart", "~/sprav/Ntd_Depart.aspx");*/
            // Cтрочка эта, последней быть всегда должна
            routes.MapPageRoute("error", "error/{p_base}/{p_error}", "~/ErrorPages/Error.aspx");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception lastError = Server.GetLastError();
                if (lastError != null)
                {
                    Session["ErrorException"] = lastError;//.InnerException;
                    Server.ClearError();

                    Response.Redirect("/ErrorPages/AppError.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Критическая ошибка. Закройте все окна браузера и попробуйте ещё раз. " + ex.Message);
            }
        }
    }
}