using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Engefibra.Data.Models;
using Engefibra.Data.Context;
using Engefibra.Web.Framework.Session;
using ClosedXML.Excel;
using System.IO;

namespace Engefibra.Web.Controllers
{
    public class ObraController : Controller
    {
        private AppContext db = new AppContext();

        public ActionResult Index()
        {
            var obra = db.Obra
                .Include(o => o.Cliente)
                .Include(o => o.Encarregado)
                .Include(o => o.Notificacao)
                .Include(o => o.ObraStatus);

            return View(obra.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obra
                .Include(o => o.Cliente)
                .Include(o => o.Encarregado)
                .Include(o => o.Notificacao)
                .Include(o => o.ObraStatus).Where(o => o.Id == id).FirstOrDefault();

            if (obra == null)
            {
                return HttpNotFound();
            }
            return View(obra);
        }

        public ActionResult AddOrUpdate(int id = 0)
        {
            var model = new Data.Models.Obra();

            if (id > 0)
            {
                model = db.Obra
                .Include(o => o.Cliente)
                .Include(o => o.Encarregado)
                .Include(o => o.Notificacao)
                .Include(o => o.ObraStatus).Where(o => o.Id == id).FirstOrDefault();
            }

            ViewBag.ClienteId = new SelectList(db.Pessoa, "Id", "Nome", model.ClienteId);
            ViewBag.EncarregadoId = new SelectList(db.Pessoa, "Id", "Nome", model.EncarregadoId);
            ViewBag.NotificacaoId = new SelectList(db.Notificacao, "Id", "Nome", model.NotificacaoId);
            ViewBag.ObraStatusId = new SelectList(db.ObraStatus, "Id", "Nome", model.ObraStatusId);

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate([Bind(Include="Id,Nome,EncarregadoId,ClienteId,Endereco,Cep,Cidade,ObraStatusId,DataAgendamento,Observacao,Pendencias,NotificacaoId,Ativo,DataCriacao,DataAlteracao,UsuarioCriacao,UsuarioAlteracao")] Obra obra)
        {
            if (obra.Id > 0)
            {
                obra.DataAlteracao = DateTime.Now;
                obra.UsuarioCriacao = SessionManager.Current.ID;

                if (ModelState.IsValid)
                {
                    db.Entry(obra).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Caso a mesma não for agendamento, pode preencher o campo Agendamento com a data atual, não vai ser utilizado
                if (obra.ObraStatusId != 1)
                {
                    obra.DataAgendamento = DateTime.Now;
                }

                obra.DataCriacao = DateTime.Now;
                obra.UsuarioCriacao = SessionManager.Current.ID;

                obra.DataAlteracao = DateTime.Now;
                obra.UsuarioCriacao = SessionManager.Current.ID;

                if (ModelState.IsValid)
                {
                    db.Obra.Add(obra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            var id = obra.Id;
            if (id > 0)
            {
                obra = db.Obra
                    .Include(o => o.Cliente)
                    .Include(o => o.Encarregado)
                    .Include(o => o.Notificacao)
                    .Include(o => o.ObraStatus).Where(o => o.Id == id).FirstOrDefault();
            }

            ViewBag.ClienteId = new SelectList(db.Pessoa, "Id", "Nome", obra.ClienteId);
            ViewBag.EncarregadoId = new SelectList(db.Pessoa, "Id", "Nome", obra.EncarregadoId);
            ViewBag.NotificacaoId = new SelectList(db.Notificacao, "Id", "Nome", obra.NotificacaoId);
            ViewBag.ObraStatusId = new SelectList(db.ObraStatus, "Id", "Nome", obra.ObraStatusId);

            return View("Create", obra);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            return View(obra);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Obra obra = db.Obra.Find(id);
            db.Obra.Remove(obra);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Exporta a obra para xls, para trabalhos futuros fora do sisema
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Excel</returns>
        public ActionResult Export(int id)
        {
            var obra = Bll.Obra.Get(id);

            var dtPedidos = new System.Data.DataTable("Obra_" + obra.Nome);

            dtPedidos.Columns.Add("Nome", typeof(string));
            dtPedidos.Columns.Add("Situação", typeof(string));
            dtPedidos.Columns.Add("Grupo", typeof(string));
            dtPedidos.Columns.Add("Cliente", typeof(string));
            dtPedidos.Columns.Add("Cliente CPFCnpj", typeof(string));
            dtPedidos.Columns.Add("Endereço", typeof(string));
            dtPedidos.Columns.Add("Encarregado", typeof(string));
            dtPedidos.Columns.Add("Observacoes", typeof(string));
            dtPedidos.Columns.Add("Pendencias", typeof(string));
            dtPedidos.Columns.Add("Data Cadastro", typeof(string));

            dtPedidos.Rows.Add(obra.Nome,
                                obra.ObraStatus.Nome,
                                obra.Notificacao.Nome,
                                obra.Cliente.Nome,
                                obra.Cliente.CpfCnpj,
                                obra.Endereco + ", " + obra.Cep + ", " + obra.Cidade,
                                obra.Encarregado.Nome,
                                obra.Observacao,
                                obra.Pendencias,
                                obra.DataCriacao);

            DataSet dsPlanilha = new DataSet();
            dsPlanilha.Tables.Add(dtPedidos);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dsPlanilha);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + "Obra_" + obra.Id + "_" + DateTime.Now.ToString("ddMMyyyy_hh_mm_ss") + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

            return View("Exportacao");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
