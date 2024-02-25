using L01_2020AC602.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace L01_2020AC602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publicacionesController : ControllerBase
    {
        private readonly BlogContext _blogContext;

        public publicacionesController(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        // GET: api/<publicacionesController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Publicacione> publicaciones = new List<Publicacione>(from e in _blogContext.Publicaciones select e).ToList();
            if (publicaciones.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(publicaciones);
            }
        }

        // GET api/<publicacionesController>/5
        [HttpGet("{id}")]
        public IActionResult Getbyid(int id)
        {
            List<Publicacione> publicaciones = _blogContext.Publicaciones.Where(u => u.UsuarioId == id).ToList();

            if (publicaciones.Count == 0)
            {
                return NotFound();
            }

            return Ok(publicaciones);
        }



        // POST api/<publicacionesController>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] Publicacione publicaciones)
        {
            try
            {
                _blogContext.Add(publicaciones);
                _blogContext.SaveChanges();
                return Ok(publicaciones);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<publicacionesController>/5
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Put(int id, [FromBody] Publicacione publicacionModificar)
        {
            Publicacione? publicacionActual = (from e in _blogContext.Publicaciones where e.PublicacionId == id select e).FirstOrDefault();
            if (publicacionActual == null)
            {
                return NotFound();
            }

           
            publicacionActual.Titulo = publicacionModificar.Titulo;
            publicacionActual.Descripcion = publicacionModificar.Descripcion;
            publicacionActual.UsuarioId = publicacionModificar.UsuarioId;
           

            _blogContext.Entry(publicacionActual).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _blogContext.SaveChanges();

            return Ok(publicacionModificar);
        }

        // DELETE api/<publicacionesController>/5
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            Publicacione? publicaciones = (from e in _blogContext.Publicaciones where e.PublicacionId == id select e).FirstOrDefault();
            if (publicaciones == null)
            {
                return NotFound();
            }

            _blogContext.Publicaciones.Attach(publicaciones);
            _blogContext.Publicaciones.Remove(publicaciones);
            _blogContext.SaveChanges();
            return Ok(id);
        }
    }
}

