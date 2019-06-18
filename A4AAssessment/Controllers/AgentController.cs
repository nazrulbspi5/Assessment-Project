using A4AAssessment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A4AAssessment.Controllers
{
    public class AgentController : Controller
    {
        A4AAssessmentEntities1 db = new A4AAssessmentEntities1();

        // GET: Agent
        public ActionResult Agent()
        {
            ViewBag.Code = "0009";
            ViewBag.MarkUpId = new SelectList(db.MarkUpPlans, "MarkUpId", "MarkUpPlanName");
            return View();
        }
        // POST: Agent
        public ActionResult SaveAgentInfo(BusinessEntity BusinessEntity)
        {
            try
            {
                var BE = db.BusinessEntities.Where(s => s.Code == BusinessEntity.Code && s.Name == BusinessEntity.Name).FirstOrDefault();
                if (BE == null)
                {
                    BusinessEntity.Status = 1;
                    BusinessEntity.SMTPPort = 1;
                    BusinessEntity.Deleted = false;
                    BusinessEntity.CurrentBalance = BusinessEntity.Balance;
                    BusinessEntity.CreatedOnUtc = DateTime.UtcNow;
                    db.BusinessEntities.Add(BusinessEntity);
                    db.SaveChanges();
                    return Json(new { Msg = "Saved Successfully.", Satatus = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Msg = "This Agent Code: "+ BusinessEntity.Code+ " & Name: " + BusinessEntity.Name + " Already Exist!", Satatus = 0 }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        // PUT: Agent
        public ActionResult UpdateAgentInfo(BusinessEntity BusinessEntity)
        {
            try
            {
                //var BE_Item = db.BusinessEntities.Where(s => s.BusinessId == BusinessEntity.BusinessId).FirstOrDefault();
                //BusinessEntity.CreatedOnUtc = BE_Item.CreatedOnUtc;
                                
                BusinessEntity.Status = 1;
                BusinessEntity.SMTPPort = 1;
                BusinessEntity.Deleted = false;
                BusinessEntity.CurrentBalance = BusinessEntity.Balance;
                BusinessEntity.UpdatedOnUtc = DateTime.UtcNow;
                BusinessEntity.CreatedOnUtc = BusinessEntity.UpdatedOnUtc;
                db.Entry(BusinessEntity).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Msg = "Update Successfully.", Satatus = 1 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        //For Bind DataTable/Grid View
        public ActionResult GetAgentListData()
        {
            var BE_itemList = db.BusinessEntities.Where(s => s.Deleted == false);
            foreach (var item in BE_itemList)
            {
                item.MarkUpPlanName = db.MarkUpPlans.Where(x => x.MarkUpId == item.MarkUpId).Select(s => s.MarkUpPlanName).FirstOrDefault().ToString();
                item.CreatedOnUtc = item.CreatedOnUtc;
            }
            return Json(new { data = BE_itemList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemById(long BusinessId)
        {
            BusinessEntity BusinessEntity = db.BusinessEntities.Find(BusinessId);
            return Json(BusinessEntity, JsonRequestBehavior.AllowGet);
        }

        // DELETE: Agent
        public ActionResult DeleteAgent(long BusinessId)
        {
            BusinessEntity BusinessEntity = db.BusinessEntities.Find(BusinessId);

            #region For Remove the data in table
            //if (BE_item == null)
            //{
            //    return Json(0, JsonRequestBehavior.AllowGet);
            //}
            //db.BusinessEntities.Remove(BE_item);
            #endregion

            if (BusinessId != BusinessEntity.BusinessId)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            BusinessEntity.Deleted = true;
            db.Entry(BusinessEntity).State = EntityState.Modified;

            db.SaveChangesAsync();
            return Json(1, JsonRequestBehavior.AllowGet);

        }

    }
}