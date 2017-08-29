using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AngularDatagrid.Domain;
using System.Web.Http.Results;

namespace AngularDatagrid.Controllers
{
    public class PersonelsController : ApiController
    {
        private PersonelDbEntities db = new PersonelDbEntities();

        public JsonResult<List<Personel>> GetList()
        {
            return Json(db.Personel.ToList());
        }
        [HttpPost]
        public IHttpActionResult Update(Personel personel)
        {
            if (personel != null && personel.Id > 0)
            {
                var entity = db.Personel.FirstOrDefault(x => x.Id == personel.Id);

                if (entity != null)
                {
                    entity.Ad = personel.Ad;
                    entity.Soyad = personel.Soyad;
                    entity.TcNo = personel.TcNo;
                    entity.Unvan = personel.Unvan;
                    entity.DogumTarihi = personel.DogumTarihi;
                    entity.Departman = personel.Departman;

                    if (db.SaveChanges() > 0)
                        return Ok(HttpStatusCode.OK);
                    else
                        return Ok(HttpStatusCode.NotModified);
                }
            }
            return BadRequest("İşlem başarısız!");
        }
        [HttpPost]
        public IHttpActionResult Insert(Personel personel)
        {
            if (personel != null)
            {
                db.Personel.Add(personel);
                if (db.SaveChanges() > 0)
                    return Created("", personel.Id);
            }
            return BadRequest();
        }
    }
}