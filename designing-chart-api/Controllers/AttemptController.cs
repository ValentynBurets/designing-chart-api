using Business.Contract.Model;
using Business.Contract.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace designing_chart_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttemptController : BaseController 
    {
        private readonly IAttemptService _attemptService;

        public AttemptController(IAttemptService attemptService)
        {
            _attemptService = attemptService;
        }

        //// GET: AttemptController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: AttemptController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: AttemptController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: AttemptController/Create
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Create(CreateAttemptViewModel attempt)
        {
            try
            {
                //attempt.StudentId = GetUserId();
                await _attemptService.Create(attempt, GetUserId());
                return Ok("New attempt created");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //// GET: AttemptController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AttemptController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AttemptController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AttemptController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
