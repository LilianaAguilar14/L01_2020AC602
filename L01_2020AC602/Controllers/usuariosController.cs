using L01_2020AC602.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace L01_2020AC602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly BlogContext _blogContext;

        public usuariosController(BlogContext    blogContext)
        {
            _blogContext = blogContext;
        }
        // GET: api/<usuariosController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Usuario> usuarios = new List<Usuario>( from e in _blogContext.Usuarios select e).ToList();
            if (usuarios.Count == 0)
            {
                return NotFound();
            }
            else
            { 
             return Ok(usuarios);
            }
        }

        //GET api/<usuariosController>/5
        [HttpGet]
        [Route("GetbyName")]
        public async Task<ActionResult> Get(string Nombre, string Apellido)
        {
            List<Usuario> usuarios = await _blogContext.Usuarios.Where(u => u.Nombre.Contains(Nombre) && u.Apellido.Contains(Apellido)).ToListAsync();

            if (usuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        [HttpGet]
        [Route("GetbyRol")]
        public async Task<ActionResult> GetbyRol(int Rol)
        {
            List<Usuario> usuarios = await _blogContext.Usuarios.Where(u => u.RolId==Rol).ToListAsync();

            if (usuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }
        // POST api/<usuariosController>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                _blogContext.Add(usuario);
                _blogContext.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<usuariosController>/5
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuarioModificar)
        {
            Usuario? usuarioActual = (from e in _blogContext.Usuarios where e.UsuarioId == id select e).FirstOrDefault();
            if (usuarioActual == null)
            {
                return NotFound();
            }

            usuarioActual.RolId = usuarioModificar.RolId;
            usuarioActual.NombreUsuario = usuarioModificar.NombreUsuario;
            usuarioActual.Clave = usuarioModificar.Clave;
            usuarioActual.Nombre = usuarioModificar.Nombre;
            usuarioActual.Apellido = usuarioModificar.Apellido;

            _blogContext.Entry(usuarioActual).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _blogContext.SaveChanges();

            return Ok(usuarioModificar);
        }

        // DELETE api/<usuariosController>/5
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            Usuario? usuario = (from e in _blogContext.Usuarios where e.UsuarioId == id select e).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }

            _blogContext.Usuarios.Attach(usuario);
            _blogContext.Usuarios.Remove(usuario);
            _blogContext.SaveChanges();
            return Ok(id);
        }
    }
}
