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
    public class ExercisesController : Controller
    {
        private readonly IExerciseService _exerciseService;
                                          
        public ExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        //// GET: ExercisesController
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Index(string sortOrder)
        {
            try
            {
                var exercises = await _exerciseService.GetSorted(sortOrder);
                return Ok(exercises);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        //// GET: ExercisesController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: ExercisesController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // GET: ExercisesController/GetAll
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var exercises = await _exerciseService.GetAll();
                return Ok(exercises);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // GET: ExercisesController/GetAll
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult> GetByid(Guid Id)
        {
            try
            {
                var exercise = await _exerciseService.GetById(Id);
                return Ok(exercise);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // POST: ExercisesController/Create
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Create(CreateExerciseViewModel newExercise)
        {
            try
            {
                await _exerciseService.Create(newExercise);
                return Ok("New exercise created!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // POST: ExercisesController/Edit/5
        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult> Edit(Guid id, CreateExerciseViewModel exercise)
        {
            try
            {
                await _exerciseService.Edit(id, exercise);
                return Accepted("Сhanges applied!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // GET: ExercisesController/Delete/5
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _exerciseService.Delete(id);
                return Accepted("Exercise deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        //// POST: ExercisesController/Delete/5
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
