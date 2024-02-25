using L01_2020AC602.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace L01_2020AC602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly BlogContext _blogContext;

        public comentariosController(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        // GET: api/<comentariosController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Comentario> comentarios = new List<Comentario>(from e in _blogContext.Comentarios select e).ToList();
            if (comentarios.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(comentarios);
            }
        }
        // GET api/<comentariosController>/5
        [HttpGet("{id}")]
        public IActionResult Getbyid(int id)
        {
            List<Comentario> comentarios = _blogContext.Comentarios.Where(u => u.PublicacionId == id).ToList();

            if (comentarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(comentarios);
        }

        // POST api/<comentariosController>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] Comentario comentarios)
        {
            try
            {
                _blogContext.Add(comentarios);
                _blogContext.SaveChanges();
                return Ok(comentarios);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        // PUT api/<comentariosController>/5
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Put(int id, [FromBody] Comentario comentarioModificar)
        {
            Comentario? comentarioActual = (from e in _blogContext.Comentarios where e.CometarioId == id select e).FirstOrDefault();
            if (comentarioActual == null)
            {
                return NotFound();
            }

            comentarioActual.PublicacionId = comentarioModificar.PublicacionId;
            comentarioActual.Comentario1 = comentarioModificar.Comentario1;
            comentarioActual.UsuarioId = comentarioModificar.UsuarioId;
            

            _blogContext.Entry(comentarioActual).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _blogContext.SaveChanges();

            return Ok(comentarioModificar);
        }

        // DELETE api/<comentariosController>/5
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            Comentario? comentario = (from e in _blogContext.Comentarios where e.CometarioId == id select e).FirstOrDefault();
            if (comentario == null)
            {
                return NotFound();
            }

            _blogContext.Comentarios.Attach(comentario);
            _blogContext.Comentarios.Remove(comentario);
            _blogContext.SaveChanges();
            return Ok(id);
        }
    }
}
